using Microsoft.UI.Xaml.Markup;
namespace CustomExtensions.WinUI;


[MarkupExtensionReturnType(ReturnType = typeof(Uri))]
internal sealed class PluginUriExtension : MarkupExtension
{
	public string Source { get; set; }

	protected override object ProvideValue()
	{
		return new Uri(Source.PluginPath());
	}
}
