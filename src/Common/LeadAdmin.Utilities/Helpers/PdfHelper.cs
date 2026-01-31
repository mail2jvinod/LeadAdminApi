using HtmlToPDFCore;
using System.IO;

namespace LeadAdmin.Utilities.Helpers
{
    public class PdfHelper
    {
        public static MemoryStream ToPdf(string htmlContent)
        {
            var result = new MemoryStream();
            var htmlToPdf = new HtmlToPDF();
            htmlToPdf.Margins = new PageMargins(0,0,0,0);

            var pdf = htmlToPdf.ReturnPDF(htmlContent);            
            result.Write(pdf);
            result.Position = 0;
            return result;
        }
    }
}
