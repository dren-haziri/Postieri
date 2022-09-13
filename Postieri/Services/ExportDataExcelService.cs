using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Postieri.Models;
using System.ComponentModel;

namespace Postieri.Services
{
    public class ExportDataExcelService : IExportDataExcelService
    {
        public XLWorkbook GenerateExcelForOrder<T>(IList<T> objectList) where T : Order
        {
            string[] propertyNames = new string[20];
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            for (int j = 0; j < props.Count; j++)
            {
                PropertyDescriptor prop = props[j];
                propertyNames[j] = prop.Name;

            }

            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Requests");
            int index = 1;
            for (int i = 0; i < propertyNames.Length; i++)
            {
                worksheet.Cell(1, index++).Value = propertyNames[i];
            }

            index = 0;
            for (int i = 1; i <= objectList.Count; i++)
            {
                worksheet.Cell(i + 1, 1).Value = objectList[index].OrderId.ToString();
                worksheet.Cell(i + 1, 2).Value = objectList[index].ProductId.ToString();
                worksheet.Cell(i + 1, 3).Value = objectList[index].Date.ToString();
                worksheet.Cell(i + 1, 4).Value = objectList[index].OrderedOn.ToString();
                worksheet.Cell(i + 1, 5).Value = objectList[index].Price.ToString();
                worksheet.Cell(i + 1, 6).Value = objectList[index].UserId.ToString();
                worksheet.Cell(i + 1, 7).Value = objectList[index].CompanyId.ToString();
                worksheet.Cell(i + 1, 8).Value = objectList[index].Address.ToString();
                worksheet.Cell(i + 1, 9).Value = objectList[index].Sign.ToString();
                worksheet.Cell(i + 1, 10).Value = objectList[index].Status.ToString();
                worksheet.Cell(i + 1, 11).Value = objectList[index].CourierId.ToString();
                worksheet.Cell(i + 1, 12).Value = objectList[index].ManagerId.ToString();
                index++;
            }
            return workbook;
        }
    }
}
