using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class DocumentInfo
    {
        public string filePath { get; set; }
        public string extension { get; set; }
        public int charCount { get; set; }
        public int pageCount { get; set; }

        public DocumentInfo(string _filePath, string _extension, int _charCount, int _pageCount)
        {
            filePath = _filePath;
            extension = _extension;
            charCount = _charCount;
            pageCount = _pageCount;
        }
    }
}
