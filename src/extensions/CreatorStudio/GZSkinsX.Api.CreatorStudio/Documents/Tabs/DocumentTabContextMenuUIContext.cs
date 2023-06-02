﻿// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

using GZSkinsX.SDK.ContextMenu;

using Microsoft.UI.Xaml.Controls;

namespace GZSkinsX.SDK.CreatorStudio.Documents.Tabs;

public sealed class DocumentTabContextMenuUIContext : ContextMenuUIContext<TabViewItem?, IDocumentTab?>
{
    public DocumentTabContextMenuUIContext(TabViewItem? uiObject, IDocumentTab? parameter)
        : base(uiObject, parameter) { }
}
