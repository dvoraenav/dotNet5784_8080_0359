using BO;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static BO.TaskStatus;

namespace PL;
/// <summary>
/// to show the text of add if this option selected and hide if not
/// </summary>

class ConvertIdToVisibilityOfAdd : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
/// <summary>
/// to show the text of update if this option selected and hide if not
/// </summary>
class ConvertIdToVisibilityOfUpdate : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? Visibility.Hidden : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertStatusToColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (value is BO.TaskStatus status)
            {
                switch (status)
                {
                    case BO.TaskStatus.Unscheduled:
                        return Brushes.LightGray;
                    case BO.TaskStatus.Scheduled:
                        return Brushes.LightGoldenrodYellow;
                    case BO.TaskStatus.OnTrack:
                        return Brushes.Yellow;
                    case BO.TaskStatus.Done:
                        return Brushes.Orange;
                    case BO.TaskStatus.Late:
                        return Brushes.Red;
                    default: return Brushes.Black;
                }
            }
        }
        catch { }

        return Brushes.Black;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ConvertDependencyTocColor : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            IEnumerable<TaskInList> dependencies = (IEnumerable<TaskInList>)values[0];
            TaskInList task = (TaskInList)values[1];

            if (dependencies is null)
                return Brushes.Transparent;
            if (dependencies.Any(x => x.Id == task.Id))
                return Brushes.Green;
            return Brushes.Transparent;
        }
        catch { } return Brushes.Transparent;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
