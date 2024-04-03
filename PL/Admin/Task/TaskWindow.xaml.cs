using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool OpenDialoge
        {
            get { return (bool)GetValue(OpenDialogProp); }
            set { SetValue(OpenDialogProp, value); }
        }

        // Using a DependencyProperty as the backing store for OpenDialoge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenDialogProp =
            DependencyProperty.Register("OpenDialoge", typeof(bool), typeof(TaskWindow));

        public bool ProjectStart
        {
            get { return (bool)GetValue(ProjectStartProp); }
            set { SetValue(ProjectStartProp, value); }
        }

        // Using a DependencyProperty as the backing store for OpenDialoge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectStartProp =
            DependencyProperty.Register("ProjectStart", typeof(bool), typeof(TaskWindow));


        public bool AddMode
        {
            get { return (bool)GetValue(AddModeProp); }
            set { SetValue(AddModeProp, value); }
        }

        public static readonly DependencyProperty AddModeProp =
            DependencyProperty.Register("AddMode", typeof(bool), typeof(TaskWindow));


        public IEnumerable<TaskInList> TaskList
        {
            get { return (IEnumerable<TaskInList>)GetValue(ListProp); }
            set { SetValue(ListProp, value); }
        }

        // Using a DependencyProperty as the backing store for TasksList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListProp =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<TaskInList>), typeof(TaskWindow));



        public ObservableCollection<TaskInList> DepList
        {
            get { return (ObservableCollection<TaskInList>)GetValue(DepListPro); }
            set { SetValue(DepListPro, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepListPro =
            DependencyProperty.Register("DepList", typeof(ObservableCollection<TaskInList>), typeof(TaskInList));



        public IEnumerable<string> Engineers
        {
            get { return (IEnumerable<string>)GetValue(EngineersProp); }
            set { SetValue(EngineersProp, value); }
        }

        // Using a DependencyProperty as the backing store for TasksList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineersProp =
            DependencyProperty.Register("Engineers", typeof(IEnumerable<string>), typeof(TaskWindow));

        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow));


        public BO.EngineerExpireance Difficulty { get; set; } = BO.EngineerExpireance.Beginner;//defult level is begineer

        public TaskWindow(int id = 0)
        {
            AddMode = id == 0;
            ProjectStart = s_bl.StartDate is not null;
            OpenDialoge = false;
            TaskList = s_bl.Task.TaskList();

            InitializeComponent();
            if (id == 0)//it means that we are in adding condition
            {
                CurrentTask = new BO.Task();//create new engineer
                CurrentTask.Depndencies = new();
            }
            else//we want to update
            {
                try
                {
                    CurrentTask = s_bl.Task.ReadTask(id)!;//all the details of this id
                    TaskList = s_bl.Task.TaskList(x => x.Id != id);
                }
                catch (Exception ex)//TODO what exception??
                { MessageBox.Show(ex.Message); }
            }
            if (CurrentTask.Depndencies is null)
                CurrentTask.Depndencies = new();
            if (CurrentTask.Engineer is null)
                CurrentTask.Engineer = new();

            Engineers = s_bl.Engineer.GetEngineerList(x => x.Level >= CurrentTask.DifficultyLevel).Select(x => x.FullName + " " + x.Id);
            DepList = new(CurrentTask.Depndencies);
        }


        /*****************************   Function  **************************/

        private void ExpirienceSelection(object sender, SelectionChangedEventArgs e)
        {
            CurrentTask.DifficultyLevel = Difficulty;//the level that selected
        }

        private void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Task.Create(CurrentTask);
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Task.Update(CurrentTask);//calling to the update method
                this.Close();//auto closing window
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }


        private void ChangeDependencies(object sender, RoutedEventArgs e) => OpenDialoge = true;

        //private void Deleteclick_Button(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (sender is Button button)
        //        {
        //            TaskInList selected = (TaskInList)button.Tag;
        //            CurrentTask.Depndencies.RemoveAll(dep => dep.Id == selected.Id);
        //            BO.Task tmp = CurrentTask;
        //            CurrentTask = null;
        //            CurrentTask = tmp;
        //        }
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //}

        private void AddDependency(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if (sender is Label label)
                {
                    if (label.Background == Brushes.Transparent)
                    {
                        TaskInList selected = label.Content as TaskInList;
                        CurrentTask.Depndencies.Add(selected);
                        DepList.Add(selected);
                        label.Background = Brushes.Green;
                    }
                    else
                    {
                        TaskInList selected = label.Content as TaskInList;
                        CurrentTask.Depndencies.Remove(selected);
                        DepList.Remove(selected);
                        label.Background = Brushes.Transparent;
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AddEngineer(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox combo)
                {
                    string selectName = combo.SelectedItem as string;
                    int id = int.Parse(selectName.Split().LastOrDefault()!);
                    CurrentTask.Engineer!.Id = id;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
