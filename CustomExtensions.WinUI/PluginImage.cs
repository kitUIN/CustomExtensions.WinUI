using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media.Imaging;

namespace CustomExtensions.WinUI;

[MarkupExtensionReturnType(ReturnType = typeof(BitmapImage))]
public class PluginImage : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="string"/> representing the image to display.
	/// </summary>
	public string Source { get; set; }
	
	/// <inheritdoc/>
	protected override object ProvideValue()
	{
		var path = Source.AssetPath(7);
		return new BitmapImage
		{
			UriSource = new Uri(path)
		};
	}
}