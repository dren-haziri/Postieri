using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using Postieri.Data;
using Postieri.Models;
using System.Collections.Generic;
using Aspose.Pdf;
using System.ComponentModel;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfExportDataController : ControllerBase
    {
        private readonly DataContext dbContext;

        public PdfExportDataController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        [HttpGet]
        public IActionResult exporPdfData()
        {
            DateTime dateTimeToday = DateTime.Now;

            var dataSet = (from a in dbContext.Orders
                           where a.Date.Month > 6
                           orderby a.Date ascending
                           select new Order()
                           {
                               OrderId = a.OrderId,
                               ProductId = a.ProductId,
                               Date = a.Date,
                               Price = a.Price,
                               CourierId = a.OrderId
                           }).ToList();

            DataTable dataTable = ToDataTable(dataSet);

            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
            };
            var pdfpage = document.Pages.Add();
            Table table = new Table()
            {
                //ColumnWidths = "8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3% 8.3%",
                ColumnWidths = "20% 20% 20% 20% 20%",
                DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black)

            };
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