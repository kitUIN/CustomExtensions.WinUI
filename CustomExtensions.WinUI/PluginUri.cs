using Microsoft.UI.Xaml.Markup;

namespace CustomExtensions.WinUI;

[MarkupExtensionReturnType(ReturnType = typeof(Uri))]
public class PluginUri : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="string"/> representing the image to display.
	/// </summary>
	public string Source { get; set; }
	
	/// <inheritdoc/>
	protected override object ProvideValue()
	{
		var path = Source.AssetPath(7);
		return new Uri(path);
	}
}