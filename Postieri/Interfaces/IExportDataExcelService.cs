using ClosedXML.Excel;
using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IExportDataExcelService
    {
        XLWorkbook GenerateExcelForOrder<T>(IList<T> objectList) where T : Order;
    }
}
