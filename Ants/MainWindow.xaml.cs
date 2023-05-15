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
using System.Text.RegularExpressions;
using System.Windows.Ink;
using System.Windows.Media.Animation;
using System.Threading;

namespace Ants
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ellipse> points = new List<Ellipse>();
        List<AntPoint> antPoints = new List<AntPoint>();
        MainACS ACS = new MainACS();
        bool CanEditPoints = true;

        public MainWindow()
        {
            InitializeComponent();

            InitializeBtnsConnected();
        }

        private void InitializeBtnsConnected()
        {
            CloseWinBtn.MouseDown += CloseBtnClick;
            MinimumWinBtn.MouseDown += TrayBtnClick;
            WinGrab.PreviewMouseLeftButtonDown += WinGrabEvent;
            StartBtn.Click += StartBtn_Click;
            DropLayer.MouseLeftButtonDown += DropLayer_MouseLeftButtonDown;
        }

        private void DropLayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanEditPoints == false)
            {
                return;
            }

            Point p = Mouse.GetPosition(DropLayer);
            InfoLengLabel.Content = p.ToString();
            Ellipse firEllipse = new Ellipse() { Width = 25, Height = 25 };
            Ellipse secEllipse = new Ellipse() { Width = 21, Height = 21 };
            Ellipse thwEllipse = new Ellipse() { Width = 7, Height = 7 };

            firEllipse.Fill = this.FindResource("SolidLightColor") as SolidColorBrush;
            secEllipse.Fill = this.FindResource("SolidMediumColor") as SolidColorBrush;
            thwEllipse.Fill = this.FindResource("SolidLightColor") as SolidColorBrush;

            firEllipse.SetValue(Canvas.LeftProperty, p.X - (25 / 2));
            secEllipse.SetValue(Canvas.LeftProperty, p.X - (21 / 2));
            thwEllipse.SetValue(Canvas.LeftProperty, p.X - (7 / 2));

            firEllipse.SetValue(Canvas.TopProperty, p.Y - (25 / 2));
            secEllipse.SetValue(Canvas.TopProperty, p.Y - (21 / 2));
            thwEllipse.SetValue(Canvas.TopProperty, p.Y - (7 / 2));

            DropLayer.Children.Add(firEllipse);
            DropLayer.Children.Add(secEllipse);
            DropLayer.Children.Add(thwEllipse);

            foreach (UIElement child in points)
            {
                Line line = new Line()
                {
                    X1 = p.X,
                    Y1 = p.Y,
                    X2 = p.X,
                    Y2 = p.Y,
                    Stroke = FindResource("SolidLightColor") as SolidColorBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 5 },
                    StrokeDashCap = PenLineCap.Round,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };

                DoubleAnimation Xanimet = new DoubleAnimation()
                {
                    Duration = TimeSpan.FromSeconds(1),
                    From = p.X,
                    To = (double)child.GetValue(Canvas.LeftProperty) + (25 / 2)
                };
                DoubleAnimation Yanimet = new DoubleAnimation()
                {
                    Duration = TimeSpan.FromSeconds(1),
                    From = p.Y,
                    To = (double)child.GetValue(Canvas.TopProperty) + (25 / 2)
                };

                LineLayer.Children.Add(line);

                line.BeginAnimation(Line.X2Property, Xanimet);
                line.BeginAnimation(Line.Y2Property, Yanimet);
            }

            points.Add(firEllipse);
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            // Start Algoritm
            CanEditPoints = false;
            DoubleAnimation OpacityImg = new DoubleAnimation()
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(10)
            };
            SpinerImg.BeginAnimation(OpacityProperty, OpacityImg);
            InfoLengLabel.Content = "Идёт выполнение алгоритма";
            GenerateAntPointList();

            Parallel.Invoke(
                new Action(
                    () =>
                    {
                        ACS.Start(
                            Beta: int.Parse(BFactorBox.Text),
                            Alpha: int.Parse(AFactorBox.Text),
                            Iterations: int.Parse(IterationBox.Text),
                            AntCount: int.Parse(CountAntsBox.Text),
                            EvaporationRate: FactorEvaporationBox.Value,
                            Points: antPoints
                        );
                    }
                )
            );
        }

        private void GenerateAntPointList()
        {
            int id_gen = 0;
            foreach (Ellipse point in points)
            {
                id_gen++;
                antPoints.Add(
                    new AntPoint(
                        id_gen,
                        (float)(double)point.GetValue(Canvas.LeftProperty) + (float)(25 / 2),
                        (float)(double)point.GetValue(Canvas.TopProperty) + (float)(25 / 2)
                    )
                );
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void WinGrabEvent(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TrayBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
