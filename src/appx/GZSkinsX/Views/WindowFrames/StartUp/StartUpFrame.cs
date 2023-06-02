﻿// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Composition;

using GZSkinsX.SDK.Appx;
using GZSkinsX.SDK.Game;
using GZSkinsX.SDK.WindowManager;

namespace GZSkinsX.Views.WindowFrames.StartUp;

[Shared, ExportWindowFrame]
[WindowFrameMetadata(Guid = WindowFrameConstants.StartUp_Guid, PageType = typeof(StartUpPage))]
internal sealed class StartUpFrame : IWindowFrame
{
    private readonly IGameService _gameService;
    private readonly IWindowManagerService _windowManagerService;

    public StartUpFrame()
    {
        _gameService = AppxContext.Resolve<IGameService>();
        _windowManagerService = AppxContext.Resolve<IWindowManagerService>();
    }

    public bool CanNavigateTo(WindowFrameNavigatingEvnetArgs args)
    {
        if (_gameService.TryUpdate(_gameService.RootDirectory, _gameService.CurrentRegion))
        {
            _windowManagerService.NavigateTo(WindowFrameConstants.NavigationRoot_Guid);
            return false;
        }
        else
        {
            // IsInvalid
            args.Parameter = _gameService.CurrentRegion != GameRegion.Unknown ||
                string.IsNullOrEmpty(_gameService.RootDirectory) is false;

            return true;
        }
    }
}

