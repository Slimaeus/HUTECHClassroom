using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.Web.Controllers;

public class BaseEntityController<T> : Controller
    where T : class, new()
{
    protected ApplicationDbContext DbContext => HttpContext.RequestServices.GetService<ApplicationDbContext>();
    protected IExcelServie ExcelServie => HttpContext.RequestServices.GetService<IExcelServie>();
    public IActionResult Import()
    {
        return View("~/Views/BaseEntity/Import.cshtml");
    }
    [HttpPost]
    public async Task<IActionResult> ImportAsync(IFormFile excelFile)
    {
        if (excelFile == null || excelFile.Length == 0)
        {
            ViewBag.Error = "Please select a file to upload.";
            return View("~/Views/BaseEntity/Import.cshtml");
        }

        if (!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Please select an Excel file (.xlsx).";
            return View("~/Views/BaseEntity/Import.cshtml");
        }

        var entities = ExcelServie.ReadExcelFileWithColumnNames<T>(excelFile.OpenReadStream(), null);
        // Do something with the imported people data, such as saving to a database
        await DbContext.AddRangeAsync(entities);
        await DbContext.SaveChangesAsync();

        ViewBag.Success = $"Successfully imported {entities.Count} rows.";
        return RedirectToAction("Index");
    }
}
