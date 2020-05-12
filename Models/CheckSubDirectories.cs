using Statistic.CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class CheckSubDirectories : OnPropertyChangedClass
    {
        private bool _checkSubDirectories;
        public bool checkSubDirectories
        {
            get { return _checkSubDirectories; }
            set { _checkSubDirectories = value; OnPropertyChanged(); }
        }
    }
}
