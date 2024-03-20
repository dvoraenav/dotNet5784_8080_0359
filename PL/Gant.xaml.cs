using BlApi;
using BO;
using System.Collections.Generic;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for Gant.xaml
/// </summary>
public partial class Gant : Window
{
    private readonly IBl bl = Factory.Get;
        public IEnumerable<TaskInGantt> GanttList
    {
        get { return (IEnumerable<TaskInGantt>)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("GanttList", typeof(IEnumerable<TaskInGantt>), typeof(Gant));



    public Gant()
    {
        GanttList = bl.Task.GanttList();
        InitializeComponent();
    }
}
