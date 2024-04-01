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

    bool isClose {  get; set; }
    public MainWindow()
    {
        isClose = true;
        CurrentDate = s_bl.Clock;
        UpdateTime();
        InitializeComponent();
    }
    public DateTime CurrentDate
    {
        get { return (DateTime)GetValue(currentTime); }
        set { SetValue(currentTime, value); }
    }
    public static readonly DependencyProperty currentTime = DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(MainWindow));


    private void AdminMainWindow_Click(object sender, RoutedEventArgs e)
    {
        new AdminMainWindow().Show();
    }

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

    private void EngineersMainW_click(object sender, RoutedEventArgs e)
    {
        new EngineersMainW().Show();
    }
}