using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class ProjectsController : BaseEntityController<Project>
{
    // GET: Projects
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = DbContext.Projects.Include(p => p.Group);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Projects/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Projects == null)
        {
            return NotFound();
        }

        var project = await DbContext.Projects
            .Include(p => p.Group)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // GET: Projects/Create
    public IActionResult Create()
    {
        ViewData["GroupId"] = new SelectList(DbContext.Groups, "Id", "Name");
        return View();
    }

    // POST: Projects/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,GroupId,Id,CreateDate")] Project project)
    {
        if (ModelState.IsValid)
        {
            project.Id = Guid.NewGuid();
            DbContext.Add(project);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["GroupId"] = new SelectList(DbContext.Groups, "Id", "Name", project.GroupId);
        return View(project);
    }

    // GET: Projects/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Projects == null)
        {
            return NotFound();
        }

        var project = await DbContext.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        ViewData["GroupId"] = new SelectList(DbContext.Groups, "Id", "Name", project.GroupId);
        return View(project);
    }

    // POST: Projects/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,GroupId,Id,CreateDate")] Project project)
    {
        if (id != project.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(project);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
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
        ViewData["GroupId"] = new SelectList(DbContext.Groups, "Id", "Name", project.GroupId);
        return View(project);
    }

    // GET: Projects/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Projects == null)
        {
            return NotFound();
        }

        var project = await DbContext.Projects
            .Include(p => p.Group)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Projects == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
        }
        var project = await DbContext.Projects.FindAsync(id);
        if (project != null)
        {
            DbContext.Projects.Remove(project);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProjectExists(Guid id)
    {
        return DbContext.Projects.Any(e => e.Id == id);
    }
}
