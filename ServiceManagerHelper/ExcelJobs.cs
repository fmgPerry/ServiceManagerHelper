using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ServiceManagerHelper
{
    class ExcelJobs
    {
        public static List<Vehicle> GetVehicles(string filename)
        {
            var vehicles = new List<Vehicle>();

            var xlApp = new Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(filename);
            var xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            // start at row 2, row 1 has column headings - Excel starts count at 1
            for (int row = 2; row <= rowCount; row++)
            {
                vehicles.Add(new Vehicle(xlRange.Rows[row]));
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close(0);
            Marshal.ReleaseComObject(xlWorkbook);

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return vehicles;
        }
    }
}
