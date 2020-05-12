using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Statistic.Models
{
    class ExcelReport 
    {
        private string directory { get; set; }
        List<DocumentInfo> filesInfo { get; set; }
        public ExcelReport(List<DocumentInfo> _filesInfo, string _directory)
        {
            directory = _directory;
            filesInfo = _filesInfo;
        }

        public void WriteReport()
        {
            Workbook workbook = new Workbook();

            //обращаемся к первому листу
            Worksheet sheet = workbook.Worksheets[0];

            //записываем в ячейки
            filesInfo.ForEach(v => { 
                sheet.Range["A" + (filesInfo.IndexOf(v) + 1)].Text = "Путь до файла: " + v.filePath;
                sheet.Range["B" + (filesInfo.IndexOf(v) + 1)].Text = "Расширение файла: " + v.extension;
                sheet.Range["C" + (filesInfo.IndexOf(v) + 1)].Text = "Количество символов: " + v.charCount;
                sheet.Range["D" + (filesInfo.IndexOf(v) + 1)].Text = "Количество страниц: " + v.pageCount;
            });

            sheet.AllocatedRange.AutoFitColumns();
            sheet.AllocatedRange.AutoFitRows();

            try
            {          
                //сохраняем файл и даём ему название
                workbook.SaveToFile(directory + @"\" + "Статистика по заказу.xls", ExcelVersion.Version97to2003);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }


            try
            {
                //открываем сохранённый файл с отчетом
                System.Diagnostics.Process.Start(workbook.FileName);
            }
            catch { }
        }

    }


}
