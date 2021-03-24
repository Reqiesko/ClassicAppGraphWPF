using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;
using OxyPlot.Axes;
using Microsoft.Win32;
using System.Text.RegularExpressions;

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

        enum err
        {
            errCoef = 0,
            errLB,
            errBB,
            errStep
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double coef = (double)coefT.Value;
            double leftBorder = (double)leftBorderT.Value;
            double rightBorder = (double)rightBorderT.Value;
            double step = (double)stepT.Value;
            if (coef == 0 || leftBorder >= rightBorder || (leftBorder == 0 && rightBorder == 0) || step == 0)
            {
                int error = 0;
                if (coef == 0)
                    error = (int)err.errCoef;
                else if (leftBorder == 0 && rightBorder == 0)
                    error = (int)err.errBB;
                else if (leftBorder >= rightBorder)
                    error = (int)err.errLB;
                else if (step == 0)
                    error = (int)err.errStep;
                switch (error)
                {
                    case (int)err.errCoef:
                        MessageBox.Show("Коэффициент не может быть равен 0!");
                        break;
                    case (int)err.errBB:
                        MessageBox.Show("Границы равны 0!");
                        break;
                    case (int)err.errLB:
                        MessageBox.Show("Левая граница не может быть больше или равняться правой!");
                        break;
                    case (int)err.errStep:
                        MessageBox.Show("Шаг не может быть равен 0!");
                        break;
                }
            }
            else
            {
                var model = new PlotModel { Title = "График функции A/2*(e^(x/a)+e^(-x/a))" };
                var serie = new FunctionSeries();
                for (double i = leftBorder; i <= rightBorder; i += step)
                {
                    double y = (double)(coef * Math.Cosh(i/coef));
                    serie.Points.Add(new DataPoint(i, y));
                }
                model.LegendPosition = LegendPosition.RightBottom;
                model.LegendPlacement = LegendPlacement.Outside;
                model.LegendOrientation = LegendOrientation.Horizontal;
                model.Series.Add(serie);
                var yAxis = new OxyPlot.Axes.LinearAxis();
                yAxis.AbsoluteMinimum = -100;
                yAxis.AbsoluteMaximum = 100;
                var xAxis = new OxyPlot.Axes.LinearAxis { Position = AxisPosition.Bottom };
                xAxis.Title = "X";
                yAxis.Title = "Y";
                model.Axes.Add(yAxis);
                model.Axes.Add(xAxis);
                model.PlotType = PlotType.Cartesian;
                Plot.Model = model;
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
