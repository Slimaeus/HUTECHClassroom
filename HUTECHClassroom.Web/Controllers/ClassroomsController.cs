using AutoMapper.QueryableExtensions;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.ApplicationUsers;
using HUTECHClassroom.Web.ViewModels.Classrooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(DeanOrTrainingOfficePolicy)]
public sealed class ClassroomsController : BaseEntityController<Classroom>
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

        var classroom = await DbContext.Classrooms
            .Include(c => c.ClassroomUsers)
            .SingleOrDefaultAsync(c => c.Id == viewModel.ClassroomId);

        if (classroom == null)
        {
            return NotFound();
        }

        var userViewModels = ExcelService.ReadExcelFileWithColumnNames<ImportedUserViewModel>(viewModel.File.OpenReadStream(), null);
        // Do something with the imported people data, such as saving to a database
        var dbUsers = new List<ApplicationUser>();

        var existingUsers = await DbContext.Users
            .Include(x => x.ClassroomUsers)
            .Where(u => userViewModels.Select(x => x.UserName).Contains(u.UserName))
            .ToListAsync();

        var newUsers = userViewModels
            .Where(vm => !existingUsers.Select(x => x.UserName).Contains(vm.UserName))
            .AsQueryable()
            .ProjectTo<ApplicationUser>(Mapper.ConfigurationProvider)
            .ToList();

        foreach (var user in newUsers)
        {
            if (user.UserName is null) continue;
            var result = await UserManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
            if (result.Succeeded)
                await UserManager.AddToRoleAsync(user, RoleConstants.Student).ConfigureAwait(false);
            dbUsers.Add(user);
        }

        dbUsers.AddRange(existingUsers.Where(x => !x.ClassroomUsers.Any(cu => cu.ClassroomId == viewModel.ClassroomId)));

        classroom.ClassroomUsers.AddRange(
            dbUsers.Select(user => new ClassroomUser { User = user })
        );

        int count = await DbContext.SaveChangesAsync().ConfigureAwait(false);

        ViewBag.Success = $"Successfully imported and updated {count} rows.";
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
