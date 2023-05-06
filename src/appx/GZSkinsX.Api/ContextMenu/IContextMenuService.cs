// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

using Windows.UI.Xaml.Controls;

namespace GZSkinsX.Api.ContextMenu;

/// <summary>
/// 表示上下文菜单的基本服务，并提供创建上下文菜单的能力
/// </summary>
public interface IContextMenuService
{
    /// <summary>
    /// 通过指定的 <paramref name="ownerGuidString"/> 创建一个新的 <see cref="MenuFlyout"/> 实现
    /// </summary>
    /// <param name="ownerGuidString">子菜单项所归属的 <see cref="System.Guid"/> 字符串值</param>
    /// <param name="coerceValueCallback">目标 UI 上下文的回调委托</param>
    /// <returns>已创建的 <see cref="MenuFlyout"/> 类型实例</returns>
    MenuFlyout CreateContextFlyout(string ownerGuidString, CoerceContextMenuUIContextCallback? coerceValueCallback = null);
}
