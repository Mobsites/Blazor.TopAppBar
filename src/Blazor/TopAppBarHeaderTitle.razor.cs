// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI child component for rendering the application title in the <see cref="TopAppBarHeader" /> component.
    /// </summary>
    public partial class TopAppBarHeaderTitle
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

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarHeaderTitle = this;
        }

        internal void SetOptions(TopAppBar.Options options)
        {
            options.HideTitleOnSmallDevices = this.HideOnSmallDevices;
            options.TopAppBarHeaderTitle = new Options
            {

            };

            base.SetOptions(options.TopAppBarHeaderTitle);
        }

        internal async Task<bool> CheckState(TopAppBar.Options options)
        {
            bool stateChanged = false;

            if (this.HideOnSmallDevices != options.HideTitleOnSmallDevices)
            {
                this.HideOnSmallDevices = options.HideTitleOnSmallDevices;
                await this.HideOnSmallDevicesChanged.InvokeAsync(this.HideOnSmallDevices);
                stateChanged = true;
            }

            return await base.CheckState(options.TopAppBarHeaderTitle) || stateChanged;
        }
    }
}