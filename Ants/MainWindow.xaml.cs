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
