using ClosedXML.Excel;
using HUTECHClassroom.Domain.Interfaces;
using Serilog;
using System.ComponentModel;

namespace HUTECHClassroom.Infrastructure.Services.Excel;

public sealed class ExcelSerive : IExcelServie
{
    public List<T> ReadExcelFileWithoutColumnNames<T>(Stream stream, string? sheetName) where T : class, new()
    {
        var workbook = new XLWorkbook(stream);
        var worksheet = sheetName != null ? workbook.Worksheet(sheetName) : workbook.Worksheet(1);

        // Get the properties of the target class
        var properties = typeof(T).GetProperties();

        // Read the data from the worksheet and map it to objects of the target class
        var rows = worksheet.RowsUsed();
        var objects = new List<T>();

        foreach (var row in rows)
        {
            var obj = new T();
            bool isRowValid = true;

            for (int i = 0; i < properties.Length; i++)
            {
                var cellValue = row.Cell(i + 1).Value.ToString();

                if (!string.IsNullOrEmpty(cellValue))
                {
                    try
                    {
                        // Convert the cell value to the target property type
                        var convertedValue = TypeDescriptor.GetConverter(properties[i].PropertyType).ConvertFromInvariantString(cellValue);
                        //var convertedValue = Convert.ChangeType(cellValue, property.PropertyType);

                        // Set the value of the target property
                        properties[i].SetValue(obj, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "This is Excel Service error");
                        isRowValid = false;
                    }
                }
                else
                {
                    isRowValid = false;
                    break;
                }
            }

            if (isRowValid)
                objects.Add(obj);
        }

        return objects;
    }
    public List<T> ReadExcelFileIgnoreColumnNames<T>(Stream stream, string? sheetName) where T : class, new()
    {
        var workbook = new XLWorkbook(stream);
        var worksheet = sheetName != null ? workbook.Worksheet(sheetName) : workbook.Worksheet(1);

        // Get the properties of the target class
        var properties = typeof(T).GetProperties();

        // Read the data from the worksheet and map it to objects of the target class
        var rows = worksheet.RowsUsed().Skip(1);
        var objects = new List<T>();

        foreach (var row in rows)
        {
            var obj = new T();
            bool isRowValid = true;

            for (int i = 0; i < properties.Length; i++)
            {
                var cellValue = row.Cell(i + 1).Value.ToString();

                if (!string.IsNullOrEmpty(cellValue))
                {
                    try
                    {
                        // Convert the cell value to the target property type
                        var convertedValue = TypeDescriptor.GetConverter(properties[i].PropertyType).ConvertFromInvariantString(cellValue);
                        //var convertedValue = Convert.ChangeType(cellValue, property.PropertyType);

                        // Set the value of the target property
                        properties[i].SetValue(obj, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "This is Excel Service error");
                        isRowValid = false;
                    }
                }
                else
                {
                    isRowValid = false;
                    break;
                }
            }

            if (isRowValid)
                objects.Add(obj);
        }

        return objects;
    }
    public List<T> ReadExcelFileWithColumnNames<T>(Stream stream, string? sheetName) where T : class, new()
    {
        var workbook = new XLWorkbook(stream);
        var worksheet = sheetName != null ? workbook.Worksheet(sheetName) : workbook.Worksheet(1);

        // Get the properties of the target class
        var properties = typeof(T).GetProperties();

        // Get the values of first row as column names
        var columnNames = worksheet.FirstRow().CellsUsed().Select(cell => cell.Value.ToString()).ToList();

        // Read the data from the worksheet and map it to objects of the target class
        var rows = worksheet.RowsUsed().Skip(1);
        var objects = new List<T>();

        foreach (var row in rows)
        {
            var obj = new T();
            bool isRowValid = true;

            for (int i = 0; i < columnNames.Count; i++)
            {
                var columnName = columnNames[i];

                // Find the property with the same name as the column
                var property = properties
                    .FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                if (property is null)
                {
                    // Skip the column if there is no matching property
                    continue;
                }

                var cellValue = row.Cell(i + 1).Value.ToString();

                if (!string.IsNullOrEmpty(cellValue))
                {
                    try
                    {
                        // Convert the cell value to the target property type
                        var convertedValue = TypeDescriptor.GetConverter(property.PropertyType).ConvertFromInvariantString(cellValue);
                        //var convertedValue = Convert.ChangeType(cellValue, property.PropertyType);

                        // Set the value of the target property
                        property.SetValue(obj, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "This is Excel Service error");
                        isRowValid = false;
                    }
                }
            }

            if (isRowValid)
                objects.Add(obj);
        }

        return objects;

    }
    public byte[] ExportToExcel<T>(IEnumerable<T> data, IEnumerable<string> propertyNames)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        // Write the property names to the first row of the worksheet
        for (int i = 0; i < propertyNames.Count(); i++)
        {
            worksheet.Cell(1, i + 1).Value = propertyNames.ElementAt(i);
        }

        // Write the data to the worksheet
        for (int i = 0; i < data.Count(); i++)
        {
            var obj = data.ElementAt(i);
            if (obj is null) continue;
            for (int j = 0; j < propertyNames.Count(); j++)
            {
                var propertyName = propertyNames.ElementAt(j);
                var propertyValue = obj.GetType().GetProperty(propertyName)!.GetValue(obj, null)?.ToString();

                worksheet.Cell(i + 2, j + 1).Value = propertyValue;
            }
        }

        // Convert the workbook to a byte array
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}