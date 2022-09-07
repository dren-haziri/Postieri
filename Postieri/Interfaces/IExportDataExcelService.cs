using ClosedXML.Excel;
using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IExportDataExcelService
    {
        XLWorkbook GenerateExcelForOrder<T>(IList<T> objList) where T : Order;
        XLWorkbook GenerateExcelForWarehouse<T>(IList<T> objList) where T : Warehouse;
    }
}
