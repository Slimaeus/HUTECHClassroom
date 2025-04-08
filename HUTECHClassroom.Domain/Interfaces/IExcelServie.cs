namespace HUTECHClassroom.Domain.Interfaces;

public interface IExcelService
{
    List<T> ReadExcelFileIgnoreColumnNames<T>(Stream stream, string? sheetName) where T : class, new();
    List<T> ReadExcelFileWithColumnNames<T>(Stream stream, string? sheetName) where T : class, new();
    List<T> ReadExcelFileWithoutColumnNames<T>(Stream stream, string? sheetName) where T : class, new();
    byte[] ExportToExcel<T>(IEnumerable<T> data, IEnumerable<string> propertyNames);
}
