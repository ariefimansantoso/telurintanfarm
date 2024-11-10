using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace QuickAccounting.Services
{
    public class PdfService
    {
        public async Task<byte[]> GeneratePdf(string content)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create a new PDF document
                var pdf = new PdfDocument();

                // Render the HTML content into the PDF document
                pdf = PdfGenerator.GeneratePdf(content, PdfSharp.PageSize.A4); // The last parameter is the starting page

                // Save the PDF document to the memory stream
                pdf.Save(memoryStream);
                await memoryStream.FlushAsync();

                return memoryStream.ToArray(); // Return the PDF as a byte array
            }
        }
    }

}
