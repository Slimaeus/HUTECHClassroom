using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class AnswersController : Controller
{
    private readonly ApplicationDbContext _context;

    public AnswersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Answers
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Answers.Include(a => a.Exercise).Include(a => a.User);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Answers/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Answers == null)
        {
            return NotFound();
        }

        var answer = await _context.Answers
            .Include(a => a.Exercise)
            .Include(a => a.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (answer == null)
        {
            return NotFound();
        }

        return View(answer);
    }

    // GET: Answers/Create
    public IActionResult Create()
    {
        ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Instruction");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: Answers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Description,Link,Score,UserId,ExerciseId,Id,CreateDate")] Answer answer)
    {
        if (ModelState.IsValid)
        {
            answer.Id = Guid.NewGuid();
            _context.Add(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Instruction", answer.ExerciseId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", answer.UserId);
        return View(answer);
    }

    // GET: Answers/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Answers == null)
        {
            return NotFound();
        }

        var answer = await _context.Answers.FindAsync(id);
        if (answer == null)
        {
            return NotFound();
        }
        ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Instruction", answer.ExerciseId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", answer.UserId);
        return View(answer);
    }

    // POST: Answers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Description,Link,Score,UserId,ExerciseId,Id,CreateDate")] Answer answer)
    {
        if (id != answer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(answer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(answer.Id))
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
        ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Instruction", answer.ExerciseId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", answer.UserId);
        return View(answer);
    }

    // GET: Answers/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Answers == null)
        {
            return NotFound();
        }

        var answer = await _context.Answers
            .Include(a => a.Exercise)
            .Include(a => a.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (answer == null)
        {
            return NotFound();
        }

        return View(answer);
    }

    // POST: Answers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Answers == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Answers'  is null.");
        }
        var answer = await _context.Answers.FindAsync(id);
        if (answer != null)
        {
            _context.Answers.Remove(answer);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AnswerExists(Guid id)
    {
        return _context.Answers.Any(e => e.Id == id);
    }
}
