using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Postieri.Models;
using System.ComponentModel;

namespace Postieri.Services
{
    public class ExportDataExcelService : IExportDataExcelService
    {
        public XLWorkbook GenerateExcelForOrder<T>(IList<T> objList) where T : Order
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
            for (int i = 1; i <= objList.Count; i++)
            {
                worksheet.Cell(i + 1, 1).Value = objList[index].OrderId.ToString();
                worksheet.Cell(i + 1, 2).Value = objList[index].ProductId.ToString();
                worksheet.Cell(i + 1, 3).Value = objList[index].Date.ToString();
                worksheet.Cell(i + 1, 4).Value = objList[index].OrderedOn.ToString();
                worksheet.Cell(i + 1, 5).Value = objList[index].Price.ToString();
                worksheet.Cell(i + 1, 6).Value = objList[index].UserId.ToString();
                worksheet.Cell(i + 1, 7).Value = objList[index].CompanyId.ToString();
                worksheet.Cell(i + 1, 8).Value = objList[index].Address.ToString();
                worksheet.Cell(i + 1, 9).Value = objList[index].Sign.ToString();
                worksheet.Cell(i + 1, 10).Value = objList[index].Status.ToString();
                worksheet.Cell(i + 1, 11).Value = objList[index].CourierId.ToString();
                worksheet.Cell(i + 1, 12).Value = objList[index].ManagerId.ToString();
                index++;
            }
            return workbook;
        }

        public XLWorkbook GenerateExcelForWarehouse<T>(IList<T> objList) where T : Warehouse
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
            for (int i = 1; i <= objList.Count; i++)
            {
                worksheet.Cell(i + 1, 1).Value = objList[index].WarehouseId.ToString();
                worksheet.Cell(i + 1, 2).Value = objList[index].Name.ToString();
                worksheet.Cell(i + 1, 3).Value = objList[index].Location.ToString();
                worksheet.Cell(i + 1, 4).Value = objList[index].Area.ToString();
                worksheet.Cell(i + 1, 5).Value = objList[index].NumOfShelves.ToString();
                index++;
            }
            return workbook;
        }
    }
}
