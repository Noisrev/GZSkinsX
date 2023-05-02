﻿// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

using System;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using GZSkinsX.Api.AccessCache;
using GZSkinsX.Api.Appx;
using GZSkinsX.Api.Game;
using GZSkinsX.Api.MRT;

namespace GZSkinsX.Extensions.CreatorStudio.AssetsExplorer;

[Shared, Export]
internal sealed partial class AssetsExplorerService
{
    private readonly IFutureAccessService _futureAccessService;
    private readonly IMRTCoreService _mrtCoreService;
    private readonly IGameService _gameService;

    private AssetsExplorerContainer? _rootContainer;

    public AssetsExplorerService()
    {
        _mrtCoreService = AppxContext.MRTCoreService;
        _futureAccessService = AppxContext.FutureAccessService;
        _gameService = AppxContext.ServiceLocator.Resolve<IGameService>();

        _loading = new();
        _rootGrid = new();
        _treeView = new();
        _refreshButton = new();
        _collapseButton = new();

        InitializeUIObject();
        InitializeEvents();
    }

    private async Task InitializeContainerAsync()
    {
        var rootDirectory = new DirectoryInfo(_gameService.RootDirectory);

        var dataFolder = Path.Combine(_gameService.GameData.GameDirectory, "DATA").ToLower();
        var pluginsFolder = Path.Combine(_gameService.GameData.LCUDirectory, "Plugins").ToLower();

        var allFiles = rootDirectory.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(file =>
            {
                var fullPath = file.FullName.ToLower();
                return fullPath.Contains(dataFolder) || fullPath.Contains(pluginsFolder);
            })
            .OrderBy(file => file.FullName);

        var filteredFiles = allFiles.Where(a => IsSupportedFileType(a.Extension));
        var rootPathLength = rootDirectory.FullName.Length + 1; // + DirectorySeparatorChar

        _rootContainer = new AssetsExplorerContainer(rootDirectory);
        foreach (var item in allFiles)
        {
            var parts = item.FullName[rootPathLength..].Split(Path.DirectorySeparatorChar);
            var currentContainer = _rootContainer;

            for (var i = 0; i < parts.Length - 1; i++)
            {
                var childContainer = currentContainer.GetChild<AssetsExplorerContainer>(parts[i]);
                if (childContainer is null)
                {
                    var directory = item.Directory;
                    for (var j = parts.Length - 2; j > i; j--)
                    {
                        directory = directory.Parent;
                    }

                    childContainer = new AssetsExplorerContainer(directory);
                    currentContainer.AddChild(childContainer);
                }

                currentContainer = childContainer;
            }

            currentContainer.AddChild(new AssetsExplorerFile(item));
        }

        if (_treeView.Dispatcher.HasThreadAccess)
            LoadNodesFromContainer(_rootContainer);
        else
            await _treeView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => LoadNodesFromContainer(_rootContainer));
    }

    private static bool IsSupportedFileType(string fileNameWithExtension) => fileNameWithExtension switch
    {
        ".cfg" or ".ini" or ".json" or ".yaml" or ".wad" or ".client" => true,
        _ => false
    };
}
