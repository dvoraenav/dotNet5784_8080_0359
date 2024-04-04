using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;//static field
    public EngineerWindow(int id = 0)//defult id parameter
    {
        UpdateId = id == 0;
        InitializeComponent();
        if (id == 0)//it means that we are in adding condition
        { BO.Engineer engineer = new BO.Engineer();//create new engineer
           CurrentEngineer=engineer;//the current engineer
        }
        else//we want to update
        {
            try
            {
                BO.Engineer engineer1 = s_bl.Engineer.Read(id);//all the details of this id
                CurrentEngineer = engineer1;
            }
            catch (Exception ex)//TODO what exception??
            { MessageBox.Show(ex.Message); }
        }
    }/// <summary>
     /// to represent a specific engineer from Bo
     /// </summary>
    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }
   
    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
    /// <summary>
    ///return the level of expiriance
    /// </summary>
    public BO.EngineerExpireance Expireance { get; set; } = BO.EngineerExpireance.Beginner;//defult level is begineer
    /// <summary>
    /// to select the level of the engineer
    /// </summary>
    /// <param name="sender"> combobox</param>
    /// <param name="e">select level </param>
    private void ExpirienceSelection(object sender, SelectionChangedEventArgs e)
    {
        CurrentEngineer.Level = Expireance;   //the level that selected
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"> button of add engineer</param>
    /// <param name="e">Contains state information and event data associated with the adding</param>
    private void AddNewEngineer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Engineer.Create(CurrentEngineer);
            this.Close();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
       
    }
    /// <summary>
    /// update the engineer details
    /// </summary>
    /// <param name="sender"> button of update</param>
    /// <param name="e">Contains state information and event data associated with the update </param>
    private void UpdateEngineer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Engineer.Update(CurrentEngineer);//calling to the update method
            this.Close();//auto closing window
        }
        catch(Exception ex) { MessageBox.Show(ex.Message); }

    }
    /// <summary>
    /// to delete engineer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    private void Delete_click(object sender, RoutedEventArgs e)
    {
        try {
            MessageBoxResult result = MessageBox.Show("Would you like to delete this engineer?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
            if (result == MessageBoxResult.Yes) { s_bl.Engineer.Delete(CurrentEngineer.Id); }
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }

    /// <summary>
    /// to know if we add engineer or update it because we don't allow to adit the id
    /// </summary>
    public bool UpdateId 
    {
        get { return (bool)GetValue(changeId); }
        set { SetValue(changeId, value); }
    }
    
    public static readonly DependencyProperty changeId =
        DependencyProperty.Register("UpdateId", typeof(bool), typeof(EngineerWindow));
}
