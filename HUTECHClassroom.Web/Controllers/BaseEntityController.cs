using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using HUTECHClassroom.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HUTECHClassroom.Web.Controllers;

public class BaseEntityController<T> : Controller
    where T : class, new()
{
    protected ApplicationDbContext DbContext => HttpContext.RequestServices.GetService<ApplicationDbContext>();
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
    public IActionResult Export()
    {
        Type type = typeof(T);
        PropertyInfo[] propertyInfos = type.GetProperties();

        var data = new List<T>();
        var propertyNames = propertyInfos.Where(x => x.Name != "Id" && x.Name != "CreateDate").Select(x => x.Name);
        var excelData = ExcelService.ExportToExcel(data, propertyNames);

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{type.Name}Sample.xlsx");
    }
}
