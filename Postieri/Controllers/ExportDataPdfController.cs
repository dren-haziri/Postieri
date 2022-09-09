using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportDataPdfController : ControllerBase
    {
        private readonly DataContext dbContext;
        private readonly IExportDataPdfService _exportDataPdfService;
        public ExportDataPdfController(DataContext dbContext, IExportDataPdfService exportDataPdfService)
        {
            this.dbContext = dbContext;
            _exportDataPdfService = exportDataPdfService;
        }

        [HttpGet]
        public IActionResult exporPdfData()
        {
            var performance = (from a in dbContext.Orders
                               orderby a.Date ascending
                               select new Order()
                               {
                                   OrderId = a.OrderId,
                                   ProductId = a.ProductId,
                                   Date = a.Date,
                                   Price = a.Price
                               }).ToList();

            PdfDocument document = _exportDataPdfService.GeneratePdfForOrder(performance);

            using (var streamout = new MemoryStream())
            {
                document.Save(streamout);
                return new FileContentResult(streamout.ToArray(), "application/pdf")
                {
                    FileDownloadName = "DataPerformance"
                };
            }
        }
    }
}