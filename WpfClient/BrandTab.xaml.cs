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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for BrandTab.xaml
    /// </summary>
    public partial class BrandTab : UserControl
    {
        public BrandTab()
        {
            InitializeComponent();
        }
        private void GetBrandWithMostCars_Click(object sender, RoutedEventArgs e)
        {
            // Hívjuk meg a GetBrandWithMostCars metódust a nézetmodellben
            (DataContext as BrandWindowViewModel)?.GetBrandWithMostCars();
        }
    }
}
