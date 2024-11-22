using Microsoft.UI.Xaml.Data;
using System.Diagnostics;


namespace CustomExtensions.WinUI;

public partial class PluginPathConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		Trace.WriteLine(value.GetType().FullName);
		string path = (string)value;
		return path.PluginPath();
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return Microsoft.UI.Xaml.DependencyProperty.UnsetValue;
	}
}
