using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postieri.Data;
using System.Collections.Concurrent;
using System.Linq;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportDataExcelController : ControllerBase
    {
        private readonly DataContext dbContext;
        private readonly IExportDataExcelService _exportDataExcelService;
        public ExportDataExcelController(DataContext dbContext, IExportDataExcelService exportDataExcelService)
        {
            this.dbContext = dbContext;
            _exportDataExcelService = exportDataExcelService;
        }

        [HttpGet]
        public IActionResult generateExcel()
        {
            var objList = (from a in dbContext.Orders
                               orderby a.Date ascending
                               select new Order()
                               {
                                   OrderId = a.OrderId,
                                   ProductId = a.ProductId,
                                   Date = a.Date,
                                   OrderedOn = a.OrderedOn,
                                   Price = a.Price,
                                   UserId = a.UserId,
                                   CompanyId = a.CompanyId,
                                   Address = a.Address,
                                   Sign = a.Sign,
                                   Status = a.Status,
                                   CourierId = a.CourierId,
                                   ManagerId = a.ManagerId
                               }).ToList();

            XLWorkbook workbook = _exportDataExcelService.GenerateExcelForOrder(objList);

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"DataPerformanc.xlsx"
                };
            }
        }
    }
}

