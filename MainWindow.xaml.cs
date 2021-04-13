using System;
using System.Linq;
using System.Windows;
using System.IO;
using OxyPlot;
using OxyPlot.Axes;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Lab3WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        enum Err
        {
            errCoef = 0,
            errLB,
            errBB,
            errStep
        };
        List<FPoints> valueList = new List<FPoints> { };
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            valueList.Clear();
            tableOfValue.ItemsSource = null;
            double coef = (double)coefT.Value;
            double leftBorder = (double)leftBorderT.Value;
            double rightBorder = (double)rightBorderT.Value;
            double step = (double)stepT.Value;
            if (coef == 0 || leftBorder >= rightBorder || (leftBorder == 0 && rightBorder == 0) || step <= 0)
            {
                int error = 0;
                if (coef == 0)
                    error = (int)Err.errCoef;
                else if (leftBorder == 0 && rightBorder == 0)
                    error = (int)Err.errBB;
                else if (leftBorder >= rightBorder)
                    error = (int)Err.errLB;
                else if (step <= 0)
                    error = (int)Err.errStep;
                switch (error)
                {
                    case (int)Err.errCoef:
                        MessageBox.Show("Коэффициент не может быть равен 0!");
                        break;
                    case (int)Err.errBB:
                        MessageBox.Show("Границы равны 0!");
                        break;
                    case (int)Err.errLB:
                        MessageBox.Show("Левая граница не может быть больше или равняться правой!");
                        break;
                    case (int)Err.errStep:
                        MessageBox.Show("Шаг не может быть меньше 0!");
                        break;
                }
            }
            else
            {
                var model = new PlotModel { };
                FPoints point = new FPoints();
                model.LegendPosition = LegendPosition.RightBottom;
                model.LegendPlacement = LegendPlacement.Outside;
                model.LegendOrientation = LegendOrientation.Horizontal;
                var serie = point.Logic(leftBorder, step, rightBorder, coef);
                model.Series.Add(serie);
                var yAxis = new LinearAxis();
                yAxis.AbsoluteMinimum = -100;
                yAxis.AbsoluteMaximum = 100;
                var xAxis = new LinearAxis { Position = AxisPosition.Bottom };
                if (coef < 0.30 && coef > -0.30)
                {
                    xAxis.AbsoluteMinimum = -5;
                    xAxis.AbsoluteMaximum = 5;
                }
                xAxis.Title = "X";
                yAxis.Title = "Y";
                model.Axes.Add(yAxis);
                model.Axes.Add(xAxis);
                model.PlotType = PlotType.Cartesian;
                Plot.Model = model;
                
                for (int i = 0; i < serie.Points.Count; i++)
                {
                    valueList.Add(new FPoints { X = serie.Points[i].X, Y = serie.Points[i].Y });
                }
                tableOfValue.CanUserSortColumns = false;
                tableOfValue.ItemsSource = valueList;
            }
            Console.WriteLine(coef + " " + leftBorder + " " + step + " " + rightBorder);
        }
        
        private void ImportFromFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                try
                {
                    var tempStr = sr.ReadToEnd();
                    double[] arr = new double[4];
                    arr = tempStr.Split(' ', '\n', '\r', '\t').Select(double.Parse).ToArray();
                    leftBorderT.Value = arr[0];
                    stepT.Value = arr[1];
                    rightBorderT.Value = arr[2];
                    coefT.Value = arr[3];
                }
                catch
                {
                    MessageBox.Show("Invalid file");
                }
                sr.Close();
            }
        }
        private void SaveInitialData(object sender, RoutedEventArgs e)
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                StreamWriter sr = new StreamWriter(saveFileDialog.FileName);
                sr.Write(leftBorderT.Value + " " + stepT.Value + " " + rightBorderT.Value + " " + coefT.Value);
                sr.Close();
            }
        }
        
        private void SaveGraph(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                Plot.SaveBitmap(saveFileDialog.FileName + ".bmp");
            }
        }

        private void SaveTable(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                StreamWriter sr = new StreamWriter(saveFileDialog.FileName);
                for (int i = 0; i < tableOfValue.Items.Count; i++)
                {
                    sr.WriteLine(valueList[i].X.ToString().PadRight(4) + " " + valueList[i].Y);
                }
                sr.Close();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
               "Автор: Нерадовский Артемий, 494 группа. \nПрограмма отрисовывает график функции A/2*(e^(x/a)+e^(-x/a))\nв заданном диапозоне.",
               "About Programm",
                MessageBoxButton.OK);
        }
    }
}
