using Statistic.CommLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class ChoseDirectory : OnPropertyChangedClass
    {
        public Logger logger => MainModel.logger;

        private string _directory;
        public string directory
        {
            get { return _directory; }
            set 
            {
                if (!Directory.Exists(value)) { logger.Logbox += "Указанная директория не существует";  }
                else { _directory = value; OnPropertyChanged(); logger.Logbox += "Директория изменена на : " + directory; } 
            }
        }


    }
}
