using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class MissionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public MissionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Missions
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Missions.Include(m => m.Project);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Missions/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Missions == null)
        {
            return NotFound();
        }

        var mission = await _context.Missions
            .Include(m => m.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (mission == null)
        {
            return NotFound();
        }

        return View(mission);
    }

    // GET: Missions/Create
    public IActionResult Create()
    {
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
        return View();
    }

    // POST: Missions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,IsDone,ProjectId,Id,CreateDate")] Mission mission)
    {
        if (ModelState.IsValid)
        {
            mission.Id = Guid.NewGuid();
            _context.Add(mission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", mission.ProjectId);
        return View(mission);
    }

    // GET: Missions/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Missions == null)
        {
            return NotFound();
        }

        var mission = await _context.Missions.FindAsync(id);
        if (mission == null)
        {
            return NotFound();
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", mission.ProjectId);
        return View(mission);
    }

    // POST: Missions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,IsDone,ProjectId,Id,CreateDate")] Mission mission)
    {
        if (id != mission.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(mission);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(mission.Id))
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
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", mission.ProjectId);
        return View(mission);
    }

    // GET: Missions/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Missions == null)
        {
            return NotFound();
        }

        var mission = await _context.Missions
            .Include(m => m.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (mission == null)
        {
            return NotFound();
        }

        return View(mission);
    }

    // POST: Missions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Missions == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Missions'  is null.");
        }
        var mission = await _context.Missions.FindAsync(id);
        if (mission != null)
        {
            _context.Missions.Remove(mission);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MissionExists(Guid id)
    {
        return _context.Missions.Any(e => e.Id == id);
    }
}
