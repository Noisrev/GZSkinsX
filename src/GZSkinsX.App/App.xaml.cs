// Copyright 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "LICENSE.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

using System;
using System.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using CommunityToolkit.Diagnostics;

using GZSkinsX.Contracts.Appx;
using GZSkinsX.Contracts.Extension;
using GZSkinsX.Contracts.WindowManager;

using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;

namespace GZSkinsX;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public sealed partial class App : Application
{
    /// <summary>
    /// The static member instance of <see cref="DesktopExtension.DesktopExtensionMethods"/>
    /// </summary>
    public static DesktopExtension.DesktopExtensionMethods DesktopExtensionMethods { get; } = new();

    /// <summary>
    /// Gets the <see cref="App"/> object for the current application.
    /// </summary>
    public static new App Current => (App)Application.Current;

    /// <summary>
    /// Use the <see cref="Lazy{T}"/> to initialize the services for the current application.
    /// </summary>
    private static Lazy<Task> InitializeServiceAsync { get; } = new(async () =>
    {
        var configuration = new ContainerConfiguration();
        configuration.WithAssemblies(new Assembly[]
        {
            // Main Application
            typeof(App).Assembly,
            // GZSkinsX.Contracts
            typeof(AppxContext).Assembly,
            // GZSkinsX.Services
            typeof(Services.AppxServices).Assembly
        });

        var compositionHost = configuration.CreateContainer();
        AppxContext.InitializeLifetimeService(compositionHost);

        await Services.Logging.LoggerImpl.Shared.InitializeAsync();

        var gameService = AppxContext.GameService;
        var rootFolder = await gameService.TryGetRootFolderAsync();
        await gameService.TryUpdateAsync(rootFolder, gameService.CurrentRegion);

        var windowManagerService = AppxContext.WindowManagerService;
        windowManagerService.NavigateTo(WindowFrameConstants.Index_Guid);

        // Load extensions
        foreach (var item in compositionHost.GetExports<Lazy<IAdvanceExtension, AdvanceExtensionMetadataAttribute>>())
        {
            _ = item.Value;
        }

        AppxContext.LoggingService.LogAlways("Application: Initialized successfully.");
    });

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
        SetRandomWindowText();
    }

    /// <summary>
    /// Set random window title to avoid detection
    /// </summary>
    private void SetRandomWindowText()
    {
        unsafe
        {
            var mainResourceMap = ResourceManager.Current.MainResourceMap;
            fixed (char* displayName = mainResourceMap.GetValue("Resources/AppDisplayName").ValueAsString)
            {
                var windowHandle = FindWindow(null, displayName);
                if (windowHandle != IntPtr.Zero)
                {
                    DesktopExtensionMethods.SetWindowText(windowHandle.ToInt64(), Guid.NewGuid().ToHexString().Substring(2));
                }
            }
        }
    }

    /// <summary>
    /// Initializes the app service on the host process 
    /// </summary>
    protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
    {
        base.OnBackgroundActivated(args);

        if (DesktopExtensionMethods.OnBackgroundActivated(args))
        {
            await DesktopExtensionMethods.SetOwner(Process.GetCurrentProcess().Id);
        }
    }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="e">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs e)
    {
        await InitializeServiceAsync.Value;
        if (e.PrelaunchActivated == false)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsMethodPresent("Windows.ApplicationModel.Core.CoreApplication", "EnablePrelaunch"))
            {
                CoreApplication.EnablePrelaunch(true);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        if (Window.Current.Content is FrameworkElement frameworkElement)
        {
            frameworkElement.RequestedTheme = AppxContext.ThemeService.CurrentTheme;
        }
    }

    [DllImport("ext-ms-win-ntuser-window-l1-1-0.dll", ExactSpelling = true, EntryPoint = "FindWindowW", SetLastError = true)]
    internal static extern unsafe IntPtr FindWindow([In, Optional] char* lpClassName, [In, Optional] char* lpWindowName);
}
