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
            OpenDialoge = false;
            Clock = s_bl.Clock;
            ProjectStart = s_bl.StartDate is null;
            ProjectEnd = s_bl.EndDate is not null;
            InitializeComponent();
        }

        public bool ProjectStart
        {
            get { return (bool)GetValue(ProjectStartProp); }
            set { SetValue(ProjectStartProp, value); }
        }

        public static readonly DependencyProperty ProjectStartProp =
            DependencyProperty.Register("ProjectStart", typeof(bool), typeof(AdminMainWindow));
        public bool ProjectEnd
        {
            get { return (bool)GetValue(ProjectEndProp); }
            set { SetValue(ProjectEndProp, value); }
        }

        public static readonly DependencyProperty ProjectEndProp =
            DependencyProperty.Register("ProjectEnd", typeof(bool), typeof(AdminMainWindow));
        /// <summary>
        /// to add engineers or update the details
        /// </summary>
        /// <param name="sender"> button</param>
        /// <param name="e"> click</param>

        public bool OpenDialoge
        {
            get { return (bool)GetValue(OpenDialogeProp); }
            set { SetValue(OpenDialogeProp, value); }
        }

        public static readonly DependencyProperty OpenDialogeProp =
            DependencyProperty.Register("OpenDialoge", typeof(bool), typeof(AdminMainWindow));



        public DateTime Clock
        {
            get { return (DateTime)GetValue(ClockProperty); }
            set { SetValue(ClockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClockProperty =
            DependencyProperty.Register("Clock", typeof(DateTime), typeof(AdminMainWindow));



        private void EngineerList_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        /// <summary>
        /// add or update tasks and watch the list of all the tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskList_Click(object sender, RoutedEventArgs e)
        {
            new TaskForListWindow().Show();
        }
        /// <summary>
        /// to initilize data
        /// </summary>
        /// <param name="sender"> button</param>
        /// <param name="e"> click</param>
        private void Initialization_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to create Initial data?", "Data Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
            if (result == MessageBoxResult.Yes) { s_bl.InitializeDB(); }//intilize data if return yes
            this.Close();
            new AdminMainWindow().Show();

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
        /// <summary>
        /// to create starting date to project will be the current time
        /// </summary>
        /// <param name="sender"> button</param>
        /// <param name="e"> click</param>

        private void StartProject_Click(object sender, RoutedEventArgs e)
        {
            OpenDialoge = true;
          
            //TODO
        }
        private void Gantt_window(object sender, RoutedEventArgs e) => new Gant().Show();

        private void CreateSchedule(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is DatePicker picker)
                {

                    s_bl.Task.ScheduleTasks((DateTime)picker.SelectedDate);
                    this.Close();
                    new AdminMainWindow().Show();
                }
             
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
    }
}
