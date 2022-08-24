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
using System.ComponentModel;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

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
        

        [HttpGet]
        public IActionResult exporPdfData()
        {
            var performance = (from a in dbContext.Orders
                           where a.Status == "success" || a.Status == "Succes"
                           orderby a.Date ascending
                           select new Order()
                           {
                               OrderId = a.OrderId,
                               ProductId = a.ProductId,
                               Date = a.Date,
                               Price = a.Price
                           }).ToList();

            double totalPrice = performance.Sum(item => item.Price);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);

            graphics.DrawString("Performance", new XFont("Arial", 30, XFontStyle.Bold), XBrushes.Red, new XPoint(200, 70));
            graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(100, 100), new XPoint(500, 100));

            graphics.DrawString("Order Id", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(50, 180));
            graphics.DrawString("Product Id", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(220, 180));
            graphics.DrawString("Date", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(400, 180));
            graphics.DrawString("Price", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(500, 180));

            graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, 190), new XPoint(550, 190));

            int currentYposition_values = 203;
            int currentYposition_lines = 210;

            if (performance.Count <= 20)
            {
                for (int i = 0; i < performance.Count; i++)
                {
                    graphics.DrawString(performance[i].OrderId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    graphics.DrawString(performance[i].ProductId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    graphics.DrawString(performance[i].Date.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(400, currentYposition_values));
                    graphics.DrawString(performance[i].Price.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(500, currentYposition_values));

                    graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, currentYposition_lines), new XPoint(550, currentYposition_lines));


                    currentYposition_lines += 20;
                    currentYposition_lines += 20;
                }
                graphics.DrawString(totalPrice.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Red, new XPoint(500, 780));
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    graphics.DrawString(performance[i].OrderId.ToString(), new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(100, 280));
                    graphics.DrawString(performance[i].ProductId.ToString(), new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(100, 280));
                    graphics.DrawString(performance[i].Date.ToString(), new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(100, 280));
                    graphics.DrawString(performance[i].Price.ToString(), new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(100, 280));


                    currentYposition_lines += 20;
                    currentYposition_lines += 20;

                    performance.Remove(performance[i]);
                }
                page = document.AddPage();
                graphics = XGraphics.FromPdfPage(page);
                currentYposition_values = 33;
                currentYposition_lines = 40;

                bool firstpage = true;
                for(int i = 0; i < performance.Count; i++)
                {
                    if(i != 0 && i % 30 == 0)
                    {
                        page = document.AddPage();
                        graphics = XGraphics.FromPdfPage(page);
                        currentYposition_values = 33;
                        currentYposition_lines = 40;
                    }
                    graphics.DrawString(performance[i].OrderId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    graphics.DrawString(performance[i].ProductId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    graphics.DrawString(performance[i].Date.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(400, currentYposition_values));
                    graphics.DrawString(performance[i].Price.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(500, currentYposition_values));

                    graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, currentYposition_lines), new XPoint(550, currentYposition_lines));


                    currentYposition_lines += 20;
                    currentYposition_lines += 20;
                }
                graphics.DrawString(totalPrice.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Red, new XPoint(500, 780));
            }

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