using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.Models;
using HUTECHClassroom.Web.ViewModels.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Web.Controllers;

public class ApplicationUsersController : BaseEntityController<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    // GET: ApplicationUsers
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = DbContext.Users.Include(a => a.Faculty);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: ApplicationUsers/Details/5
    public async Task<IActionResult> Details(Guid? id)
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

    public IActionResult ImportUsers()
    {
        var viewModel = new ImportUsersFromExcelViewModel();
        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> ImportUsers(ImportUsersFromExcelViewModel viewModel)
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

        var userViewModels = ExcelService.ReadExcelFileWithColumnNames<ImportedUserViewModel>(viewModel.File.OpenReadStream(), null);
        // Map userViewModels to users
        var users = userViewModels.Select(x => new ApplicationUser
        {
            UserName = x.UserName,
            Email = "users@gmail.com",
            FirstName = x.FirstName,
            LastName = x.LastName,
            FacultyId = x.FacultyId != Guid.Empty ? x.FacultyId : null
        }).ToList();
        // Do something with the imported people data, such as saving to a database
        foreach (var user in users)
        {
            await _userManager.CreateAsync(user, user.UserName);
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
        return View();
    }

    // POST: ApplicationUsers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return RedirectToAction(nameof(Index));
        }
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", viewModel.FacultyId).Append(new SelectListItem("None", Guid.Empty.ToString()));
        return View(viewModel);
    }

    // GET: ApplicationUsers/Edit/5
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
        return View(viewModel);
    }

    // POST: ApplicationUsers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        return View(viewModel);
    }

    // GET: ApplicationUsers/Delete/5
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

    // POST: ApplicationUsers/Delete/5
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
