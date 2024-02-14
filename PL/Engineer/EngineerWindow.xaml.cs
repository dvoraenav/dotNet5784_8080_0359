using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
    public EngineerWindow(int id = 0)
    {
        
        InitializeComponent();
        if (id == 0)
        { BO.Engineer engineer = new BO.Engineer();
           CurrentEngineer=engineer;
        }
        else
        {
            try
            {
                BO.Engineer engineer1 = s_bl.Engineer.Read(id);
                CurrentEngineer = engineer1;
            }
            catch (Exception ex)//TODO what exception??
            { }
        }
    }
        public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    public BO.EngineerExpireance Expireance { get; set; } = BO.EngineerExpireance.Beginner;
    private void ExpirienceSelection(object sender, SelectionChangedEventArgs e)
    {
        CurrentEngineer.Level = Expireance;

    }

    private void AddNewEngineer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Engineer.Create(CurrentEngineer);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }//TODO
    }

    private void UpdateEngineer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            s_bl.Engineer.Update(CurrentEngineer);
        }
        catch(Exception ex) { MessageBox.Show(ex.Message); }

    }
}
