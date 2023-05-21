using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.Exercises;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

public class ExercisesController : BaseEntityController<Exercise>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Exercises
            .Include(e => e.Classroom)
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Exercises == null)
        {
            return NotFound();
        }

        var exercise = await DbContext.Exercises
            .Include(e => e.Classroom)
            .Include(e => e.ExerciseUsers)
            .ThenInclude(e => e.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    public async Task<IActionResult> ImportExerciseUsers(Guid? id)
    {
        if (id == null)
            return View("Index");
        if (id == null || DbContext.Exercises == null)
        {
            return NotFound();
        }
        var exercise = await DbContext.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        var viewModel = new ImportUsersToExerciseViewModel
        {
            ExerciseId = exercise.Id,
            ExerciseTitle = exercise.Title
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportExerciseUsers(ImportUsersToExerciseViewModel viewModel)
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
        var results = new List<IdentityResult>();
        foreach (var user in users)
        {
            results.Add(await UserManager.CreateAsync(user, user.UserName));
        }

        var exercise = await DbContext.Exercises
            .Include(c => c.ExerciseUsers)
            .SingleOrDefaultAsync(c => c.Id == viewModel.ExerciseId);

        if (exercise == null)
        {
            return NotFound();
        }

        exercise.ExerciseUsers.AddRange(
            users.Select(user => new ExerciseUser { User = user })
        );

        await DbContext.SaveChangesAsync();

        ViewBag.Success = $"Successfully imported {results.Count(x => x.Succeeded)} rows.";
        return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Instruction,Link,TotalScore,Deadline,Topic,Criteria,ClassroomId,Id,CreateDate")] Exercise exercise)
    {
        if (ModelState.IsValid)
        {
            exercise.Id = Guid.NewGuid();
            DbContext.Add(exercise);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", exercise.ClassroomId);
        return View(exercise);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Exercises == null)
        {
            return NotFound();
        }

        var exercise = await DbContext.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", exercise.ClassroomId);
        return View(exercise);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Instruction,Link,TotalScore,Deadline,Topic,Criteria,ClassroomId,Id,CreateDate")] Exercise exercise)
    {
        if (id != exercise.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(exercise);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(exercise.Id))
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
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", exercise.ClassroomId);
        return View(exercise);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Exercises == null)
        {
            return NotFound();
        }

        var exercise = await DbContext.Exercises
            .Include(e => e.Classroom)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Exercises == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Exercises'  is null.");
        }
        var exercise = await DbContext.Exercises.FindAsync(id);
        if (exercise != null)
        {
            DbContext.Exercises.Remove(exercise);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExerciseExists(Guid id)
    {
        return DbContext.Exercises.Any(e => e.Id == id);
    }
}
