using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

public class CommentsController : BaseEntityController<Comment>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Comments
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Comments == null)
        {
            return NotFound();
        }

        var comment = await DbContext.Comments
            .Include(c => c.Post)
            .Include(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    public IActionResult Create()
    {
        ViewData["PostId"] = new SelectList(DbContext.Posts, "Id", "Content");
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Content,PostId,UserId,Id,CreateDate")] Comment comment)
    {
        if (ModelState.IsValid)
        {
            comment.Id = Guid.NewGuid();
            DbContext.Add(comment);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PostId"] = new SelectList(DbContext.Posts, "Id", "Content", comment.PostId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", comment.UserId);
        return View(comment);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Comments == null)
        {
            return NotFound();
        }

        var comment = await DbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        ViewData["PostId"] = new SelectList(DbContext.Posts, "Id", "Content", comment.PostId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", comment.UserId);
        return View(comment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Content,PostId,UserId,Id,CreateDate")] Comment comment)
    {
        if (id != comment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(comment);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(comment.Id))
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
        ViewData["PostId"] = new SelectList(DbContext.Posts, "Id", "Content", comment.PostId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", comment.UserId);
        return View(comment);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Comments == null)
        {
            return NotFound();
        }

        var comment = await DbContext.Comments
            .Include(c => c.Post)
            .Include(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Comments == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Comments'  is null.");
        }
        var comment = await DbContext.Comments.FindAsync(id);
        if (comment != null)
        {
            DbContext.Comments.Remove(comment);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CommentExists(Guid id)
    {
        return DbContext.Comments.Any(e => e.Id == id);
    }
}
