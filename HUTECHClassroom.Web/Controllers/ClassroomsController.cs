using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class ClassroomsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClassroomsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Classrooms
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Classrooms.Include(c => c.Faculty).Include(c => c.Lecturer);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Classrooms/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Classrooms == null)
        {
            return NotFound();
        }

        var classroom = await _context.Classrooms
            .Include(c => c.Faculty)
            .Include(c => c.Lecturer)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (classroom == null)
        {
            return NotFound();
        }

        return View(classroom);
    }

    // GET: Classrooms/Create
    public IActionResult Create()
    {
        ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
        ViewData["LecturerId"] = new SelectList(_context.Users, "Id", "UserName");
        return View();
    }

    // POST: Classrooms/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Topic,Room,Description,LecturerId,FacultyId,Id,CreateDate")] Classroom classroom)
    {
        if (ModelState.IsValid)
        {
            classroom.Id = Guid.NewGuid();
            _context.Add(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", classroom.FacultyId);
        ViewData["LecturerId"] = new SelectList(_context.Users, "Id", "UserName", classroom.LecturerId);
        return View(classroom);
    }

    // GET: Classrooms/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Classrooms == null)
        {
            return NotFound();
        }

        var classroom = await _context.Classrooms.FindAsync(id);
        if (classroom == null)
        {
            return NotFound();
        }
        ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", classroom.FacultyId);
        ViewData["LecturerId"] = new SelectList(_context.Users, "Id", "UserName", classroom.LecturerId);
        return View(classroom);
    }

    // POST: Classrooms/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Topic,Room,Description,LecturerId,FacultyId,Id,CreateDate")] Classroom classroom)
    {
        if (id != classroom.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(classroom);
                await _context.SaveChangesAsync();
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
        ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", classroom.FacultyId);
        ViewData["LecturerId"] = new SelectList(_context.Users, "Id", "UserName", classroom.LecturerId);
        return View(classroom);
    }

    // GET: Classrooms/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Classrooms == null)
        {
            return NotFound();
        }

        var classroom = await _context.Classrooms
            .Include(c => c.Faculty)
            .Include(c => c.Lecturer)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (classroom == null)
        {
            return NotFound();
        }

        return View(classroom);
    }

    // POST: Classrooms/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Classrooms == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Classrooms'  is null.");
        }
        var classroom = await _context.Classrooms.FindAsync(id);
        if (classroom != null)
        {
            _context.Classrooms.Remove(classroom);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClassroomExists(Guid id)
    {
        return _context.Classrooms.Any(e => e.Id == id);
    }
}
