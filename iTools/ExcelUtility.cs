using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace iTools
{
	public class ExcelUtility
	{
		public static Excel.Worksheet GetWorksheet(string file, Excel.Application excelApp, string sheetName = null)
		{
			var excelWorkbook = excelApp.Workbooks.Open(file,
				Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
				Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

			if (excelWorkbook == null) return null;

			var worksheet = (Excel.Worksheet)excelWorkbook.Worksheets.Item[sheetName];

			return worksheet;
		}

		public static string GetCellValue(Excel.Worksheet worksheet, string column, int row)
		{
			if (worksheet == null) return string.Empty;

			var excelCell = worksheet.Range[column + row, column + row];
			return excelCell.Text != null ? excelCell.Text.ToString() : string.Empty;
		}

		public static void SetCellValue(Excel.Worksheet worksheet, string column, int row, string keyValue)
		{
			if (worksheet == null) throw new ArgumentNullException("worksheet");

			var excelCell = worksheet.Range[column + row, column + row];
			excelCell.Value2 = keyValue;
		}

		public static void KillExcelApp(Excel.Application excelApp)
		{
			var hwnd = new IntPtr(excelApp.Hwnd);
			int id;
			GetWindowThreadProcessId(hwnd, out id);
			var process = Process.GetProcessById(id);
			process.Kill();
		}

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int id);
	}
}
