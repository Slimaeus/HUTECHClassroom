using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using HUTECHClassroom.Web.Models;
using HUTECHClassroom.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HUTECHClassroom.Web.Controllers;

[Authorize]
public class BaseEntityController<T> : Controller
    where T : class, new()
{
    protected ApplicationDbContext DbContext => HttpContext.RequestServices.GetService<ApplicationDbContext>();
    protected UserManager<ApplicationUser> UserManager => HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
    protected IExcelServie ExcelService => HttpContext.RequestServices.GetService<IExcelServie>();
    public IActionResult Import()
    {
        var viewModel = new ImportEntitiesFromExcelViewModel();
        Type type = typeof(T);
        PropertyInfo[] propertyInfos = type.GetProperties();
        viewModel.PropertyNames = propertyInfos.Select(x => x.Name);

        return View("~/Views/BaseEntity/Import.cshtml", viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportAsync(ImportEntitiesFromExcelViewModel viewModel)
    {
        if (viewModel.File == null || viewModel.File.Length == 0)
        {
            ViewBag.Error = "Please select a file to upload.";
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            viewModel.PropertyNames = propertyInfos.Select(x => x.Name);
            return View("~/Views/BaseEntity/Import.cshtml", viewModel);
        }

        if (!Path.GetExtension(viewModel.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Please select an Excel file (.xlsx).";
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            viewModel.PropertyNames = propertyInfos.Select(x => x.Name);
            return View("~/Views/BaseEntity/Import.cshtml", viewModel);
        }

        var entities = ExcelService.ReadExcelFileWithColumnNames<T>(viewModel.File.OpenReadStream(), null);
        // Do something with the imported people data, such as saving to a database
        await DbContext.AddRangeAsync(entities);
        await DbContext.SaveChangesAsync();

        ViewBag.Success = $"Successfully imported {entities.Count} rows.";
        return RedirectToAction("Index");
    }
    public IActionResult ExportSample()
    {
        Type type = typeof(T);
        PropertyInfo[] propertyInfos = type.GetProperties();

        var data = new List<T>();
        var propertyNames = propertyInfos
            .Where(x => x.Name != "Id"
            && x.Name != "CreateDate"
            && x.CanRead
            && (x.PropertyType.IsPrimitive
                || x.PropertyType.IsEnum
                || x.PropertyType.Equals(typeof(DateTime))
                || x.PropertyType.Equals(typeof(Guid))
                || x.PropertyType.Equals(typeof(string))
            ))
            .Select(x => x.Name);

        var excelData = ExcelService.ExportToExcel(data, propertyNames);

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{type.Name}Sample.xlsx");
    }
    public IActionResult ExportUsers()
    {
        Type type = typeof(ImportedUserViewModel);
        PropertyInfo[] propertyInfos = type.GetProperties();

        var data = new List<ImportedUserViewModel>();
        var propertyNames = propertyInfos
            .Where(x => x.Name != "Id"
            && x.Name != "CreateDate"
            && x.CanRead
            && (x.PropertyType.IsPrimitive
                || x.PropertyType.IsEnum
                || x.PropertyType.Equals(typeof(DateTime))
                || x.PropertyType.Equals(typeof(Guid))
                || x.PropertyType.Equals(typeof(string))
            ))
            .Select(x => x.Name);

        var excelData = ExcelService.ExportToExcel(data, propertyNames);

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportUsersSample.xlsx");
    }
    public IActionResult Export()
    {
        Type type = typeof(T);
        PropertyInfo[] propertyInfos = type.GetProperties();

        var data = DbContext.Set<T>();
        var propertyNames = propertyInfos
            .Where(x => x.Name != "CreateDate"
            && x.CanRead
            && (x.PropertyType.IsPrimitive
                || x.PropertyType.IsEnum
                || x.PropertyType.Equals(typeof(DateTime))
                || x.PropertyType.Equals(typeof(Guid))
                || x.PropertyType.Equals(typeof(string))
            ))
            .Select(x => x.Name);
        var excelData = ExcelService.ExportToExcel(data, propertyNames);
        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Import{typeof(T).Name}Sample.xlsx");
    }
}
