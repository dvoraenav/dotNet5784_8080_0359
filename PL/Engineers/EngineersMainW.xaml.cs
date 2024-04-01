using PL.Engineer;
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
using System.Windows.Shapes;

namespace PL.Engineers
{
    /// <summary>
    /// Interaction logic for EngineersMainW.xaml
    /// </summary>
    public partial class EngineersMainW : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;//static field

        public EngineersMainW()
        {
            InitializeComponent();
        }

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        /// <summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            try
            {
               
            }
            catch(Exception ex) { }
        }

        private void CurrentTask_click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
