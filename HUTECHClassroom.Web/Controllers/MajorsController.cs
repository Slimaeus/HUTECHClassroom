using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

public class MajorsController : BaseEntityController<Major>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Majors
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }

        var major = await DbContext.Majors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (major == null)
        {
            return NotFound();
        }

        return View(major);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,TotalCredits,NonComulativeCredits,Id,CreateDate")] Major major)
    {
        if (ModelState.IsValid)
        {
            DbContext.Add(major);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(major);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }

        var major = await DbContext.Majors.FindAsync(id);
        if (major == null)
        {
            return NotFound();
        }
        return View(major);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Title,TotalCredits,NonComulativeCredits,Id,CreateDate")] Major major)
    {
        if (id != major.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(major);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorExists(major.Id))
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
        return View(major);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }

        var major = await DbContext.Majors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (major == null)
        {
            return NotFound();
        }

        return View(major);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (DbContext.Majors == null)
        {
            return Problem("Entity set 'ApplicationDbDbContext.Majors'  is null.");
        }
        var major = await DbContext.Majors.FindAsync(id);
        if (major != null)
        {
            DbContext.Majors.Remove(major);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MajorExists(string id)
    {
        return DbContext.Majors.Any(e => e.Id == id);
    }
}
