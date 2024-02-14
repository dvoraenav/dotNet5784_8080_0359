using PL.Engineer;
using System.Windows;

namespace PL;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
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
        //add a message box
        //TODO how to initializat data?
    }
}