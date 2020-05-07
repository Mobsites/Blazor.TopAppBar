// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI child component for rendering the application logo in the <see cref="TopAppBarHeader" /> component.
    /// </summary>
    public sealed partial class TopAppBarHeaderLogo
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        private string src;

        /// <summary>
        /// Image source.
        /// </summary>
        [Parameter]
        public string Src
        {
            get => src;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    src = value;
                }
            }
        }

        private int width = 36;

        /// <summary>
        /// Image width. Defaults to 36px.
        /// </summary>
        [Parameter]
        public int Width
        {
            get => width;
            set
            {
                if (value > 0)
                {
                    width = value;
                }
            }
        }

        private int height = 36;

        /// <summary>
        /// Image height. Defaults to 36px.
        /// </summary>
        [Parameter]
        public int Height
        {
            get => height;
            set
            {
                if (value > 0)
                {
                    height = value;
                }
            }
        }

        /// <summary>
        /// Whether to hide image on small devices.
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
            base.Parent.Logo = this;
        }

        /// <summary>
        /// Set values on options that need to be maintained when keeping state.
        /// </summary>
        internal void SetOptions(TopAppBar.Options options)
        {
            options.HideLogoOnSmallDevices = this.HideOnSmallDevices;
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
        internal async Task<bool> CheckState(TopAppBar.Options options)
        {
            bool stateChanged = false;

            if (this.HideOnSmallDevices != options.HideLogoOnSmallDevices)
            {
                this.HideOnSmallDevices = options.HideLogoOnSmallDevices;
                await this.HideOnSmallDevicesChanged.InvokeAsync(this.HideOnSmallDevices);
                stateChanged = true;
            }

            return stateChanged;
        }
    }
}