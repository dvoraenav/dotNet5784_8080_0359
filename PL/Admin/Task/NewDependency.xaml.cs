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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for NewDependency.xaml
    /// </summary>
    public partial class NewDependency : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
        public NewDependency(BO.Task task)
        {
            InitializeComponent();
           
        }
    }
}
