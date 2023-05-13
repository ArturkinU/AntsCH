﻿using System;
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



namespace Ants
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            /*
                         <Ellipse Width="25" Height="25" Canvas.Left="500" Canvas.Top="500" Fill="{StaticResource SolidLightColor}"/>
                         <Ellipse Width="21" Height="21" Canvas.Left="502" Canvas.Top="502" Fill="{StaticResource SolidMediumColor}"/>
                         <Ellipse Width="7" Height="7" Canvas.Left="509" Canvas.Top="509" Fill="{StaticResource SolidLightColor}"/>
                         <Line X1="0" Y1="0" X2="500" Y2="500" Stroke="Coral" StrokeThickness="5" StrokeDashCap="Round" StrokeEndLineCap="Round" StrokeDashArray="4 4"/>
             */
            Point p = Mouse.GetPosition(DropLayer);
            InfoLengLabel.Content = p.ToString();
            Ellipse firEllipse = new Ellipse() { Width = 25, Height = 25 };
            Ellipse secEllipse = new Ellipse() { Width = 21, Height = 21 };
            Ellipse thwEllipse = new Ellipse() { Width = 7 , Height = 7  };

            firEllipse.Fill = this.FindResource("SolidLightColor") as SolidColorBrush;
            secEllipse.Fill = this.FindResource("SolidMediumColor") as SolidColorBrush;
            thwEllipse.Fill = this.FindResource("SolidLightColor") as SolidColorBrush;

            firEllipse.SetValue(Canvas.LeftProperty, p.X - (25 / 2));
            secEllipse.SetValue(Canvas.LeftProperty, p.X - (21 / 2));
            thwEllipse.SetValue(Canvas.LeftProperty, p.X - ( 7 / 2));

            firEllipse.SetValue(Canvas.TopProperty, p.Y - (25 / 2));
            secEllipse.SetValue(Canvas.TopProperty, p.Y - (21 / 2));
            thwEllipse.SetValue(Canvas.TopProperty, p.Y - ( 7 / 2));

            firEllipse.Tag = 'f';
            firEllipse.Tag = 'w';
            firEllipse.Tag = 'w';

            DropLayer.Children.Add(firEllipse);
            DropLayer.Children.Add(secEllipse);
            DropLayer.Children.Add(thwEllipse);

            

            
            foreach (UIElement child in DropLayer.Children)
            {
                if (child is Ellipse)
                {
                    
                    {
                        
                        {
                            InfoLengLabel.Content = child.GetValue(TagProperty).ToString();
                        }
                    }
                }
            }

            





        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            // 
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
