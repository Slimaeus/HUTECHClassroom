using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(DeanOrTrainingOfficePolicy)]
public sealed class AnswersController : BaseEntityController<Answer>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Answers
            .Include(x => x.User)
            .Include(x => x.Exercise)
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

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

    public IActionResult Create()
    {
        ViewData["ExerciseId"] = new SelectList(DbContext.Exercises, "Id", "Title");
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName");
        return View();
    }

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
