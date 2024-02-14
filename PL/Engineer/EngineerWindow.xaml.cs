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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

    public EngineerWindow(int id = 0)
    {
        InitializeComponent();
        if (id == 0)
        { BO.Engineer engineer = new BO.Engineer(); }
        else
        { BO.Engineer engineer1 = s_bl.Engineer.Read(id); }
       
    }
        public IEnumerable<BO.Engineer> CurrentEngineer
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));


    }
