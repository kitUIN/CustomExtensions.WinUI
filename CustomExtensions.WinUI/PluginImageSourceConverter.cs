using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;


namespace CustomExtensions.WinUI;

public partial class PluginImageSourceConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		string path = (string)value;
		return new BitmapImage(new Uri(path.PluginPath()));
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return Microsoft.UI.Xaml.DependencyProperty.UnsetValue;
	}
}
