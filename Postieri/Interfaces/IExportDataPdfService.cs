using PdfSharp.Pdf;
using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IExportDataPdfService
    {
        PdfDocument GeneratePdfForOrder<T>(IList<T> objList) where T : Order;
    }
}
