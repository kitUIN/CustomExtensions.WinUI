using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Imaging;
using WinRT;

namespace CustomExtensions.WinUI;

public static class WinUIExtensions
{
	public static void LoadComponent<T>(this T component, ref bool contentLoaded,
		[CallerFilePath] string callerFilePath = "") where T : IWinRTObject
	{
		if (contentLoaded) return;

		contentLoaded = true;

		var resourceLocator = ApplicationExtensionHost.Current.LocateResource(component, callerFilePath);
		Application.LoadComponent(component, resourceLocator, ComponentResourceLocation.Nested);
	}

	public static string AssetPath(this string path,int skip = 2)
	{
		var callerMethod = new StackFrame(skip, false).GetMethod();
		if (callerMethod != null && callerMethod.DeclaringType != null)
			return ApplicationExtensionHost.Current.LocateResourcePrefix(
				callerMethod.DeclaringType.Assembly.GetName()) + path;
		return path;
	}

	public static string AssetPath(this string path, Type type)
	{
		return ApplicationExtensionHost.Current.LocateResourcePrefix(type.Assembly.GetName()) + path;
	}

	public static string AssetPath<T>(this string path, T t)
	{
		if (t == null) return path;
		return ApplicationExtensionHost.Current.LocateResourcePrefix(t.GetType().Assembly.GetName()) + path;
	}

	public static string PluginPath(this string path)
	{
		if (string.IsNullOrEmpty(path)) return path;
		if (path.StartsWith("ms-plugin://"))
		{
			var uri = new Uri(path);
			var host = uri.Host;
			if (string.IsNullOrEmpty(host)) return "ms-appx://" + uri.AbsolutePath;
			return ApplicationExtensionHost.Current.LocateResourcePrefix(host) + uri.AbsolutePath;
		}
		return path;
	}
}