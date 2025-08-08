using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Pomni.Converters
{
    public class NoteContentTrimmerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            string text = value.ToString();
            int maxLen = 55;

            if (parameter != null && int.TryParse(parameter.ToString(), out int customMaxLen))
                maxLen = customMaxLen;

            return text.Length <= maxLen ? text : text.Substring(0, maxLen) + "...";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
