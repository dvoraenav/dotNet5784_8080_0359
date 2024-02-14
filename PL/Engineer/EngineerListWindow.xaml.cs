﻿using System;
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
        EngineerList = s_bl?.Engineer.GetEngineerList()!;
    }
    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProporty); }
        set { SetValue(EngineerListProporty, value); }
    }

    public static readonly DependencyProperty EngineerListProporty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

    public BO.EngineerExpireance Expireance { get; set; } = BO.EngineerExpireance.All;

    private void ExpirienceSelection (object sender, SelectionChangedEventArgs e)
    {
        EngineerList = (Expireance == BO.EngineerExpireance.All) ?
    s_bl.Engineer.GetEngineerList()! : s_bl?.Engineer.GetEngineerList(eg => eg.Level == Expireance)!;

    }

    private void AddEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().Show();
        EngineerList = s_bl?.Engineer.GetEngineerList()!;
    }

    public BO.Engineer Selected_Engineer { get; set; } 
    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        //BO.Engineer? engineerlist = (sender as ListView)?.SelectedItem as BO.Engineer;
        //new EngineerWindow(engineerlist.Id).ShowDialog();//TODO 
        new EngineerWindow(Selected_Engineer.Id).ShowDialog();
       // EngineerList = s_bl?.Engineer.GetEngineerList()!;
    }

}
