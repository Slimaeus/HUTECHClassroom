using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class GroupsController : Controller
{
    private readonly ApplicationDbContext _context;

    public GroupsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Groups
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Groups.Include(g => g.Classroom).Include(g => g.Leader);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Groups/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Groups == null)
        {
            return NotFound();
        }

        var group = await _context.Groups
            .Include(g => g.Classroom)
            .Include(g => g.Leader)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (group == null)
        {
            return NotFound();
        }

        return View(group);
    }

    // GET: Groups/Create
    public IActionResult Create()
    {
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title");
        ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName");
        return View();
    }

    // POST: Groups/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,LeaderId,ClassroomId,Id,CreateDate")] Group group)
    {
        if (ModelState.IsValid)
        {
            group.Id = Guid.NewGuid();
            _context.Add(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title", group.ClassroomId);
        ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName", group.LeaderId);
        return View(group);
    }

    // GET: Groups/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Groups == null)
        {
            return NotFound();
        }

        var group = await _context.Groups.FindAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title", group.ClassroomId);
        ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName", group.LeaderId);
        return View(group);
    }

    // POST: Groups/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,LeaderId,ClassroomId,Id,CreateDate")] Group group)
    {
        if (id != group.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(group);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.Id))
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
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title", group.ClassroomId);
        ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName", group.LeaderId);
        return View(group);
    }

    // GET: Groups/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Groups == null)
        {
            return NotFound();
        }

        var group = await _context.Groups
            .Include(g => g.Classroom)
            .Include(g => g.Leader)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (group == null)
        {
            return NotFound();
        }

        return View(group);
    }

    // POST: Groups/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Groups == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
        }
        var group = await _context.Groups.FindAsync(id);
        if (group != null)
        {
            _context.Groups.Remove(group);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupExists(Guid id)
    {
        return _context.Groups.Any(e => e.Id == id);
    }
}
