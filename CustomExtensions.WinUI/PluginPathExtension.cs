using Microsoft.UI.Xaml.Markup;
namespace CustomExtensions.WinUI;


[MarkupExtensionReturnType(ReturnType = typeof(string))]
internal sealed class PluginPathExtension : MarkupExtension
{
    public string Source { get; set; }

	protected override object ProvideValue()
	{
		return Source.PluginPath();
	}
}
