using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace IslandLanding.Converters
{
 public class ImageConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string ButtonImage = "volume_off_24px.png";

      if (value is string)
      {
        var imageValue = (string)value;
        if (!string.IsNullOrEmpty(imageValue))
        {
          ButtonImage = imageValue;
        }
      }

      return ButtonImage;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value;
    }
  }
}