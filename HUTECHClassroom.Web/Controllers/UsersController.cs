using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(Roles = RoleConstants.ADMIN)]
public class UsersController : BaseEntityController<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Users
            .Include(a => a.Faculty)
            .Include(a => a.ApplicationUserRoles)
            .ThenInclude(a => a.Role)
            .OrderBy(x => x.UserName)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Users == null)
        {
            return NotFound();
        }

        var applicationUser = await DbContext.Users
            .Include(a => a.Faculty)
            .Include(a => a.ApplicationUserRoles)
            .ThenInclude(a => a.Role)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (applicationUser == null)
        {
            return NotFound();
        }
        var viewModel = new UserViewModel
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            FacultyName = applicationUser.Faculty?.Name ?? string.Empty
        };
        return View(viewModel);
    }

    public IActionResult ImportUsers()
    {
        var viewModel = new ImportUsersFromExcelViewModel();
        Type type = typeof(ImportedUserViewModel);
        PropertyInfo[] propertyInfos = type.GetProperties();
        viewModel.PropertyNames = propertyInfos.Select(x => x.Name);

        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> ImportUsers(ImportUsersFromExcelViewModel viewModel)
    {
        if (viewModel.File == null || viewModel.File.Length == 0)
        {
            ViewBag.Error = "Please select a file to upload."; Type type = typeof(ImportedUserViewModel);
            PropertyInfo[] propertyInfos = type.GetProperties();
            viewModel.PropertyNames = propertyInfos.Select(x => x.Name);
            return View(viewModel);
        }

        if (!Path.GetExtension(viewModel.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Please select an Excel file (.xlsx)."; Type type = typeof(ImportedUserViewModel);
            PropertyInfo[] propertyInfos = type.GetProperties();
            viewModel.PropertyNames = propertyInfos.Select(x => x.Name);
            return View(viewModel);
        }

        var userViewModels = ExcelService.ReadExcelFileWithColumnNames<ImportedUserViewModel>(viewModel.File.OpenReadStream(), null);
        // Map userViewModels to users
        var users = userViewModels.Select(x => Mapper.Map<ApplicationUser>(x)).ToList();
        // Do something with the imported people data, such as saving to a database
        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, "Student");
        }

        ViewBag.Success = $"Successfully imported {users.Count} users.";
        return RedirectToAction("Index");
    }
    // GET: ApplicationUsers/Create
    public IActionResult Create()
    {
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name")
            .Append(new SelectListItem("None", Guid.Empty.ToString()));
        ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                FacultyId = viewModel.FacultyId != Guid.Empty ? viewModel.FacultyId : null
            };
            await _userManager.CreateAsync(applicationUser, applicationUser.UserName);
            await _userManager.AddToRoleAsync(applicationUser, viewModel.RoleName);
            return RedirectToAction(nameof(Index));
        }
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", viewModel.FacultyId).Append(new SelectListItem("None", Guid.Empty.ToString()));
        ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name", viewModel.RoleName);
        return View(viewModel);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Users == null)
        {
            return NotFound();
        }

        var applicationUser = await DbContext.Users.FindAsync(id);
        if (applicationUser == null)
        {
            return NotFound();
        }
        var viewModel = new EditUserViewModel
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            FacultyId = applicationUser.FacultyId ?? Guid.Empty
        };
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", viewModel.FacultyId).Append(new SelectListItem("None", Guid.Empty.ToString()));
        ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name");
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EditUserViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var applicationUser = await DbContext.Users.FindAsync(viewModel.Id);
            applicationUser.Id = viewModel.Id;
            applicationUser.UserName = viewModel.UserName;
            applicationUser.Email = viewModel.Email;
            applicationUser.FirstName = viewModel.FirstName;
            applicationUser.LastName = viewModel.LastName;
            applicationUser.FacultyId = viewModel.FacultyId != Guid.Empty ? viewModel.FacultyId : null;

            try
            {
                DbContext.Update(applicationUser);
                if (!await _userManager.IsInRoleAsync(applicationUser, viewModel.RoleName))
                {
                    await _userManager.AddToRoleAsync(applicationUser, viewModel.RoleName);
                }
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(applicationUser.Id))
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
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", viewModel.FacultyId).Append(new SelectListItem("None", Guid.Empty.ToString()));
        ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name", viewModel.RoleName);
        return View(viewModel);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Users == null)
        {
            return NotFound();
        }

        var applicationUser = await DbContext.Users
            .Include(a => a.Faculty)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (applicationUser == null)
        {
            return NotFound();
        }
        var viewModel = new UserViewModel
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            FacultyName = applicationUser.Faculty?.Name ?? string.Empty
        };
        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (DbContext.Users == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }
        var applicationUser = await DbContext.Users.FindAsync(id);
        if (applicationUser != null)
        {
            DbContext.Users.Remove(applicationUser);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ApplicationUserExists(Guid id)
    {
        return DbContext.Users.Any(e => e.Id == id);
    }
}
