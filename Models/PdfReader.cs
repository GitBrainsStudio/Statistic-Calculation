using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class PdfReader : IDocumentReader
    {
        string pdfFilePath { get; set; }
        string extension { get; set; }
        public PdfReader(string _pdfFilePath, string _extension)
        {
            this.pdfFilePath = _pdfFilePath;
            this.extension = _extension;
        }

        public DocumentInfo GetInfo()
        {
            PdfDocument document = new PdfDocument();
            
            try
            {
                document.LoadFromFile(this.pdfFilePath);
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            

            int pageCount = document.Pages.Count;

            StringBuilder content = new StringBuilder();

            foreach (PdfPageBase page in document.Pages)
            {
                content.Append(page.ExtractText());
            }

            //отнимаем 60 символов, это автограф от библиотеки Spire, которая она вставляет без твоего ведома
            int charCount = content.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "").Length - 60;

            return new DocumentInfo(this.pdfFilePath, this.extension, charCount, pageCount);
        }
    }
}
