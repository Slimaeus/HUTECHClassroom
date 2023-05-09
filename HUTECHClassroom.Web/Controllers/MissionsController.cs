using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.Missions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

public class MissionsController : BaseEntityController<Mission>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Missions
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    // GET: Missions/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Missions == null)
        {
            return NotFound();
        }

        var mission = await DbContext.Missions
            .Include(m => m.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (mission == null)
        {
            return NotFound();
        }

        return View(mission);
    }

    public async Task<IActionResult> ImportMissionUsers(Guid? id)
    {
        if (id == null)
            return View("Index");
        if (id == null || DbContext.Missions == null)
        {
            return NotFound();
        }
        var mission = await DbContext.Missions.FindAsync(id);
        if (mission == null)
        {
            return NotFound();
        }
        var viewModel = new ImportUsersToMissionViewModel
        {
            MissionId = mission.Id,
            MissionTitle = mission.Title
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportMissionUsers(ImportUsersToMissionViewModel viewModel)
    {
        if (viewModel.File == null || viewModel.File.Length == 0)
        {
            ViewBag.Error = "Please select a file to upload.";
            return View(viewModel);
        }

        if (!Path.GetExtension(viewModel.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Please select an Excel file (.xlsx).";
            return View(viewModel);
        }

        var users = ExcelService.ReadExcelFileWithColumnNames<ApplicationUser>(viewModel.File.OpenReadStream(), null);
        // Do something with the imported people data, such as saving to a database
        var results = new List<IdentityResult>();
        foreach (var user in users)
        {
            results.Add(await UserManager.CreateAsync(user, user.UserName));
        }

        var mission = await DbContext.Missions
            .Include(c => c.MissionUsers)
            .SingleOrDefaultAsync(c => c.Id == viewModel.MissionId);

        if (mission == null)
        {
            return NotFound();
        }

        mission.MissionUsers.AddRange(
            users.Select(user => new MissionUser { User = user })
        );

        await DbContext.SaveChangesAsync();

        ViewBag.Success = $"Successfully imported {results.Count(x => x.Succeeded)} rows.";
        return RedirectToAction("Index");
    }

    // GET: Missions/Create
    public IActionResult Create()
    {
        ViewData["ProjectId"] = new SelectList(DbContext.Projects, "Id", "Name");
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
            DbContext.Add(mission);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProjectId"] = new SelectList(DbContext.Projects, "Id", "Name", mission.ProjectId);
        return View(mission);
    }

    // GET: Missions/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Missions == null)
        {
            return NotFound();
        }

        var mission = await DbContext.Missions.FindAsync(id);
        if (mission == null)
        {
            return NotFound();
        }
        ViewData["ProjectId"] = new SelectList(DbContext.Projects, "Id", "Name", mission.ProjectId);
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
                DbContext.Update(mission);
                await DbContext.SaveChangesAsync();
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
        ViewData["ProjectId"] = new SelectList(DbContext.Projects, "Id", "Name", mission.ProjectId);
        return View(mission);
    }

    // GET: Missions/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Missions == null)
        {
            return NotFound();
        }

        var mission = await DbContext.Missions
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
        if (DbContext.Missions == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Missions'  is null.");
        }
        var mission = await DbContext.Missions.FindAsync(id);
        if (mission != null)
        {
            DbContext.Missions.Remove(mission);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MissionExists(Guid id)
    {
        return DbContext.Missions.Any(e => e.Id == id);
    }
}
