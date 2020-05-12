using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class WordReader : IDocumentReader
    {
        string wordFilePath { get; set; }
        string extension { get; set; }
        public WordReader(string _wordFilePath, string _extension)
        {
            this.wordFilePath = _wordFilePath;
            this.extension = _extension;
        }

        public DocumentInfo GetInfo()
        {
            Document document = new Document();
            try
            {
                document.LoadFromFile(this.wordFilePath, FileFormat.Docx2010);
            }

            catch(Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }

            //символы с пробелами
            int charCount = document.BuiltinDocumentProperties.CharCountWithSpace;

            //символы без пробелов
            int charCountWithoutSpace = document.BuiltinDocumentProperties.CharCount;

            int pageCount = document.PageCount;

            return new DocumentInfo(this.wordFilePath, this.extension, charCount, pageCount);
        }

    }
}
