using OfficeOpenXml;
using OfficeOpenXml.Table;
using QuickAccounting.Data;
using QuickAccounting.Data.Setting;
using QuickAccounting.Repository.Interface;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;

namespace QuickAccounting.Services
{
	public class ExcelService : IExcelService
	{
		ISalesInvoice _salesInvoice;
		IWebHostEnvironment _hostingEnvironment;

		public ExcelService(ISalesInvoice salesInvoice, IWebHostEnvironment hostingEnvironment)
		{
			_salesInvoice = salesInvoice;
			_hostingEnvironment = hostingEnvironment;				
		}

		public async Task<byte[]> GenerateExcelWorkbook(int id)
		{
			SalesMasterView model = new SalesMasterView();
			List<ProductView> TodoList = await _salesInvoice.SalesInvoiceDetailsView(Convert.ToInt32(id));
			model = await _salesInvoice.SalesInvoiceMasterView(id);
			
			string fileName = @"faktur-template.xlsx";
			//string filePath = $@"{_hostingEnvironment.ContentRootPath}\wwwroot\templates\" + fileName;
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "templates", fileName);

            var fileNotaTemplate = new System.IO.FileInfo(filePath);
            if (!fileNotaTemplate.Exists)
                throw new Exception($"Template not found at {fileNotaTemplate.FullName}");

            using (ExcelPackage package = new ExcelPackage(fileNotaTemplate))
			{
				if(package == null)
				{
					throw new Exception("Template not found");
                }
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; //package.Workbook.Worksheets["Faktur"];
				if(worksheet == null)
				{
					throw new Exception("Worksheet not found");
                }
				if(worksheet.Cells[3, 5] == null)
				{
					throw new Exception("Cell not found");
                }
                worksheet.Cells[3, 5].Value = model.SalesMasterId;
				worksheet.Cells[4, 5].Value = model.LedgerName;
				worksheet.Cells[5, 5].Value = model.Address;
				worksheet.Cells[3, 10].Value = model.Date;				
				worksheet.Cells[4, 10].Value = model.PaymentStatus == "Approved" ? "Disetujui" : "Belum disetujui";

				for (int c = 0; c < TodoList.Count; c++)
				{
					int startingRow = c + 8;
					worksheet.Cells[startingRow, 3].Value = TodoList[c].ProductCode;
					worksheet.Cells[startingRow, 5].Value = TodoList[c].ProductName;
					worksheet.Cells[startingRow, 6].Value = TodoList[c].Qty;
					worksheet.Cells[startingRow, 7].Value = TodoList[c].UnitName;
					worksheet.Cells[startingRow, 8].Value = Convert.ToInt32(TodoList[c].SalesRate);
				}				

				return package.GetAsByteArray();
			}
		}

        public byte[] ExportListToExcel<T>(List<T> data, string sheetName)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            worksheet.Cells["A1"].LoadFromCollection(data, true, OfficeOpenXml.Table.TableStyles.Medium1);
            return package.GetAsByteArray();
        }

        public byte[] ExportDataTableToExcel(DataTable dataTable, string sheetName)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true, OfficeOpenXml.Table.TableStyles.Medium1);
            return package.GetAsByteArray();
        }
    }

	public interface IExcelService
	{
		Task<byte[]> GenerateExcelWorkbook(int id);
		byte[] ExportListToExcel<T>(List<T> data, string sheetName);
		byte[] ExportDataTableToExcel(DataTable dataTable, string sheetName);
    }
}
