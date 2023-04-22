using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class CommentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CommentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Comments
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Comments.Include(c => c.Post).Include(c => c.User);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Comments/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Comments == null)
        {
            return NotFound();
        }

        var comment = await _context.Comments
            .Include(c => c.Post)
            .Include(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    // GET: Comments/Create
    public IActionResult Create()
    {
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: Comments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Content,PostId,UserId,Id,CreateDate")] Comment comment)
    {
        if (ModelState.IsValid)
        {
            comment.Id = Guid.NewGuid();
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
        return View(comment);
    }

    // GET: Comments/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Comments == null)
        {
            return NotFound();
        }

        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
        return View(comment);
    }

    // POST: Comments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                _context.Update(comment);
                await _context.SaveChangesAsync();
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
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
        return View(comment);
    }

    // GET: Comments/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Comments == null)
        {
            return NotFound();
        }

        var comment = await _context.Comments
            .Include(c => c.Post)
            .Include(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    // POST: Comments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Comments == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Comments'  is null.");
        }
        var comment = await _context.Comments.FindAsync(id);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CommentExists(Guid id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}
