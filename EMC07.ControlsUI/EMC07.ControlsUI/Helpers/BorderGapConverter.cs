using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace EMC07.ControlsUI.Helpers
{
    internal class BorderGapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Логика конвертации для создания нужного вам эффекта border gap.
            // Возвращайте экземпляр объекта, реализующего интерфейс IBrush

            // Пример кода для создания SolidColorBrush
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
