using Microsoft.Win32;
using Statistic.CommLibrary;
using Statistic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Statistic.ViewModels
{
    class MainVM : OnPropertyChangedClass
    {
        public FolderBrowserDialog folderBrowser { get; set; }
        public DocumentService service { get; set; }
        public RelayCommand calculationStats { get; }
        public RelayCommand chooseFolder { get; }


        public CheckSubDirectories checkSubDirectories => MainModel.checkSubDirectories;
        public Logger logger => MainModel.logger;
        public ChoseDirectory directory => MainModel.directory;

        public ProgressBarEvent progressBar => MainModel.progressBar;
       


        public MainVM()
        {
            logger.Logbox += "Перед началом подсчета сохраните и закройте файлы Word, Excel и PowerPoint на своем компьютере, чтобы случайно не потерять необходимые данные.";
            calculationStats = new RelayCommand(CalculationStatsVoid);
            chooseFolder = new RelayCommand(ChooseFolderVoid);
            folderBrowser = new FolderBrowserDialog();
        }

        private void CalculationStatsVoid(object parameter)
        {
            bool error = false;
            string errorMessage = "";
            try
            {
                progressBar.Value = 0;
                service = new DocumentService();
                logger.Logbox += "Сбор статистики запущен. Пожалуйста, ожидайте. Это может занять некоторое время";
                service.GetDirectoryStatistic();
            }
            catch (ArgumentNullException ex)
            {
                progressBar.Value = 0;
                MessageBox.Show(ex.ParamName);
                error = true;
                errorMessage = ex.ParamName;
            }

            catch (Exception)
            {
                progressBar.Value = 0;
                errorMessage = "Произошла непредвиденная ошибка в приложении";
                MessageBox.Show(errorMessage);
                error = true;
            }
            finally
            {
                if (error) { logger.Logbox += "При сборе статистики призошла ошибка: " + errorMessage; }
                else { logger.Logbox += "Сбор статистики завершён. Результаты проверки находятся в директории: " + directory.directory; }
            }
        }

        private void ChooseFolderVoid(object parameter)
        {
            folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                directory.directory = folderBrowser.SelectedPath;
            }
        }



    }
}
