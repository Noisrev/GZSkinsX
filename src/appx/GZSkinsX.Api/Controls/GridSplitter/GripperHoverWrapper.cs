// Copyright 2022 - 2023 GZSkins, Inc. All rights reserved.
// Licensed under the Mozilla Public License, Version 2.0 (the "License.txt").
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

#nullable enable

using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace GZSkinsX.SDK.Controls;

internal sealed class GripperHoverWrapper
{
    private readonly GridSplitter.GridResizeDirection _gridSplitterDirection;

    private CoreCursor? _splitterPreviousPointer;
    private CoreCursor? _previousCursor;
    private GridSplitter.GripperCursorType _gripperCursor;
    private int _gripperCustomCursorResource;
    private bool _isDragging;
    private UIElement _element;

    internal GridSplitter.GripperCursorType GripperCursor
    {
        get => _gripperCursor;
        set => _gripperCursor = value;
    }

    internal int GripperCustomCursorResource
    {
        get => _gripperCustomCursorResource;
        set => _gripperCustomCursorResource = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GripperHoverWrapper"/> class that add cursor change on hover functionality for GridSplitter.
    /// </summary>
    /// <param name="element">UI element to apply cursor change on hover</param>
    /// <param name="gridSplitterDirection">GridSplitter resize direction</param>
    /// <param name="gripperCursor">GridSplitter gripper on hover cursor type</param>
    /// <param name="gripperCustomCursorResource">GridSplitter gripper custom cursor resource number</param>
    internal GripperHoverWrapper(UIElement element, GridSplitter.GridResizeDirection gridSplitterDirection, GridSplitter.GripperCursorType gripperCursor, int gripperCustomCursorResource)
    {
        _gridSplitterDirection = gridSplitterDirection;
        _gripperCursor = gripperCursor;
        _gripperCustomCursorResource = gripperCustomCursorResource;
        _element = element;
        UnhookEvents();
        _element.PointerEntered += Element_PointerEntered;
        _element.PointerExited += Element_PointerExited;
    }

    internal void UpdateHoverElement(UIElement element)
    {
        UnhookEvents();
        _element = element;
        _element.PointerEntered += Element_PointerEntered;
        _element.PointerExited += Element_PointerExited;
    }

    private void Element_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        if (_isDragging)
        {
            // if dragging don't update the cursor just update the splitter cursor with the last window cursor,
            // because the splitter is still using the arrow cursor and will revert to original case when drag completes
            _splitterPreviousPointer = _previousCursor;
        }
        else
        {
            Window.Current.CoreWindow.PointerCursor = _previousCursor;
        }
    }

    private void Element_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        // if not dragging
        if (!_isDragging)
        {
            _previousCursor = _splitterPreviousPointer = Window.Current.CoreWindow.PointerCursor;
            UpdateDisplayCursor();
        }

        // if dragging
        else
        {
            _previousCursor = _splitterPreviousPointer;
        }
    }

    private void UpdateDisplayCursor()
    {
        if (_gripperCursor == GridSplitter.GripperCursorType.Default)
        {
            if (_gridSplitterDirection == GridSplitter.GridResizeDirection.Columns)
            {
                Window.Current.CoreWindow.PointerCursor = GridSplitter.s_columnsSplitterCursor;
            }
            else if (_gridSplitterDirection == GridSplitter.GridResizeDirection.Rows)
            {
                Window.Current.CoreWindow.PointerCursor = GridSplitter.s_rowSplitterCursor;
            }
        }
        else
        {
            var coreCursor = (CoreCursorType)(int)_gripperCursor;
            if (_gripperCursor == GridSplitter.GripperCursorType.Custom)
            {
                if (_gripperCustomCursorResource > GridSplitter.GripperCustomCursorDefaultResource)
                {
                    Window.Current.CoreWindow.PointerCursor = new CoreCursor(coreCursor, (uint)_gripperCustomCursorResource);
                }
            }
            else
            {
                Window.Current.CoreWindow.PointerCursor = new CoreCursor(coreCursor, 1);
            }
        }
    }

    internal void SplitterManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
    {
        if (sender is not GridSplitter splitter)
        {
            return;
        }

        _splitterPreviousPointer = splitter.PreviousCursor;
        _isDragging = true;
    }

    internal void SplitterManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
    {
        if (sender is not GridSplitter splitter)
        {
            return;
        }

        Window.Current.CoreWindow.PointerCursor = splitter.PreviousCursor = _splitterPreviousPointer;
        _isDragging = false;
    }

    internal void UnhookEvents()
    {
        if (_element == null)
        {
            return;
        }

        _element.PointerEntered -= Element_PointerEntered;
        _element.PointerExited -= Element_PointerExited;
    }
}
