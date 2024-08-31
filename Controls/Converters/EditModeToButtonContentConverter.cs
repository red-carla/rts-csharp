using System.Globalization;
using System.Windows.Data;

namespace RTS.Controls.Converters;

public class EditModeToButtonContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isEditing = (bool)value;
        return isEditing ? "Cancel" : "Edit";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}