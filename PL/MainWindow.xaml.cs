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

    private void EngineerListWindow_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
       
    }

    private void Initialization_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to create Initial data?", "Data Initialization", MessageBoxButton.YesNo ,MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
        if (result == MessageBoxResult.Yes) {s_bl.InitializeDB(); }
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to reset the data?", "Reset Data", MessageBoxButton.YesNoCancel);
        if (result == MessageBoxResult.Yes) { s_bl.ResetDB(); }
    }
}