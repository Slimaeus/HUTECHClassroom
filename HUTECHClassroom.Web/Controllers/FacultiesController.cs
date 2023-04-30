using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class FacultiesController : BaseEntityController<Faculty>
{
    // GET: Faculties
    public async Task<IActionResult> Index()
    {
        return View(await DbContext.Faculties.ToListAsync());
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
