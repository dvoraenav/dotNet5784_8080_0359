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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>

public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

    public EngineerListWindow()
    {
        InitializeComponent();
        try
        {
            EngineerList = s_bl?.Engineer.GetEngineerList()!;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    /// <summary>
    /// to represent the engineer list
    /// </summary>
    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProporty); }
        set { SetValue(EngineerListProporty, value); }
    }

    public static readonly DependencyProperty EngineerListProporty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
    /// <summary>
    /// add new field to select("all" means that we want to select all the engineer)
    /// </summary>
    public BO.EngineerExpireance Experience { get; set; } = BO.EngineerExpireance.All;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">combobox </param>
    /// <param name="e">event of select option from all the expirience levels</param>

    private void ExperienceSelection(object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (Experience == BO.EngineerExpireance.All) ?
    s_bl.Engineer.GetEngineerList()! : s_bl?.Engineer.GetEngineerList(eg => eg.Level == Experience)!;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"> button of add angineer to list</param>
    /// <param name="e">  event of add</param>
    private void AddEngineer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            new EngineerWindow().ShowDialog();
            EngineerList = s_bl?.Engineer.GetEngineerList()!;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }

    }
    /// <summary>
    /// return the selected engineer
    /// </summary>

    public BO.Engineer Selected_Engineer { get; set; } = new BO.Engineer();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"> combobox</param>
    /// <param name="e"> mouse button</param>
    private void EngineerSelection(object sender, MouseButtonEventArgs e)
    {
        try
        {
            BO.Engineer? engineerlist = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(engineerlist!.Id).ShowDialog();// create new engineer window in adding condition and prevent return to the privius window until it is closed
            EngineerList = s_bl?.Engineer.GetEngineerList()!;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }

    }
    /// <summary>
    /// filter the list by searching chars in the engineer's name
    /// </summary>
    /// <param name="sender"> textbox of search</param>
    /// <param name="e">entering a text to search/filter </param>
    private void Search_TaskChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            EngineerList = s_bl?.Engineer.GetEngineerList()!.Where(X => X.FullName!.Contains((sender as TextBox)!.Text))?.ToList()!;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}

