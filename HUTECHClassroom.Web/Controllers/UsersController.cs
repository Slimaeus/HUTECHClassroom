﻿using AutoMapper.QueryableExtensions;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(Roles = $"{RoleConstants.Dean},{RoleConstants.TrainingOffice},{RoleConstants.Administrator}")]
public sealed class UsersController : BaseEntityController<ApplicationUser>
{
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
        if (applicationUser is null || applicationUser.UserName is null || applicationUser.Email is null)
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
        ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name");
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
            ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name");
            return View(viewModel);
        }

        if (!Path.GetExtension(viewModel.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Please select an Excel file (.xlsx)."; Type type = typeof(ImportedUserViewModel);
            PropertyInfo[] propertyInfos = type.GetProperties();
            viewModel.PropertyNames = propertyInfos.Select(x => x.Name);
            ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name");
            return View(viewModel);
        }

        var userViewModels = ExcelService.ReadExcelFileWithColumnNames<ImportedUserViewModel>(viewModel.File.OpenReadStream(), null);

        var existingUsers = await DbContext.Users
            .Include(u => u.ApplicationUserRoles)
            .ThenInclude(ar => ar.Role)
            .Where(u => userViewModels.Select(x => x.UserName).Contains(u.UserName))
            .ToListAsync();

        var newUsers = userViewModels
            .Where(vm => !existingUsers.Select(x => x.UserName).Contains(vm.UserName))
            .AsQueryable()
            .ProjectTo<ApplicationUser>(Mapper.ConfigurationProvider)
            .ToList();

        foreach (var user in existingUsers)
        {
            DbContext.Entry(user).State = EntityState.Modified;
            foreach (var applicationUserRole in user.ApplicationUserRoles)
            {
                if (applicationUserRole.Role is { Name: { } })
                    await UserManager.RemoveFromRoleAsync(user, applicationUserRole.Role.Name).ConfigureAwait(false);
            }
            await UserManager.AddToRoleAsync(user, viewModel.RoleName).ConfigureAwait(false);
        }

        foreach (var user in newUsers)
        {
            if (string.IsNullOrEmpty(user.UserName)) continue;
            var result = await UserManager.CreateAsync(user, user.UserName).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user, viewModel.RoleName).ConfigureAwait(false);
            }
        }

        ViewBag.Success = $"Successfully imported and updated {userViewModels.Count} users.";
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
        if (ModelState.IsValid && viewModel.RoleName is { })
        {
            var applicationUser = new ApplicationUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                FacultyId = viewModel.FacultyId != Guid.Empty ? viewModel.FacultyId : null
            };
            await UserManager.CreateAsync(applicationUser, applicationUser.UserName);
            await UserManager.AddToRoleAsync(applicationUser, viewModel.RoleName);
            return RedirectToAction(nameof(Index));
        }
        ViewData["FacultyId"] = new SelectList(DbContext.Faculties, "Id", "Name", viewModel.FacultyId).Append(new SelectListItem("None", Guid.Empty.ToString()));
        ViewData["RoleName"] = new SelectList(DbContext.Roles, "Name", "Name", viewModel.RoleName);
        return View(viewModel);
    }
    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Users == null)
        {
            return NotFound();
        }

        var applicationUser = await DbContext.Users.FindAsync(id);
        if (applicationUser is null || applicationUser.UserName is null || applicationUser.Email is null)
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
    [Authorize(Roles = RoleConstants.Administrator)]
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
            var applicationUser = await DbContext.Users
                .Include(u => u.ApplicationUserRoles)
                .ThenInclude(ar => ar.Role)
                .SingleOrDefaultAsync(u => u.Id == viewModel.Id);
            if (applicationUser is null)
            {
                return NotFound();
            }
            applicationUser.Id = viewModel.Id;
            applicationUser.UserName = viewModel.UserName;
            applicationUser.Email = viewModel.Email;
            applicationUser.FirstName = viewModel.FirstName ?? applicationUser.FirstName;
            applicationUser.LastName = viewModel.LastName ?? applicationUser.LastName;
            applicationUser.FacultyId = viewModel.FacultyId != Guid.Empty ? viewModel.FacultyId : null;

            try
            {
                DbContext.Update(applicationUser);
                foreach (var applicationUserRole in applicationUser.ApplicationUserRoles)
                {
                    if (applicationUserRole.Role is { Name: { } })
                        await UserManager.RemoveFromRoleAsync(applicationUser, applicationUserRole.Role.Name);
                }
                await UserManager.AddToRoleAsync(applicationUser, viewModel.RoleName ?? RoleConstants.Student);
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
    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Users == null)
        {
            return NotFound();
        }

        var applicationUser = await DbContext.Users
            .Include(a => a.Faculty)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (applicationUser is null || applicationUser.UserName is null || applicationUser.Email is null)
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
    [Authorize(Roles = RoleConstants.Administrator)]
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
