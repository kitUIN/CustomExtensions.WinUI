﻿using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;

namespace CustomExtensions.WinUI;

internal partial class ApplicationExtensionHostSingleton<T> : IApplicationExtensionHost where T : Application
{
	private readonly T Application;
	private readonly ConcurrentDictionary<string, IExtensionAssembly> AssembliesByPath = new();
	private readonly ConcurrentDictionary<string, IExtensionAssembly> AssembliesByAssemblyName = new();

	public Assembly EntryAssembly { get; }
	public string HostingProcessDir { get; }

	public ApplicationExtensionHostSingleton(T application)
	{
		Application = application;
		EntryAssembly = Assembly.GetEntryAssembly().AssertDefined();
		HostingProcessDir = Path.GetDirectoryName(EntryAssembly.AssertDefined().Location).AssertDefined();
	}

	public async Task<IExtensionAssembly> LoadExtensionAsync(string pathToAssembly)
	{
		IExtensionAssembly asm = GetExtensionAssembly(pathToAssembly);
		await asm.LoadAsync();
		return asm;
	}

	public Uri LocateResource(object component, [CallerFilePath] string callerFilePath = "")
	{
		IExtensionAssembly extensionAsm = GetExtensionAssembly(component.GetType().Assembly.GetName());
		return extensionAsm.LocateResource(component, callerFilePath);
	}

	private IExtensionAssembly GetExtensionAssembly(AssemblyName assemblyName)
	{
		return !AssembliesByAssemblyName.TryGetValue(assemblyName.FullName, out IExtensionAssembly? extensionAssembly)
			? throw new EntryPointNotFoundException()
			: extensionAssembly;
	}
	private IExtensionAssembly GetExtensionAssembly(string pathToAssembly)
	{
		FileInfo fi = new(pathToAssembly);
		IExtensionAssembly asm = AssembliesByPath.GetOrAdd(fi.FullName, asm => new ExtensionAssembly(pathToAssembly));
		_ = AssembliesByAssemblyName.AddOrUpdate(asm.ForeignAssembly.GetName().FullName, asm, (_, _) => asm);
		return asm;
	}
	public string LocateResourcePrefix(AssemblyName assemblyName)
	{
		return GetExtensionAssembly(assemblyName).SourceResourcePrefix;
	}
}
