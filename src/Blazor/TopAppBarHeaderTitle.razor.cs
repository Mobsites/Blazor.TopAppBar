// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI child component for rendering the application title in the <see cref="TopAppBarHeader" /> component.
    /// </summary>
    public sealed partial class TopAppBarHeaderTitle
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Whether to hide title on small devices.
        /// </summary>
        [Parameter] public bool HideOnSmallDevices { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> HideOnSmallDevicesChanged { get; set; }



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Life cycle method for when parameters from parent are set.
        /// </summary>
        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.Title = this;
        }

        /// <summary>
        /// Set values on options that need to be maintained when keeping state.
        /// </summary>
        internal void SetOptions(TopAppBar.Options options)
        {
            options.HideTitleOnSmallDevices = this.HideOnSmallDevices;
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
        internal async Task<bool> CheckState(TopAppBar.Options options)
        {
            bool stateChanged = false;

            if (this.HideOnSmallDevices != options.HideTitleOnSmallDevices)
            {
                this.HideOnSmallDevices = options.HideTitleOnSmallDevices;
                await this.HideOnSmallDevicesChanged.InvokeAsync(this.HideOnSmallDevices);
                stateChanged = true;
            }

            return stateChanged;
        }
    }
}