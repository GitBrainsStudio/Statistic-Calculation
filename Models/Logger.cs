using Statistic.CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    public class Logger : OnPropertyChangedClass
    {
        private string logbox;
        public string Logbox
        {
            get { return logbox; }
            set { logbox = value; logbox += " : " + DateTime.Now.ToString() + "\n"; OnPropertyChanged(); }
        }

    }
}
