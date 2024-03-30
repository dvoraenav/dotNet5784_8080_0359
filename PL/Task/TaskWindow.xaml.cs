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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
        public TaskWindow(int id=0)
        {
            InitializeComponent();
            if (id == 0)//it means that we are in adding condition
            {
                BO.Task task = new BO.Task();//create new engineer
                CurrentTask = task;//the current engineer
            }
            else//we want to update
            {
                try
                {
                    BO.Task task1 = s_bl.Task.ReadTask(id)!;//all the details of this id
                    CurrentTask = task1;
                }
                catch (Exception ex)//TODO what exception??
                { MessageBox.Show(ex.Message); }
            }
        }
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
        /// <summary>
        ///create the level of expiriance
        /// </summary>

        public IEnumerable<BO.TaskInList> DependencyList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(DependencyListProporty); }
            set { SetValue(DependencyListProporty, value); }
        }

        public static readonly DependencyProperty DependencyListProporty =
            DependencyProperty.Register("TasksList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskForListWindow), new PropertyMetadata(null));
        /// <summary>
        public BO.EngineerExpireance Difficulty { get; set; } = BO.EngineerExpireance.Beginner;//defult level is begineer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"> combobox</param>
        /// <param name="e">select level </param>
        private void ExpirienceSelection(object sender, SelectionChangedEventArgs e)
        {
            CurrentTask.DifficultyLevel = Difficulty;//the level that selected
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"> button of add engineer</param>
        /// <param name="e">Contains state information and event data associated with the adding</param>
        private void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Task.Create(CurrentTask);
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        /// <summary>
        /// update the engineer
        /// </summary>
        /// <param name="sender"> button of update</param>
        /// <param name="e">Contains state information and event data associated with the update </param>
        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Task.Update(CurrentTask);//calling to the update method
                this.Close();//auto closing window
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }
    }
}
  