using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.Faculties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

public class FacultiesController : BaseEntityController<Faculty>
{
    // GET: Faculties
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Faculties
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    // GET: Faculties/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Faculties == null)
        {
            return NotFound();
        }

        var faculty = await DbContext.Faculties
            .FirstOrDefaultAsync(m => m.Id == id);
        if (faculty == null)
        {
            return NotFound();
        }

        return View(faculty);
    }

    public async Task<IActionResult> ImportFacultyUsers(Guid? id)
    {
        if (id == null)
            return View("Index");
        if (id == null || DbContext.Faculties == null)
        {
            return NotFound();
        }
        var classroom = await DbContext.Faculties.FindAsync(id);
        if (classroom == null)
        {
            return NotFound();
        }
        var viewModel = new ImportUsersToFacultyViewModel
        {
            FacultyId = classroom.Id,
            FacultyName = classroom.Name
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportFacultyUsers(ImportUsersToFacultyViewModel viewModel)
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
        foreach (var user in users)
        {
            var dbUser = await UserManager.FindByNameAsync(user.UserName) ?? await UserManager.FindByEmailAsync(user.Email);
            if (dbUser == null)
            {
                user.Id = Guid.NewGuid();
                user.FacultyId = viewModel.FacultyId;
                results.Add(await UserManager.CreateAsync(user, user.UserName).ConfigureAwait(false));
                await UserManager.AddToRoleAsync(user, "Student");
            }
            else
            {
                dbUser.FacultyId = viewModel.FacultyId;
                DbContext.Entry(dbUser).State = EntityState.Modified;
                DbContext.Update(dbUser);
            }
        }
        await DbContext.SaveChangesAsync().ConfigureAwait(false);

        ViewBag.Success = $"Successfully imported {results.Count(x => x.Succeeded)} rows.";
        return RedirectToAction("Index");
    }

    // GET: Faculties/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Faculties/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Id,CreateDate")] Faculty faculty)
    {
        if (ModelState.IsValid)
        {
            faculty.Id = Guid.NewGuid();
            DbContext.Add(faculty);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(faculty);
    }

    // GET: Faculties/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Faculties == null)
        {
            return NotFound();
        }

        var faculty = await DbContext.Faculties.FindAsync(id);
        if (faculty == null)
        {
            return NotFound();
        }
        return View(faculty);
    }

    // POST: Faculties/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreateDate")] Faculty faculty)
    {
        if (id != faculty.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(faculty);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyExists(faculty.Id))
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
        return View(faculty);
    }

    // GET: Faculties/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Faculties == null)
        {
            return NotFound();
        }

        var faculty = await DbContext.Faculties
            .FirstOrDefaultAsync(m => m.Id == id);
        if (faculty == null)
        {
            return NotFound();
        }

        return View(faculty);
    }

    // POST: Faculties/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Faculties == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Faculties'  is null.");
        }
        var faculty = await DbContext.Faculties.FindAsync(id);
        if (faculty != null)
        {
            DbContext.Faculties.Remove(faculty);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FacultyExists(Guid id)
    {
        return DbContext.Faculties.Any(e => e.Id == id);
    }
}
