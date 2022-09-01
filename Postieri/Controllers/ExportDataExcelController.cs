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

        public ExportDataExcelController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult exporDataExcel()
        {
            var performance = (from a in dbContext.Orders
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

            var workbook = new XLWorkbook();

            IXLWorksheet worksheet =
            workbook.Worksheets.Add("Requests");
            worksheet.Cell(1, 1).Value = "Order ID";
            worksheet.Cell(1, 2).Value = "Product ID";
            worksheet.Cell(1, 3).Value = "Date";
            worksheet.Cell(1, 4).Value = "Order On";
            worksheet.Cell(1, 5).Value = "Price";
            worksheet.Cell(1, 6).Value = "User ID";
            worksheet.Cell(1, 7).Value = "Company ID";
            worksheet.Cell(1, 8).Value = "Address";
            worksheet.Cell(1, 9).Value = "Sign";
            worksheet.Cell(1, 10).Value = "Status";
            worksheet.Cell(1, 11).Value = "Courier ID";
            worksheet.Cell(1, 12).Value = "Manager ID";
            
            int index = 0;
            for (int i = 1; i <= performance.Count; i++)
            {
                worksheet.Cell(i + 1, 1).Value = performance[index].OrderId.ToString();
                worksheet.Cell(i + 1, 2).Value = performance[index].ProductId.ToString();
                worksheet.Cell(i + 1, 3).Value = performance[index].Date.ToString();
                worksheet.Cell(i + 1, 4).Value = performance[index].OrderedOn.ToString();
                worksheet.Cell(i + 1, 5).Value = performance[index].Price.ToString();
                worksheet.Cell(i + 1, 6).Value = performance[index].UserId.ToString();
                worksheet.Cell(i + 1, 7).Value = performance[index].CompanyId.ToString();
                worksheet.Cell(i + 1, 8).Value = performance[index].Address.ToString();
                worksheet.Cell(i + 1, 9).Value = performance[index].Sign.ToString();
                worksheet.Cell(i + 1, 10).Value = performance[index].Status.ToString();
                worksheet.Cell(i + 1, 11).Value = performance[index].CourierId.ToString();
                worksheet.Cell(i + 1, 12).Value = performance[index].ManagerId.ToString();
                index++;
            }

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

