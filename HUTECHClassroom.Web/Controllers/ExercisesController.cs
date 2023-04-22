﻿using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class ExercisesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExercisesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Exercises
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Exercises.Include(e => e.Classroom);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Exercises/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Exercises == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises
            .Include(e => e.Classroom)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    // GET: Exercises/Create
    public IActionResult Create()
    {
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title");
        return View();
    }

    // POST: Exercises/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Instruction,Link,TotalScore,Deadline,Topic,Criteria,ClassroomId,Id,CreateDate")] Exercise exercise)
    {
        if (ModelState.IsValid)
        {
            exercise.Id = Guid.NewGuid();
            _context.Add(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title", exercise.ClassroomId);
        return View(exercise);
    }

    // GET: Exercises/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Exercises == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title", exercise.ClassroomId);
        return View(exercise);
    }

    // POST: Exercises/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Title,Instruction,Link,TotalScore,Deadline,Topic,Criteria,ClassroomId,Id,CreateDate")] Exercise exercise)
    {
        if (id != exercise.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(exercise);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(exercise.Id))
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
        ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Title", exercise.ClassroomId);
        return View(exercise);
    }

    // GET: Exercises/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Exercises == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercises
            .Include(e => e.Classroom)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    // POST: Exercises/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Exercises == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Exercises'  is null.");
        }
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExerciseExists(Guid id)
    {
        return _context.Exercises.Any(e => e.Id == id);
    }
}
