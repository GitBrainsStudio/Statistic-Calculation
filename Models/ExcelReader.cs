
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class ExcelReader : IDocumentReader
    {
        string excelFilePath { get; set; }
        string extension { get; set; }
        public ExcelReader(string _excelFilePath, string _extension)
        {
            this.excelFilePath = _excelFilePath;
            this.extension = _extension;
        }

        public DocumentInfo GetInfo()
        {
            Workbook document = new Workbook();

            try
            {
                document.LoadFromFile(this.excelFilePath);
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
           

            int charCount = 0;
            int pageCount = document.Worksheets.Count();
            char[] charsToTrim = { ' ' };

            document.Worksheets.ToList().ForEach(
                v =>
                {
                    var iXlsRange = v.Cells;
                    foreach (var spireObj in iXlsRange)
                    {
                        spireObj.Cells.ToList().ForEach(
                            cell =>
                            {
                                string row = cell.DisplayedText.Replace(" ", "");
                                if (!String.IsNullOrWhiteSpace(row))
                                charCount += cell.DisplayedText.Replace(" ", "").Length;
                            }
                            );
                    }
                }
                );


            return new DocumentInfo(this.excelFilePath, this.extension, charCount, pageCount);
        }
    }
}
