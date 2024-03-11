using PL.Admin;
using PL.Engineer;
using System.Windows;
using System.Windows.Controls;

namespace PL;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
    public MainWindow()
    {
        CurrentDate = DateTime.Now;
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

    /// <summary>
    /// "data intiization" click
    /// </summary>
    /// <param name="sender"> button</param>
    /// <param name="e"> click</param>
    /// 

}