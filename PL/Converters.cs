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
