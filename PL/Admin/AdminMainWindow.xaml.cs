using PL.Engineer;
using PL.Task;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void EngineerList_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void TaskList_Click(object sender, RoutedEventArgs e)
        {
            new TaskForListWindow().Show();
        }
        private void Initialization_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to create Initial data?", "Data Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
            if (result == MessageBoxResult.Yes) { s_bl.InitializeDB(); }//intilize data if return yes
        }
        /// <summary>
        /// "reset data click"
        /// </summary>
        /// <param name="sender"> button</param>
        /// <param name="e"> click</param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to reset the data?", "Reset Data", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) { s_bl.ResetDB(); }//reset data if return yes
        }

        private void StartProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl == null)
                    s_bl!.StartDate = DateTime.Now;
                else
                    MessageBox.Show("A new project cannot be created. There is an existing project in progress");//TODO
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
        private void Gantt_window(object sender, RoutedEventArgs e) => new Gant().Show();

    }
}
