using AutoMapper.QueryableExtensions;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.ApplicationUsers;
using HUTECHClassroom.Web.ViewModels.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(DeanOrTrainingOfficePolicy)]
public sealed class GroupsController : BaseEntityController<Group>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Groups
            .Include(g => g.Classroom)
            .Include(g => g.Leader)
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Groups == null)
        {
            return NotFound();
        }

        var group = await DbContext.Groups
            .Include(g => g.Classroom)
            .Include(g => g.Leader)
            .Include(g => g.GroupUsers)
            .ThenInclude(g => g.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (group == null)
        {
            return NotFound();
        }

        return View(group);
    }

    public async Task<IActionResult> ImportGroupUsers(Guid? id)
    {
        if (id == null)
            return View("Index");
        if (id == null || DbContext.Groups == null)
        {
            return NotFound();
        }
        var group = await DbContext.Groups.FindAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        var viewModel = new ImportUsersToGroupViewModel
        {
            GroupId = group.Id,
            GroupName = group.Name
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportGroupUsers(ImportUsersToGroupViewModel viewModel)
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

        var group = await DbContext.Groups
            .Include(c => c.GroupUsers)
            .SingleOrDefaultAsync(c => c.Id == viewModel.GroupId);

        if (group == null)
        {
            return NotFound();
        }

        var userViewModels = ExcelService.ReadExcelFileWithColumnNames<ImportedUserViewModel>(viewModel.File.OpenReadStream(), null);
        // Do something with the imported people data, such as saving to a database
        var dbUsers = new List<ApplicationUser>();
        var existingUsers = await DbContext.Users
            .Include(x => x.GroupUsers)
            .Where(u => userViewModels.Select(x => x.UserName).Contains(u.UserName))
            .ToListAsync();

        var newUsers = userViewModels
            .Where(vm => !existingUsers.Select(x => x.UserName).Contains(vm.UserName))
            .AsQueryable()
            .ProjectTo<ApplicationUser>(Mapper.ConfigurationProvider)
            .ToList();

        foreach (var user in newUsers)
        {
            var result = await UserManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
            if (result.Succeeded)
                await UserManager.AddToRoleAsync(user, RoleConstants.Student).ConfigureAwait(false);
            dbUsers.Add(user);
        }

        dbUsers.AddRange(existingUsers.Where(x => !x.GroupUsers.Any(cu => cu.GroupId == viewModel.GroupId)));

        var groupRole = await DbContext.GroupRoles.SingleOrDefaultAsync(x => x.Name == "Member");

        group.GroupUsers.AddRange(
            dbUsers.Select(user => new GroupUser { User = user, GroupRole = groupRole })
        );

        int count = await DbContext.SaveChangesAsync().ConfigureAwait(false);

        ViewBag.Success = $"Successfully imported and updated {count} rows.";
        return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title");
        ViewData["LeaderId"] = new SelectList(DbContext.Users, "Id", "UserName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,LeaderId,ClassroomId,Id,CreateDate")] Group group)
    {
        if (ModelState.IsValid)
        {
            group.Id = Guid.NewGuid();
            DbContext.Add(group);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", group.ClassroomId);
        ViewData["LeaderId"] = new SelectList(DbContext.Users, "Id", "UserName", group.LeaderId);
        return View(group);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Groups == null)
        {
            return NotFound();
        }

        var group = await DbContext.Groups.FindAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", group.ClassroomId);
        ViewData["LeaderId"] = new SelectList(DbContext.Users, "Id", "UserName", group.LeaderId);
        return View(group);
    }

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
                DbContext.Update(group);
                await DbContext.SaveChangesAsync();
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
        ViewData["ClassroomId"] = new SelectList(DbContext.Classrooms, "Id", "Title", group.ClassroomId);
        ViewData["LeaderId"] = new SelectList(DbContext.Users, "Id", "UserName", group.LeaderId);
        return View(group);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Groups == null)
        {
            return NotFound();
        }

        var group = await DbContext.Groups
            .Include(g => g.Classroom)
            .Include(g => g.Leader)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (group == null)
        {
            return NotFound();
        }

        return View(group);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Groups == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
        }
        var group = await DbContext.Groups.FindAsync(id);
        if (group != null)
        {
            DbContext.Groups.Remove(group);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupExists(Guid id)
    {
        return DbContext.Groups.Any(e => e.Id == id);
    }
}
