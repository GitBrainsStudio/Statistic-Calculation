using Statistic.CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class ProgressBarEvent : OnPropertyChangedClass
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set { if (value > 100) { _value = 100; } else { _value = value; } OnPropertyChanged(); }
        }
    }
}
