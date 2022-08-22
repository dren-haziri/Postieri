using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Data;
using System.IO;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportDataController : ControllerBase
    {

        ExportData dataObj = new ExportData();

        [HttpGet]
        public IActionResult exportData()
        {
            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
            };
            var pdfpage = document.Pages.Add();
            Table table = new Table()
            {
                ColumnWidths = "8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3%",
                DefaultCellPadding= new MarginInfo(10, 5, 10, 5),
                Border = new BorderInfo(BorderSide.All,.5f,Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All,.2f,Color.Black)
                
            };
            DataTable dataTable = dataObj.GetRecord();
            table.ImportDataTable(dataTable, true, 0, 0);
            document.Pages[1].Paragraphs.Add(table);
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
