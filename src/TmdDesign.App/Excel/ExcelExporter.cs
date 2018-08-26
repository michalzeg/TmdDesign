using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using OfficeOpenXml;
using TmdDesign.Calculations.Solvers;

namespace TmdDesign.Excel
{
    internal class ExcelExporter
    {
        private string directoryPath;
        private ISolverDataProvider solverDataProvider;

        public ExcelExporter(ISolverDataProvider solverDataProvider)
        {
            this.solverDataProvider = solverDataProvider;
            this.getDictionaryPath();
        }

        private void getDictionaryPath()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:///", "");

            this.directoryPath = Path.GetDirectoryName(exePath).Replace("\\", @"/");
        }

        private string FilePath(double frequency)
        {
            string filePath = string.Format("{0}/fr_{1}.xlsx", this.directoryPath, frequency.ToString("F2"));
            return filePath;
        }

        private void CheckIfWorksheetExistsAndDeleteIt(ExcelWorkbook xlBook, string title)
        {
            var xlSheet = xlBook.Worksheets.FirstOrDefault(e => e.Name == title);
            if (xlSheet != null)
            {
                xlBook.Worksheets.Delete(xlSheet);
            }
        }

        private void SaveData(ExcelWorkbook xlBook, IList<double> x, IList<double> y, string title)
        {
            this.CheckIfWorksheetExistsAndDeleteIt(xlBook, title);
            xlBook.Worksheets.Add(title);
            var xlSheet = xlBook.Worksheets[title];

            int numberOfCells = x.Count();
            for (int i = 0; i <= numberOfCells - 1; i++)
            {
                xlSheet.Cells[i + 1, 1].Value = x[i];
                xlSheet.Cells[i + 1, 2].Value = y[i];
                if (i >= 100000)
                {
                    numberOfCells = 100000;
                    break;
                }
            }

            AddChart(title, xlSheet, numberOfCells);
        }

        private void AddChart(string title, ExcelWorksheet xlSheet, int numberOfCells)
        {
            var chart = xlSheet.Drawings.AddChart(title, OfficeOpenXml.Drawing.Chart.eChartType.Line);

            var serie = chart.Series.Add(xlSheet.Cells[1, 2, numberOfCells, 2], xlSheet.Cells[1, 1, numberOfCells, 1]);

            this.FormatChart(title, chart);
        }

        private void FormatChart(string title, OfficeOpenXml.Drawing.Chart.ExcelChart chart)
        {
            chart.SetPosition(0, 630);
            chart.SetSize(800, 600);
            chart.Title.Text = title;
            chart.XAxis.Title.Text = "Frequency";
            chart.YAxis.Title.Text = title;
        }

        public void SaveResultsToFile(double frequency)
        {
            string filePath = this.FilePath(frequency);
            FileInfo xlsFile = new FileInfo(filePath);
            using (ExcelPackage xlPackage = new ExcelPackage(xlsFile))
            {
                ExcelWorkbook xlBook = xlPackage.Workbook;

                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Acceleration.Select(e => e.A1).ToList(), "Tmd acceleration");
                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Acceleration.Select(e => e.A1).ToList(), "Tmd acceleration");
                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Acceleration.Select(e => e.A2).ToList(), "Structure acceleration");
                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Velocity.Select(e => e.A1).ToList(), "Tmd velocity");
                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Velocity.Select(e => e.A2).ToList(), "Structure velocity");
                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Displacement.Select(e => e.A1).ToList(), "Tmd displacement");
                this.SaveData(xlBook, this.solverDataProvider.Time.ToList(), this.solverDataProvider.Displacement.Select(e => e.A2).ToList(), "Structure displacement");

                xlPackage.Save();
            }
        }
    }
}