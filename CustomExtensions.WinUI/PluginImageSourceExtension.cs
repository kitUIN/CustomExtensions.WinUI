using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
namespace CustomExtensions.WinUI;


[MarkupExtensionReturnType(ReturnType = typeof(ImageSource))]
public sealed class PluginImageSourceExtension : MarkupExtension
{
	public string Source { get; set; }

	protected override object ProvideValue()
	{
		return new BitmapImage(new Uri(Source.PluginPath()));
	}
}
