using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace Lab3WPF
{
    public partial class FPoints
    {
        public FunctionSeries logic(double leftBorder, double step, double rightBorder, double coef)
        {
            var serie = new FunctionSeries();
            for (double i = leftBorder; i <= rightBorder; i += step)
            {
                double y = (double)(coef * Math.Cosh(i / coef));
                serie.Points.Add(new DataPoint(i, y));
            }
            return serie;
        }
    }
}
