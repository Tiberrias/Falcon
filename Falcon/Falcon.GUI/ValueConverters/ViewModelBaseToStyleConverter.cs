using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Falcon.GUI.ViewModel;

namespace Falcon.GUI.ValueConverters
{
    public class ViewModelBaseToStyleConverter : IValueConverter
    {
        public Style HighlightedStyle { get; set; }

        public Style NormalStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string))
            {
                return NormalStyle;
            }

            if ((value is ImportLoadoutsViewModel && (string)parameter == "Import") ||
                (value is SelectLoadoutsViewModel && (string)parameter == "Select") ||
                (value is MissionLoadoutEditorViewModel && (string)parameter == "Edit"))
            {
                return HighlightedStyle;
            }
            return NormalStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}