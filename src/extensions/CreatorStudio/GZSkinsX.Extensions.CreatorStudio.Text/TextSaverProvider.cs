﻿// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Composition;

using GZSkinsX.SDK.CreatorStudio.Documents.Tabs;

namespace GZSkinsX.Extensions.CreatorStudio.Text;

[Shared, ExportTabSaverProvider]
[TabSaverProviderMetadata(TypedGuid = "2E464B27-C4BC-4819-A8C4-DCF622ED4863")]
internal sealed class TextSaverProvider : ITabSaverProvider
{
    public ITabSaver Create(IDocumentTab tab)
    {
        return new TextSaver(tab);
    }
}
