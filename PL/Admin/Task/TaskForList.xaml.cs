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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskForListWindow.xaml
    /// </summary>
    public partial class TaskForListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

        public TaskForListWindow()
        {
            InitializeComponent();
            try
            {
                TasksList = s_bl?.Task.TaskList()!;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// to represent the engineer list
        /// </summary>
        public IEnumerable<BO.TaskInList> TasksList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProporty); }
            set { SetValue(TaskListProporty, value); }
        }

        public static readonly DependencyProperty TaskListProporty =
            DependencyProperty.Register("TasksList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskForListWindow));
        /// <summary>
        /// add new field to select("all" means that we want to select all the engineer)
        /// </summary>
        public BO.TaskStatus Status { get; set; } = BO.TaskStatus.All;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">combobox </param>
        /// <param name="e">event of select option from all the expirience levels</param>

        private void StatusSelection(object sender, SelectionChangedEventArgs e)
        {
            TasksList = (Status == BO.TaskStatus.All) ?
        s_bl.Task.TaskList()! : s_bl?.Task.TaskList(task => task.Status == Status)!;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"> button of add angineer to list</param>
        /// <param name="e">  event of add</param>
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new TaskWindow().ShowDialog();
                TasksList = s_bl?.Task.TaskList()!;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        
        private void TaskSelection(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.TaskInList? taskList = ((sender as ListView)?.SelectedItem as BO.TaskInList);
                new TaskWindow(taskList!.Id).ShowDialog();// create new engineer window in adding condition and prevent return to the privius window until it is closed
                TasksList = s_bl?.Task.TaskList()!;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
      
        private void Search_TaskChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TasksList = s_bl?.Task.TaskList()!.Where(X => X.Name!.Contains((sender as TextBox)!.Text))?.ToList()!;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

}

