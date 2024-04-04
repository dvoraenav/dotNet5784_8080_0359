using PL.Admin;
using PL.Engineer;
using PL.Engineers;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PL;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
    /// <summary>
    /// boolian field if the window is open or closed
    /// </summary>

    bool isClose {  get; set; }
    /// <summary>
    /// constractor to initilize data
    /// </summary>
    public MainWindow()         
    {
        isClose = true;   //in the begining the window is closed
        CurrentDate = s_bl.Clock;
        UpdateTime();
        InitializeComponent();
        
    }
    /// <summary>
    /// return the current time for the clock
    /// </summary>
    public DateTime CurrentDate
    {
        get { return (DateTime)GetValue(currentTime); }
        set { SetValue(currentTime, value); }
    }
    public static readonly DependencyProperty currentTime =
        DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(MainWindow));

    /// <summary>
    /// button of the manager"admin" 
    /// </summary>
    /// <param name="sender">button</param>
    /// <param name="e"> click</param>
    private void AdminMainWindow_Click(object sender, RoutedEventArgs e)
    {
        new AdminMainWindow().Show();
    }
    /// <summary>
    /// buttons to change the date:add year,hour,day and month
    /// </summary>
    /// <param name="sender">button</param>
    /// <param name="e"> click</param>

    private void ChangeDate(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            switch (button.Content)
            {
                case "Add Day":
                    CurrentDate = CurrentDate.AddDays(1);
                    break;
                case "Add Hour":
                    CurrentDate = CurrentDate.AddHours(1);
                    break;
                case "Add Month":
                    CurrentDate = CurrentDate.AddMonths(1);
                    break;
                case "Add Year":
                    CurrentDate = CurrentDate.AddYears(1);
                    break;
                default:
                    return;
            }
        }
    }
    /// <summary>
    /// Advances the seconds on the clock automatically
    /// </summary>
    private void UpdateTime()
    {
        new Thread(() =>
        {
            while (isClose)
            {
                Application.Current.Dispatcher.Invoke(() => { CurrentDate = CurrentDate.AddSeconds(1); });
                Thread.Sleep(1000);
            }
        }).Start();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        isClose = false;

        base.OnClosing(e);
    }
    /// <summary>
    /// Manages the engineer's user login using an ID stored in the system
    /// </summary>
    /// <param name="sender"> button</param>
    /// <param name="e"> click</param>

    private void EngineersMainW_click(object sender, RoutedEventArgs e)
    {
        try
        {
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Please enter your Id:", "Enter Id", "273044483");
            int result = int.Parse(userInput);                              //convert string to int
            BO.Engineer engineer = s_bl.Engineer.Read(result);              //return the engineer that have this id(if its null,throw an exception that this id does not exit)
            new EngineersMainW(result).Show();                        //open the engineer window because the id was correct
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}