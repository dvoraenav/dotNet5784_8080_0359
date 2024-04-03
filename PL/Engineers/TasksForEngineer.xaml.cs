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

namespace PL.Engineers
{
    /// <summary>
    /// Interaction logic for TasksForEngineer.xaml
    /// </summary>
    public partial class TasksForEngineer : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

        public TasksForEngineer(int id=0)
        {
            InitializeComponent();
            TasksList = s_bl.Task.TaskForEngineer(id);
        }

        public IEnumerable<BO.TaskInList> TasksList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProporty); }
            set { SetValue(TaskListProporty, value); }
        }

        public static readonly DependencyProperty TaskListProporty =
            DependencyProperty.Register("TasksList", typeof(IEnumerable<BO.TaskInList>), typeof(TasksForEngineer));
        private void TaskSelection(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.TaskInList? taskList = ((sender as ListView)?.SelectedItem as BO.TaskInList);
               // create new engineer window in adding condition and prevent return to the privius window until it is closed
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
