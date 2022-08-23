using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Data.SqlClient;
using System.Data;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PddSharpController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PddSharpController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult exporPdfData()
        {
            string name = null;
            string lastname = null;
            string departament = null;

            SqlConnection connectionDb = new SqlConnection(_config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            SqlCommand dbCommand = new SqlCommand("SELECT * FROM People", connectionDb);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCommand);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            connectionDb.Close();

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Database to PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Arial", 20, XFontStyle.Regular);
            int yPoint = 100;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                name = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                lastname = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                departament = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                graph.DrawString(name, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(lastname, font, XBrushes.Black, new XRect(280, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(departament, font, XBrushes.Black, new XRect(420, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                yPoint = yPoint + 40;
            }
            using (var streamout = new MemoryStream())
            {
                pdf.Save(streamout);
                return new FileContentResult(streamout.ToArray(), "application/pdf")
                {
                    FileDownloadName = "DataPerformance"
                };
            }
        }
    }
}
