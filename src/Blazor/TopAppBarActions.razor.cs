// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor child component that acts as a container for action items.
    /// </summary>
    public partial class TopAppBarActions
    {
        /// <summary>
        /// Whether to show all actions on all device sizes. Default is to hide all but first on small devices.
        /// </summary>
        [Parameter] public bool ShowActionsAlways { get; set; }
        [Parameter] public EventCallback<bool> ShowActionsAlwaysChanged { get; set; }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarActions = this;
        }
    }
}