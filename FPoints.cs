using System;
using System.Collections.Generic;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;

namespace Lab3WPF
{
    public class FPoints
    {
        public double X { get; set; }
        public double Y { get; set; }
        public FunctionSeries Logic(double leftBorder, double step, double rightBorder, double coef)
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
