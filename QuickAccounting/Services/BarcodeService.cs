using QRCoder;
using QuickAccounting.Repository.Interface;
using System.Drawing;

namespace QuickAccounting.Services
{
	public interface IBarcodeService
	{
		string GenerateQRCode(string text);
	}

	public class BarcodeService : IBarcodeService
	{
		ISalesInvoice _salesInvoice;

		public BarcodeService(ISalesInvoice salesInvoice)
		{
			_salesInvoice = salesInvoice;
		}

		public string GenerateQRCode(string text)
		{
			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

			PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
			byte[] byteArray = qrCode.GetGraphic(20);
			return Convert.ToBase64String(byteArray);
		}
	}
}
