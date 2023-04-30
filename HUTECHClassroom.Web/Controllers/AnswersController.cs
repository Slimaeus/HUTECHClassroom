using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class AnswersController : BaseEntityController<Answer>
{
    // GET: Answers
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = DbContext.Answers.Include(a => a.Exercise).Include(a => a.User);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Answers/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Answers == null)
        {
            return NotFound();
        }

        var answer = await DbContext.Answers
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
        ViewData["ExerciseId"] = new SelectList(DbContext.Exercises, "Id", "Title");
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName");
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
            DbContext.Add(answer);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ExerciseId"] = new SelectList(DbContext.Exercises, "Id", "Title", answer.ExerciseId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", answer.UserId);
        return View(answer);
    }

    // GET: Answers/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Answers == null)
        {
            return NotFound();
        }

        var answer = await DbContext.Answers.FindAsync(id);
        if (answer == null)
        {
            return NotFound();
        }
        ViewData["ExerciseId"] = new SelectList(DbContext.Exercises, "Id", "Title", answer.ExerciseId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", answer.UserId);
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
                DbContext.Update(answer);
                await DbContext.SaveChangesAsync();
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
        ViewData["ExerciseId"] = new SelectList(DbContext.Exercises, "Id", "Title", answer.ExerciseId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", answer.UserId);
        return View(answer);
    }

    // GET: Answers/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Answers == null)
        {
            return NotFound();
        }

        var answer = await DbContext.Answers
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
        if (DbContext.Answers == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Answers'  is null.");
        }
        var answer = await DbContext.Answers.FindAsync(id);
        if (answer != null)
        {
            DbContext.Answers.Remove(answer);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AnswerExists(Guid id)
    {
        return DbContext.Answers.Any(e => e.Id == id);
    }
}
