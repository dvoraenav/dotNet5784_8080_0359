using BlApi;
using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        get { return (IEnumerable<TaskInGantt>)GetValue(GanttListProperty); }
        set { SetValue(GanttListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for OpenDialoge.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GanttListProperty =
        DependencyProperty.Register("GanttList", typeof(IEnumerable<TaskInGantt>), typeof(Gant));

    public List<List<string>> DepList
    {
        get { return (List<List<string>>)GetValue(DepListPro); }
        set { SetValue(DepListPro, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DepListPro =
        DependencyProperty.Register("DepList", typeof(List<List<string>>), typeof(Gant));



    public Gant() 
    {
        GanttList = bl.Task.GanttList();
        DepList= bl.Task.GanttList().Select(x=>x.DepndenciesNames).ToList()!;
        InitializeComponent();
    }
}
