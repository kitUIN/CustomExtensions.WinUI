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

	public static string AssetPath(this string path)
	{
		var callerMethod = new StackFrame(2, false)?.GetMethod();
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

	public static BitmapImage ImageSource(this string path)
	{
		return new BitmapImage(new Uri(AssetPath(path)));
	}
	public static BitmapImage ImageSource(this string path, Type type)
	{
		return new BitmapImage(new Uri(AssetPath(path, type)));
	}

	public static BitmapImage ImageSource<T>(this string path, T t)
	{
		return new BitmapImage(new Uri(AssetPath(path, t)));
	}
	public static Uri AssetUri(this string path)
	{
		return new Uri(AssetPath(path));
	}

	public static Uri AssetUri(this string path, Type type)
	{
		return new Uri(AssetPath(path, type));
	}

	public static Uri AssetUri<T>(this string path, T t)
	{
		return new Uri(AssetPath(path, t));
	}
}