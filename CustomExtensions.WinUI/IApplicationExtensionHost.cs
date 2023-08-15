using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Markup;

namespace CustomExtensions.WinUI;

public interface IApplicationExtensionHost
{
	Task<IExtensionAssembly> LoadExtensionAsync(string pathToAssembly);
	IDisposable RegisterXamlTypeMetadataProvider(IXamlMetadataProvider provider);
	Uri LocateResource(object component, [CallerFilePath] string callerFilePath = "");
	string LocateResourcePrefix(AssemblyName assemblyName);
}
