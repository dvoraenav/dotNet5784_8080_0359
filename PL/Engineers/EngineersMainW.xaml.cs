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

namespace PL.Engineers;

/// <summary>
/// Interaction logic for EngineersMainW.xaml
/// </summary>
public partial class EngineersMainW : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;     //static field

    public EngineersMainW(int id = 0)
    {
        ProjectStart = s_bl.StartDate is not null;
        InitializeComponent();

        if (id == 0)       //it means that we are in adding condition
            CurrentEngineer = new BO.Engineer();   //create new engineer

        else//in update condition
        {
            try
            {
                BO.Engineer engineer1 = s_bl.Engineer.Read(id);   //all the details of this id
                CurrentEngineer = engineer1;
                BO.TaskInEngineer? t = s_bl.Engineer.GetTaskInEngineer(id);
                if (t != null)
                {
                    TaskStart = CurrentTask!.StartTask is not null;
                    CurrentTask = s_bl.Task.ReadTask(t.Id);
                    Busy = "Visible";
                    Messege = "Hidden";
                    if (CurrentTask!.StartTask != null)
                        TaskOnTrack = true; //we start the task
                    else
                        TaskOnTrack = false;
                }
                else
                {
                    Busy = "Hidden"; // he cant select more tasks
                    Messege = "Visible";
                }


            }
            catch (Exception ex)//TODO what exception??
            { MessageBox.Show(ex.Message); }
        }
    }
    /// <summary>
    /// bring the engineer's details according to the id the user put
    /// </summary>

    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineersMainW), new PropertyMetadata(null));
    /// <summary>
    /// bring the task details the engineer work on it
    /// </summary>
    public BO.Task? CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }

    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(EngineersMainW));
    /// <summary>
    /// tell us the task  started
    /// </summary>
    public bool TaskOnTrack
    {
        get { return (bool)GetValue(TaskOnTrackProp); }
        set { SetValue(TaskOnTrackProp, value); }
    }

    // Using a DependencyProperty as the backing store for OpenDialoge.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TaskOnTrackProp =
        DependencyProperty.Register("TaskOnTrack", typeof(bool), typeof(EngineersMainW));
    /// <summary>
    /// the engineer in the middle of task so he must finish it before select anouther task
    /// </summary>
    public string Busy
    {
        get { return (string)GetValue(BusyProp); }
        set { SetValue(BusyProp, value); }
    }

    public static readonly DependencyProperty BusyProp =
        DependencyProperty.Register("Busy", typeof(string), typeof(EngineersMainW));
    /// <summary>
    ///  when the engineer does not have task  so the message: "select a task from the list" will be up
    /// </summary>
    public string Messege
    {
        get { return (string)GetValue(MessegeProp); }
        set { SetValue(MessegeProp, value); }
    }

    public static readonly DependencyProperty MessegeProp =
        DependencyProperty.Register("Messege", typeof(string), typeof(EngineersMainW));
    /// <summary>
    /// we have a starting date to project 
    /// </summary>

    public bool ProjectStart
    {
        get { return (bool)GetValue(ProjectStartProp); }
        set { SetValue(ProjectStartProp, value); }
    }

    public static readonly DependencyProperty ProjectStartProp =
        DependencyProperty.Register("ProjectStart", typeof(bool), typeof(TaskWindow));
    /// <summary>
    /// boolian field tell us the task started
    /// </summary>
    public bool TaskStart
    {
        get { return (bool)GetValue(TaskStartProp); }
        set { SetValue(TaskStartProp, value); }
    }

    public static readonly DependencyProperty TaskStartProp =
        DependencyProperty.Register("TaskStart", typeof(bool), typeof(TaskWindow));
    /// <summary>
    /// button that update the information of the engineer
    /// </summary>
    /// <param name="sender">button</param>
    /// <param name="e"> click</param>

    private void UpdateInfo_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Engineer.Update(CurrentEngineer);
            MessageBox.Show("The details have been successfully updated");
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
    /// <summary>
    /// to see more tasks to select
    /// </summary>
    /// <param name="sender">button</param>
    /// <param name="e">click</param>
    private void TaskList_click(object sender, RoutedEventArgs e)
    {
        try
        {
            new TasksForEngineer(CurrentEngineer.Id).Show();
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }
    /// <summary>
    /// to click this button when the engineer finished his task
    /// </summary>
    /// <param name="sender"> button</param>
    /// <param name="e"> click</param>
    private void EndTask_click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (CurrentTask.EndTask != null) { MessageBox.Show("The task already finished"); }
            CurrentTask.EndTask = s_bl.Clock;
            s_bl.Task.Update(CurrentTask);
            MessageBox.Show("The ending date was updated succesfully");
            this.Close();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    /// <summary>
    /// start the task
    /// </summary>
    /// <param name="sender">button</param>
    /// <param name="e">click</param>
    private void StratTask_click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (CurrentTask.StartTask != null)
                MessageBox.Show("The task already started");
            else
            {
                CurrentTask.StartTask = s_bl.Clock;
                s_bl.Task.Update(CurrentTask);
                MessageBox.Show("The starting date was updated succesfully");

                this.Close();
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}




