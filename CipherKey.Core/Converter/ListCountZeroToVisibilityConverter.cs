using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CipherKey.Core.Converter
{
	public class ListCountZeroToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			IEnumerable<object> objects = value as IEnumerable<object>;
			if(objects == null || objects.Count() == 0)
			{
				return System.Windows.Visibility.Visible;
			}
			else
			{
				return System.Windows.Visibility.Collapsed;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
