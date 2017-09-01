using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using OxyPlot.Axes;

namespace WpfApp1.WindowList
{
    public partial class Graph : Window
    {
        public PlotModel Model { get; private set; }
        private ListItem[] list;
        OxyPlot.Series.LineSeries lineTotal = new OxyPlot.Series.LineSeries() { Title = "Total" };
        OxyPlot.Series.LineSeries line1 = new OxyPlot.Series.LineSeries() { Title = "Android" };
        OxyPlot.Series.LineSeries line2 = new OxyPlot.Series.LineSeries() { Title = "Java" };
        OxyPlot.Series.LineSeries line3 = new OxyPlot.Series.LineSeries() { Title = ".NET" };
        OxyPlot.Series.LineSeries line4 = new OxyPlot.Series.LineSeries() { Title = "PHP" };
        OxyPlot.Series.LineSeries line5 = new OxyPlot.Series.LineSeries() { Title = "C++" };
        OxyPlot.Series.LineSeries line6 = new OxyPlot.Series.LineSeries() { Title = "JavaScript" };
        OxyPlot.Series.LineSeries line7 = new OxyPlot.Series.LineSeries() { Title = "Ruby" };
        OxyPlot.Series.LineSeries line8 = new OxyPlot.Series.LineSeries() { Title = "IOS" };



        //        public IList<DataPoint> Points { get; private set; }


        public Graph()
        {
            Model = new PlotModel { Title = "Java" };
        }
        public Graph(ListItem[] list)
        {
            DataContext = this;
            Model = new PlotModel
            {
                Title = "Java",
                LegendTitle="Legend",
                LegendBorder = OxyColors.Black,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.TopLeft,
                LegendOrientation = LegendOrientation.Horizontal
            };
            
            this.list = list;
            SetPoints();
        }

        public void SetPoints()
        {
            Model.Axes.Add(new OxyPlot.Axes.LinearAxis() {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                TickStyle = TickStyle.Outside });

            DateTime maxDate = DateTime.ParseExact(
                    list[list.Length - 1].Date,
                    "MM/dd/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
            DateTime minDate = DateTime.ParseExact(
                list[0].Date,
                "MM/dd/yyyy",
                System.Globalization.CultureInfo.InvariantCulture);

            Model.Axes.Add(new OxyPlot.Axes.DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(minDate),
                Maximum = OxyPlot.Axes.DateTimeAxis.ToDouble(maxDate),
                StringFormat = "MM/dd"
            });

            for (int i = 0; i<list.Length;i++) {

                DateTime week = DateTime.ParseExact(
                list[i].Date,
                "MM/dd/yyyy",
                System.Globalization.CultureInfo.InvariantCulture);

                line1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value1)));
                line2.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value2)));
                line3.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value3)));
                line4.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value4)));
                line5.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value5)));
                line6.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value6)));
                line7.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value7)));
                line8.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value8)));
                lineTotal.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(week), double.Parse(list[i].Value1)+
                    double.Parse(list[i].Value2)+
                    double.Parse(list[i].Value3)+
                    double.Parse(list[i].Value4)+
                    double.Parse(list[i].Value5)+
                    double.Parse(list[i].Value6)+
                    double.Parse(list[i].Value7)+
                    double.Parse(list[i].Value8)));
            }


            Model.Series.Add(line1);
            Model.Series.Add(line2);
            Model.Series.Add(line3);
            Model.Series.Add(line4);
            Model.Series.Add(line5);
            Model.Series.Add(line6);
            Model.Series.Add(line7);
            Model.Series.Add(line8);
            Model.Series.Add(lineTotal);
        }


        private void Toggle_Total(object sender, RoutedEventArgs e)
        {
            if (Model.Series[8].IsVisible)
            {
                Model.Series[8].IsVisible=false;
                TotalButton.Content = "Total";
                TotalButton.Opacity = 1.0;
            }
            else
            {
                Model.Series[8].IsVisible=true;
                TotalButton.Opacity = 0.0;
            }
            Model.InvalidatePlot(true);
        }

       public void Initialize()
        {
            InitializeComponent();
            TotalButton.Opacity = 0.0;
        }
            
    }
}
