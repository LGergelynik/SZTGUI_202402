﻿using QV596H_HFT_2023241.Models;
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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for CarTab.xaml
    /// </summary>
    public partial class CarTab : UserControl
    {
        public CarTab()
        {
            InitializeComponent();

        }
        private void GetMostPopularBrand_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CarWindowViewModel).GetMostPopularBrand();
        }
    }


    
}
