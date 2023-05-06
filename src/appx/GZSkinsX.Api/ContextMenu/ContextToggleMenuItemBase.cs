// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

using Windows.UI.Xaml.Controls;

namespace GZSkinsX.Api.ContextMenu;

public abstract class ContextToggleMenuItemBase : IContextToggleMenuItem
{
    public string? Header { get; protected set; }

    public IconElement? Icon { get; protected set; }

    public ContextMenuItemHotKey? HotKey { get; protected set; }

    public virtual bool IsChecked(IContextMenuUIContext context) => false;

    public virtual bool IsEnabled(IContextMenuUIContext context) => true;

    public virtual bool IsVisible(IContextMenuUIContext context) => true;

    public abstract void OnExecute(IContextMenuUIContext context);

    public abstract void OnToggle(bool newValue, IContextMenuUIContext context);
}
