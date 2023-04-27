﻿// <auto-generated by AppxContext.Services.g.tt (t4 template file). />

// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

namespace GZSkinsX.Api.Appx;

public static partial class AppxContext
{
    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.AccessCache.IFutureAccessService"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.AccessCache.IFutureAccessService FutureAccessService
    {
        get => CheckAccess(ref s_futureAccessService);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.AccessCache.IMostRecentlyUsedService"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.AccessCache.IMostRecentlyUsedService MostRecentlyUsedService
    {
        get => CheckAccess(ref s_mostRecentlyUsedService);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Appx.IAppxWindow"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Appx.IAppxWindow AppxWindow
    {
        get => CheckAccess(ref s_appxWindow);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Appx.IAppxTitleBar"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Appx.IAppxTitleBar AppxTitleBar
    {
        get => CheckAccess(ref s_appxTitleBar);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Appx.IAppxTitleBarButton"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Appx.IAppxTitleBarButton AppxTitleBarButton
    {
        get => CheckAccess(ref s_appxTitleBarButton);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Logging.ILoggingService"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Logging.ILoggingService LoggingService
    {
        get => CheckAccess(ref s_loggingService);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.MRT.IMRTCoreService"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.MRT.IMRTCoreService MRTCoreService
    {
        get => CheckAccess(ref s_mRTCoreService);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Scripting.IServiceLocator"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Scripting.IServiceLocator ServiceLocator
    {
        get => CheckAccess(ref s_serviceLocator);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Settings.ISettingsService"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Settings.ISettingsService SettingsService
    {
        get => CheckAccess(ref s_settingsService);
    }

    /// <summary>
    /// 获取全局静态共享的 <see cref="global::GZSkinsX.Api.Themes.IThemeService"/> 对象实例
    /// </summary>
    public static global::GZSkinsX.Api.Themes.IThemeService ThemeService
    {
        get => CheckAccess(ref s_themeService);
    }

    /// <summary>
    /// 检查和获取指定导出类型的成员对象
    /// </summary>
    /// <typeparam name="T">需要获取对象导出类型</typeparam>
    /// <param name="service">需要检查的成员对象</param>
    /// <returns>已获取的非空的 <typeparamref name="T"/> 类型对象实例</returns>
    /// <exception cref="global::System.InvalidOperationException">当应用程序未初始化，或找不到指定 <typeparamref name="T"/> 导出类型的对象时发生</exception>
    private static T CheckAccess<T>([global::System.Diagnostics.CodeAnalysis.NotNull] ref T? service) where T : class
    {
        if (s_serviceLocator is null)
        {
            throw new global::System.InvalidOperationException("The main app is not initialized!");
        }

        return service ??= s_serviceLocator.Resolve<T>();
    }
    
    private static global::GZSkinsX.Api.AccessCache.IFutureAccessService? s_futureAccessService;
    private static global::GZSkinsX.Api.AccessCache.IMostRecentlyUsedService? s_mostRecentlyUsedService;
    private static global::GZSkinsX.Api.Appx.IAppxWindow? s_appxWindow;
    private static global::GZSkinsX.Api.Appx.IAppxTitleBar? s_appxTitleBar;
    private static global::GZSkinsX.Api.Appx.IAppxTitleBarButton? s_appxTitleBarButton;
    private static global::GZSkinsX.Api.Logging.ILoggingService? s_loggingService;
    private static global::GZSkinsX.Api.MRT.IMRTCoreService? s_mRTCoreService;
    private static global::GZSkinsX.Api.Scripting.IServiceLocator? s_serviceLocator;
    private static global::GZSkinsX.Api.Settings.ISettingsService? s_settingsService;
    private static global::GZSkinsX.Api.Themes.IThemeService? s_themeService;
}
