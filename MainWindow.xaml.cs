using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double leftBorder = (double)leftBorderT.Value;
            double step = (double)stepT.Value;
            double rightBorder = (double)rightBorderT.Value;
            double coef = (double)CoefT.Value;
            var model = new PlotModel { Title = "График функции A/2*(e^(x/2)+e^(-x/2))" };
            var serie = new FunctionSeries();
            for (double i = leftBorder; i <= rightBorder; i += step)
            {
                double y = (double)(double)(coef * Math.Cosh(i / coef));
                serie.Points.Add(new DataPoint(i, y));
            }
            model.LegendPosition = LegendPosition.RightBottom;
            model.LegendPlacement = LegendPlacement.Outside;
            model.LegendOrientation = LegendOrientation.Horizontal;
            model.Series.Add(serie);
            var yAxis = new OxyPlot.Axes.LinearAxis();
            var xAxis = new OxyPlot.Axes.LinearAxis { Position = AxisPosition.Bottom };
            xAxis.Title = "X";
            yAxis.Title = "Y";
            model.Axes.Add(yAxis);
            model.Axes.Add(xAxis);
            model.PlotType = PlotType.Cartesian;
            Plot.Model = model;
        }
        private void ImportFromFile(object sender, RoutedEventArgs e)
        {

        }
        private void SaveInitialData(object sender, RoutedEventArgs e)
        {

        }
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {

        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {

        }

    }
}
