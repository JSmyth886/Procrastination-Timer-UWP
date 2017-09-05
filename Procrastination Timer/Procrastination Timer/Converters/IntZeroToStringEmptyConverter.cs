using System;
using Windows.UI.Xaml.Data;

namespace Procrastination_Timer.Converters
{
  public class IntZeroToStringEmptyConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (value as int?).Equals(0) ? string.Empty : value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      return value;
    }
  }
}
