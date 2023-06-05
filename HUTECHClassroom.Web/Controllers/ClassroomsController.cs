﻿using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.Classrooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(DeanOrTrainingOfficePolicy)]
public class ClassroomsController : BaseEntityController<Classroom>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Classrooms
            .Include(c => c.Faculty)
            .Include(c => c.Lecturer)
            .Include(c => c.Subject)
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Classrooms == null)
        {
            return NotFound();
        }

        var classroom = await DbContext.Classrooms
            .Include(c => c.Faculty)
            .Include(c => c.Lecturer)
            .Include(c => c.Subject)
            .Include(c => c.ClassroomUsers)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (classroom == null)
        {
            return NotFound();
        }
        ViewBag.Users = classroom.ClassroomUsers;
        return View(classroom);
    }

    public async Task<IActionResult> ImportClassroomUsers(Guid? id)
    {
        if (id == null)
            return View("Index");
        if (id == null || DbContext.Classrooms == null)
        {
            return NotFound();
        }
        var classroom = await DbContext.Classrooms.FindAsync(id);
        if (classroom == null)
        {
            return NotFound();
        }
        var viewModel = new ImportUsersToClassroomViewModel
        {
            ClassroomId = classroom.Id,
            ClassroomTitle = classroom.Title
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportClassroomUsers(ImportUsersToClassroomViewModel viewModel)
    {
        if (viewModel.File == null || viewModel.File.Length == 0)
        {
            ViewBag.Error = "Please select a file to upload.";
            return View(viewModel);
        }

        if (!Path.GetExtension(viewModel.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Please select an Excel file (.xlsx).";
            return View(viewModel);
        }

        var users = ExcelService.ReadExcelFileWithColumnNames<ApplicationUser>(viewModel.File.OpenReadStream(), null);
        // Do something with the imported people data, such as saving to a database
        var results = new List<IdentityResult>();
        var dbUsers = new List<ApplicationUser>();
        var existingUsers = await DbContext.Users
            .Where(u => users.Select(x => x.UserName).Contains(u.UserName) || users.Select(x => x.Email).Contains(u.Email))
            .ToListAsync();
        foreach (var user in users)
        {
            var dbUser = existingUsers.FirstOrDefault(u => u.UserName == user.UserName || u.Email == user.Email); ;
            if (dbUser == null)
            {
                user.Id = Guid.NewGuid();
                results.Add(await UserManager.CreateAsync(user, user.UserName).ConfigureAwait(false));
                await UserManager.AddToRoleAsync(user, "Student");
                dbUsers.Add(user);
            }
            else
            {
                dbUsers.Add(dbUser);
            }
        }

        var classroom = await DbContext.Classrooms
            .Include(c => c.ClassroomUsers)
            .SingleOrDefaultAsync(c => c.Id == viewModel.ClassroomId);

        if (classroom == null)
        {
            return NotFound();
        }

        classroom.ClassroomUsers.AddRange(
            users.Select(user => new ClassroomUser { User = user })
        );

        await DbContext.SaveChangesAsync().ConfigureAwait(false);

        ViewBag.Success = $"Successfully imported {results.Count(x => x.Succeeded)} rows.";
        return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name");
        ViewData["LecturerId"] = new SelectList(DbContext.Users, "Id", "UserName");
        ViewData["SubjectId"] = new SelectList(DbContext.Subjects, "Id", "Title");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Topic,Room,Description,LecturerId,FacultyId,SubjectId,Id,CreateDate")] Classroom classroom)
    {
        if (ModelState.IsValid)
        {
            classroom.Id = Guid.NewGuid();
            DbContext.Add(classroom);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", classroom.FacultyId);
        ViewData["LecturerId"] = new SelectList(DbContext.Users, "Id", "UserName", classroom.LecturerId);
        ViewData["SubjectId"] = new SelectList(DbContext.Subjects, "Id", "Title", classroom.SubjectId);
        return View(classroom);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Classrooms == null)
        {
            return NotFound();
        }

        var classroom = await DbContext.Classrooms.FindAsync(id);
        if (classroom == null)
        {
            return NotFound();
        }
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", classroom.FacultyId);
        ViewData["LecturerId"] = new SelectList(DbContext.Users, "Id", "UserName", classroom.LecturerId);
        ViewData["SubjectId"] = new SelectList(DbContext.Subjects, "Id", "Title", classroom.SubjectId);
        return View(classroom);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Topic,Room,Description,LecturerId,FacultyId,SubjectId,Id,CreateDate")] Classroom classroom)
    {
        if (id != classroom.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(classroom);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(classroom.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", classroom.FacultyId);
        ViewData["LecturerId"] = new SelectList(DbContext.Users, "Id", "UserName", classroom.LecturerId);
        ViewData["SubjectId"] = new SelectList(DbContext.Subjects, "Id", "Title", classroom.SubjectId);
        return View(classroom);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Classrooms == null)
        {
            return NotFound();
        }

        var classroom = await DbContext.Classrooms
            .Include(c => c.Faculty)
            .Include(c => c.Lecturer)
            .Include(c => c.Subject)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (classroom == null)
        {
            return NotFound();
        }

        return View(classroom);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Classrooms == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Classrooms'  is null.");
        }
        var classroom = await DbContext.Classrooms.FindAsync(id);
        if (classroom != null)
        {
            DbContext.Classrooms.Remove(classroom);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClassroomExists(Guid id)
    {
        return DbContext.Classrooms.Any(e => e.Id == id);
    }
}
