using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

public class PostsController : BaseEntityController<Post>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Posts
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    // GET: Posts/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Posts == null)
        {
            return NotFound();
        }

        var post = await DbContext.Posts
            .Include(p => p.Classroom)
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // GET: Posts/Create
    public IActionResult Create()
    {
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title");
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName");
        return View();
    }

    // POST: Posts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Content,Link,ClassroomId,UserId,Id,CreateDate")] Post post)
    {
        if (ModelState.IsValid)
        {
            post.Id = Guid.NewGuid();
            DbContext.Add(post);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", post.ClassroomId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", post.UserId);
        return View(post);
    }

    // GET: Posts/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Posts == null)
        {
            return NotFound();
        }

        var post = await DbContext.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", post.ClassroomId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", post.UserId);
        return View(post);
    }

    // POST: Posts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Content,Link,ClassroomId,UserId,Id,CreateDate")] Post post)
    {
        if (id != post.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                DbContext.Update(post);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.Id))
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
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", post.ClassroomId);
        ViewData["UserId"] = new SelectList(DbContext.Users, "Id", "UserName", post.UserId);
        return View(post);
    }

    // GET: Posts/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Posts == null)
        {
            return NotFound();
        }

        var post = await DbContext.Posts
            .Include(p => p.Classroom)
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // POST: Posts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Posts == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }
        var post = await DbContext.Posts.FindAsync(id);
        if (post != null)
        {
            DbContext.Posts.Remove(post);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PostExists(Guid id)
    {
        return DbContext.Posts.Any(e => e.Id == id);
    }
}
