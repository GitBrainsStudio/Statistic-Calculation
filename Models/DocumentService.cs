using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Statistic.Models
{
    class DocumentService
    {
        List<string> filesCollection { get; set; }

        List<string> wordExtensions { get; set; }

        List<string> pdfExtensions { get; set; }

        List<string> excelExtensions { get; set; }

        CheckSubDirectories checkSubDirectories => MainModel.checkSubDirectories;
        ChoseDirectory choseDirectory => MainModel.directory;

        ProgressBarEvent progressBar => MainModel.progressBar;

        public DocumentService()
        {
            FillDictionaries();
        }

        public void GetDirectoryStatistic()
        {
            progressBar.Value = 0;
            List<DocumentInfo> filesStats = new List<DocumentInfo>();

            if (choseDirectory.directory == null) throw new ArgumentNullException("Выберите директорию");

            filesCollection = new List<string>(Directory.GetFiles(choseDirectory.directory));

            if (filesCollection.Count() == 0) throw new ArgumentNullException("Выбрана пустая директория");

            if (checkSubDirectories.checkSubDirectories)
            {
                Directory.GetDirectories(choseDirectory.directory).ToList().ForEach(v => Directory.GetFiles(v).ToList().ForEach(x => filesCollection.Add(x)));
            }

            int progressStep = 100 / filesCollection.Count();



            filesCollection.ForEach(v => {

                string extensionFile = v.Substring(v.LastIndexOf('.'));

                wordExtensions.ForEach(w => { if (extensionFile == w) filesStats.Add(new WordReader(v, extensionFile).GetInfo());});

                excelExtensions.ForEach(w => { if (extensionFile == w) filesStats.Add(new ExcelReader(v, extensionFile).GetInfo()); });

                pdfExtensions.ForEach(w => { if (extensionFile == w) filesStats.Add(new PdfReader(v, extensionFile).GetInfo()); });

                progressBar.Value += progressStep;
                //в дальнейшем сюда добавляем новые классы для различных форматов документов со своей логикой обработки (pdf,excel,и т.д.)
            });

            

            if (filesStats.Count() == 0) throw new ArgumentNullException("В данной директории нет подходящих документов");

            //печать отчета
            new ExcelReport(filesStats, choseDirectory.directory).WriteReport();

            progressBar.Value = 100;
        }

        //Подгружаем словари расширений документов из файла конфигурации. 
        //Все они вынесены в конфиг, для удобства в дальнейшем добавлении новых форматов. Нужно будет просто добавить в конфиг через блокнот нужный формат через запятую. 
        //Не надо будет залазить в код и пересобирать решение.
        private void FillDictionaries()
        {
            try
            {
                wordExtensions = new List<string>(ConfigurationManager.AppSettings["word_extension"].Split(','));
                pdfExtensions = new List<string>(ConfigurationManager.AppSettings["pdf_extension"].Split(','));
                excelExtensions = new List<string>(ConfigurationManager.AppSettings["excel_extension"].Split(','));
            }

            catch
            {
                throw new ArgumentNullException("Не корректные данные в файле конфигурации");
            }
        }


    }
}
