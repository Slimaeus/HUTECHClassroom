using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Web.ViewModels.Majors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using X.PagedList;

namespace HUTECHClassroom.Web.Controllers;

[Authorize(DeanOrTrainingOfficePolicy)]
public class MajorsController : BaseEntityController<Major>
{
    public IActionResult Index(int? page, int? size)
    {
        int pageIndex = page ?? 1;
        int pageSize = size ?? 5;
        return View(DbContext.Majors
            .OrderByDescending(x => x.CreateDate)
            .ToPagedList(pageIndex, pageSize));
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }

        var major = await DbContext.Majors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (major == null)
        {
            return NotFound();
        }

        return View(major);
    }

    protected override async Task<IEnumerable<Major>> GetExistingEntities(IEnumerable<Major> majors)
    {
        return await DbContext.Majors
            .Where(m => majors.Select(x => x.Code).Contains(m.Code))
            .ToListAsync();
    }

    protected override IEnumerable<Major> GetNewEntities(IEnumerable<Major> majors, IEnumerable<Major> existingMajors)
    {
        return majors.Where(m => !existingMajors.Select(em => em.Code).Contains(m.Code));
    }

    public async Task<IActionResult> ImportSubjects(Guid? id)
    {
        if (id == null)
            return View("Index");
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }
        var major = await DbContext.Majors.FindAsync(id);
        if (major == null)
        {
            return NotFound();
        }
        var viewModel = new ImportSubjectsToMajorViewModel
        {
            MajorId = major.Id,
            MajorTitle = major.Title
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportSubjects(ImportSubjectsToMajorViewModel viewModel)
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

        var subjects = ExcelService.ReadExcelFileWithColumnNames<Subject>(viewModel.File.OpenReadStream(), null);

        var existingSubjects = await DbContext.Subjects
            .Where(u => subjects.Select(x => x.Code).Contains(u.Code))
            .ToListAsync();

        var newSubjects = subjects
            .Where(vm => !existingSubjects.Select(x => x.Code).Contains(vm.Code))
            .ToList();

        foreach (var subject in existingSubjects)
        {
            subject.MajorId = viewModel.MajorId;
            DbContext.Entry(subject).State = EntityState.Modified;
            DbContext.Update(subject);
        }

        foreach (var subject in newSubjects)
        {
            subject.MajorId = viewModel.MajorId;
            await DbContext.Subjects.AddAsync(subject);
        }

        int count = await DbContext.SaveChangesAsync().ConfigureAwait(false);

        ViewBag.Success = $"Successfully imported and updated {count} rows.";
        return RedirectToAction("Index");
    }

    public IActionResult ExportSubjects()
    {
        Type type = typeof(Subject);
        PropertyInfo[] propertyInfos = type.GetProperties();

        var data = new List<Subject>();
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

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportSubjectsSample.xlsx");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Code,Title,TotalCredits,NonComulativeCredits,Id,CreateDate")] Major major)
    {
        if (ModelState.IsValid)
        {
            if (await DbContext.Majors.AnyAsync(x => x.Code.ToUpper().Equals(major.Code.ToUpper())))
            {
                ModelState.AddModelError("Code", "Code taken");
                return View(major);
            }
            DbContext.Add(major);
            await DbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(major);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }

        var major = await DbContext.Majors.FindAsync(id);
        if (major == null)
        {
            return NotFound();
        }
        return View(major);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid? id, [Bind("Code,Title,TotalCredits,NonComulativeCredits,Id,CreateDate")] Major major)
    {
        if (id != major.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (await DbContext.Majors.AnyAsync(x => x.Code.ToUpper().Equals(major.Code.ToUpper())))
            {
                ModelState.AddModelError("Code", "Code taken");
                return View(major);
            }
            try
            {
                DbContext.Update(major);
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorExists(major.Id))
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
        return View(major);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || DbContext.Majors == null)
        {
            return NotFound();
        }

        var major = await DbContext.Majors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (major == null)
        {
            return NotFound();
        }

        return View(major);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
        if (DbContext.Majors == null)
        {
            return Problem("Entity set 'ApplicationDbDbContext.Majors'  is null.");
        }
        var major = await DbContext.Majors.FindAsync(id);
        if (major != null)
        {
            DbContext.Majors.Remove(major);
        }

        await DbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MajorExists(Guid? id)
    {
        return DbContext.Majors.Any(e => e.Id == id);
    }
}
