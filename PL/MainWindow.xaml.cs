using PL.Admin;
using PL.Engineer;
using System.Windows;

namespace PL;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void AdminMainWindow_Click(object sender, RoutedEventArgs e)
    {
        new AdminMainWindow().Show();
       
    }

    private void Gantt_window(object sender, RoutedEventArgs e)=> new Gant().Show();

    /// <summary>
    /// "data intiization" click
    /// </summary>
    /// <param name="sender"> button</param>
    /// <param name="e"> click</param>

}