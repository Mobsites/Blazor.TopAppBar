// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
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

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> ShowActionsAlwaysChanged { get; set; }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarActions = this;
        }

        internal void SetOptions(TopAppBar.Options options)
        {
            options.ShowActionsAlways = this.ShowActionsAlways;
        }

        internal async Task CheckState(TopAppBar.Options options)
        {
            if (this.ShowActionsAlways != options.ShowActionsAlways)
            {
                await this.ShowActionsAlwaysChanged.InvokeAsync(options.ShowActionsAlways);
            }
        }
    }
}