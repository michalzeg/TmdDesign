using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Threading.Tasks;
using TmdDesign.SimpleClasses;
using OfficeOpenXml;

namespace TmdDesign.Excel
{
    class SavingDataToExcel
    {
        private string directoryPath;
        private ISolver solver;

        public SavingDataToExcel(ISolver solver)
        {
            this.solver = solver;
            this.getDictionaryPath();
        }

        private void getDictionaryPath()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:///", "");
  
            this.directoryPath = Path.GetDirectoryName(exePath).Replace("\\",@"/");
            
        }
        private string filePath(double frequency)
        {
            string s = string.Format("{0}/fr_{1}.xlsx", this.directoryPath, frequency.ToString("F2"));
            return s;
        }
        private void checkIfWorksheetExistsAndDeleteIt(ExcelWorkbook xlBook,string title)
        {
            ExcelWorksheet xlSheet = xlBook.Worksheets.FirstOrDefault(e => e.Name == title);
            if (xlSheet != null)
            {
                xlBook.Worksheets.Delete(xlSheet);
            }
        }
        
        private void saveData(ExcelWorkbook xlBook, List<double> x, List<double> y, string title)
        {
            this.checkIfWorksheetExistsAndDeleteIt(xlBook, title);
            xlBook.Worksheets.Add(title);
            ExcelWorksheet xlSheet = xlBook.Worksheets[title];

            //adding data to the cells
            int numberOfCells = x.Count;
            for (int i = 0; i<= numberOfCells - 1;i++)
            {
                xlSheet.Cells[i + 1, 1].Value = x[i];
                xlSheet.Cells[i + 1, 2].Value = y[i];
                if (i >= 100000)
                {
                    numberOfCells = 100000;
                    break;
                }
            }

            //adding chart
            
            var chart = xlSheet.Drawings.AddChart(title, OfficeOpenXml.Drawing.Chart.eChartType.Line);
            
            //adding data to chart
            var serie = chart.Series.Add(xlSheet.Cells[1, 2, numberOfCells, 2], xlSheet.Cells[1, 1, numberOfCells, 1]);

            //formating chart
            this.formatChart(title, chart);
        }

        private void formatChart(string title, OfficeOpenXml.Drawing.Chart.ExcelChart chart)
        {
            chart.SetPosition(0, 630);
            chart.SetSize(800, 600);
            chart.Title.Text = title;
            chart.XAxis.Title.Text = "Frequency";
            chart.YAxis.Title.Text = title;
        }
        public void SaveResultsToFile(double frequency)
        {
            //main void - saves accelerations, velocities and displacements to exe file

            string filePath = this.filePath(frequency);
            FileInfo xlsFile = new FileInfo(filePath);
            using (ExcelPackage xlPackage = new ExcelPackage(xlsFile))
            {
                ExcelWorkbook xlBook = xlPackage.Workbook;

                this.saveData(xlBook, this.solver.Time,this.solver.A.ConvertAll(e => e.A1).ToList(), "Tmd acceleration");
                this.saveData(xlBook, this.solver.Time,this.solver.A.ConvertAll(e => e.A1).ToList(), "Tmd acceleration");
                this.saveData(xlBook, this.solver.Time,this.solver.A.ConvertAll(e => e.A2).ToList(),  "Structure acceleration");
                this.saveData(xlBook, this.solver.Time,this.solver.V.ConvertAll(e => e.A1).ToList(),  "Tmd velocity");
                this.saveData(xlBook, this.solver.Time,this.solver.V.ConvertAll(e => e.A2).ToList(),  "Structure velocity");
                this.saveData(xlBook, this.solver.Time,this.solver.U.ConvertAll(e => e.A1).ToList(),  "Tmd displacement");
                this.saveData(xlBook, this.solver.Time,this.solver.U.ConvertAll(e => e.A2).ToList(),  "Structure displacement");

                xlPackage.Save();
                
            }
        }

    }
}
