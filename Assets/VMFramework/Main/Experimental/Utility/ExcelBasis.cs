using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public static class ExcelCellStyle
{
    private static IWorkbook workbook;

    private static XSSFSheet sheet;

    private static ICellStyle titleCellStyle;

    private static XSSFCellStyle defaultValueCellStyle;

    private static Dictionary<Type, Func<object, ICellStyle>> allValueCellStyles = new();

    private static XSSFCellStyle boolTrueCellStyle, boolFalseCellStyle;

    public static void HandleWorkbook(IWorkbook workbook, XSSFSheet sheet)
    {
        ExcelCellStyle.workbook = workbook;
        ExcelCellStyle.sheet = sheet;

        CreateTitleCellStyle();

        defaultValueCellStyle = CreateDefaultValueCellStyle();

        CreateBoolValueCellStyles();

        allValueCellStyles.Clear();

        allValueCellStyles[typeof(bool)] = GetBoolValueCellStyle;
    }

    public static void CreateTitleCellStyle()
    {
        titleCellStyle = workbook.CreateCellStyle();

        XSSFFont font = (XSSFFont)workbook.CreateFont();
        font.SetColor(new XSSFColor(System.Drawing.Color.FromArgb(68, 84, 106)));
        font.IsBold = true;
        font.FontHeightInPoints = 15;

        titleCellStyle.SetFont(font);

        titleCellStyle.WrapText = true;
        titleCellStyle.Alignment = HorizontalAlignment.Center;
        titleCellStyle.VerticalAlignment = VerticalAlignment.Center;

        titleCellStyle.BorderBottom = BorderStyle.Thick;
        titleCellStyle.BottomBorderColor = IndexedColors.BlueGrey.Index;

        titleCellStyle.FillForegroundColor = IndexedColors.LightOrange.Index;
        titleCellStyle.FillPattern = FillPattern.SolidForeground;
    }

    public static ICellStyle GetTitleCellStyle()
    {
        return titleCellStyle;
    }

    public static ICellStyle GetValueCellStyle(object value)
    {
        if (value == null)
        {
            return defaultValueCellStyle;
        }

        if (allValueCellStyles.TryGetValue(value.GetType(), out var cellStyleGetter))
        {
            return cellStyleGetter(value);
        }

        return defaultValueCellStyle;
    }

    private static XSSFCellStyle CreateDefaultValueCellStyle()
    {
        XSSFCellStyle cellStyle = (XSSFCellStyle)workbook.CreateCellStyle();

        cellStyle.WrapText = true;
        cellStyle.Alignment = HorizontalAlignment.Left;
        cellStyle.VerticalAlignment = VerticalAlignment.Center;
        cellStyle.SetFillForegroundColor(new XSSFColor(System.Drawing.Color.FromArgb(255, 255, 204)));
        cellStyle.FillPattern = FillPattern.SolidForeground;

        return cellStyle;
    }

    private static void CreateBoolValueCellStyles()
    {
        boolTrueCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();

        IFont trueFont = workbook.CreateFont();

        trueFont.Color = IndexedColors.DarkGreen.Index;

        boolTrueCellStyle.SetFont(trueFont);

        boolTrueCellStyle.WrapText = true;
        boolTrueCellStyle.Alignment = HorizontalAlignment.Center;
        boolTrueCellStyle.VerticalAlignment = VerticalAlignment.Center;
        boolTrueCellStyle.FillForegroundColor = IndexedColors.LightGreen.Index;
        boolTrueCellStyle.FillPattern = FillPattern.SolidForeground;

        boolFalseCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();

        IFont falseFont = workbook.CreateFont();

        falseFont.Color = IndexedColors.DarkRed.Index;

        boolFalseCellStyle.SetFont(falseFont);

        boolFalseCellStyle.WrapText = true;
        boolFalseCellStyle.Alignment = HorizontalAlignment.Center;
        boolFalseCellStyle.VerticalAlignment = VerticalAlignment.Center;
        boolFalseCellStyle.FillForegroundColor = IndexedColors.Coral.Index;
        boolFalseCellStyle.FillPattern = FillPattern.SolidForeground;

    }

    private static ICellStyle GetBoolValueCellStyle(object value)
    {
        if (value is bool boolValue)
        {
            return boolValue ? boolTrueCellStyle : boolFalseCellStyle;
        }
        return boolTrueCellStyle;
    }
}
