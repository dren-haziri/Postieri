using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Postieri.Models;
using System.ComponentModel;

namespace Postieri.Services
{
    public class ExportDataPdfService : IExportDataPdfService
    {
        public PdfDocument GeneratePdfForOrder<T>(IList<T> objList) where T : Order
        {
            string[] propertyNames = new string[20];
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            for (int j = 0; j < props.Count; j++)
            {
                PropertyDescriptor prop = props[j];
                propertyNames[j] = prop.Name;

            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);

            graphics.DrawString("Performance", new XFont("Arial", 30, XFontStyle.Bold), XBrushes.Black, new XPoint(200, 70));
            graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(100, 100), new XPoint(500, 100));

            graphics.DrawString(propertyNames[0], new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(50, 280));
            graphics.DrawString(propertyNames[1], new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(220, 280));
            graphics.DrawString(propertyNames[2], new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(400, 280));
            graphics.DrawString(propertyNames[4], new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(500, 280));

            graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, 290), new XPoint(550, 290));

            int currentYposition_values = 303;
            int currentYposition_lines = 310;

            if (objList.Count <= 20)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    graphics.DrawString(objList[i].OrderId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    graphics.DrawString(objList[i].ProductId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    graphics.DrawString(objList[i].Date.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(400, currentYposition_values));
                    graphics.DrawString(objList[i].Price.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(500, currentYposition_values));

                    graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, currentYposition_lines), new XPoint(550, currentYposition_lines));

                    currentYposition_values += 20;
                    currentYposition_lines += 20;
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    graphics.DrawString(objList[i].OrderId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    graphics.DrawString(objList[i].ProductId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    graphics.DrawString(objList[i].Date.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(400, currentYposition_values));
                    graphics.DrawString(objList[i].Price.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(500, currentYposition_values));

                    graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, currentYposition_lines), new XPoint(550, currentYposition_lines));


                    currentYposition_values += 20;
                    currentYposition_lines += 20;

                    objList.Remove(objList[i]);
                }
                page = document.AddPage();
                graphics = XGraphics.FromPdfPage(page);
                currentYposition_values = 33;
                currentYposition_lines = 40;

                bool firstpage = true;
                for (int i = 0; i < objList.Count; i++)
                {
                    if (i != 0 && i % 30 == 0)
                    {
                        page = document.AddPage();
                        graphics = XGraphics.FromPdfPage(page);
                        currentYposition_values = 33;
                        currentYposition_lines = 40;
                    }
                    graphics.DrawString(objList[i].OrderId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    graphics.DrawString(objList[i].ProductId.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    graphics.DrawString(objList[i].Date.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(400, currentYposition_values));
                    graphics.DrawString(objList[i].Price.ToString(), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(500, currentYposition_values));

                    graphics.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(50, currentYposition_lines), new XPoint(550, currentYposition_lines));


                    currentYposition_values += 20;
                    currentYposition_lines += 20;
                }
            }
            return document;
        }
    }
}
