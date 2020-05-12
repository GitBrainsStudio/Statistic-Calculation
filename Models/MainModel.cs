using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class MainModel
    {
        public static ProgressBarEvent progressBar { get; set; } = new ProgressBarEvent();
        public static CheckSubDirectories checkSubDirectories { get; } = new CheckSubDirectories();
        public static ChoseDirectory directory { get; } = new ChoseDirectory();
        public static Logger logger { get; } = new Logger();
    }
}
