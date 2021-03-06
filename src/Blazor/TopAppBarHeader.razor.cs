// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI subcomponent for the <see cref="TopAppBar" /> component 
    /// that acts as a container for the application title, logo, and optional navigation button.
    /// </summary>
    public sealed partial class TopAppBarHeader
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



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeaderNavTrigger NavTrigger { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeaderTitle Title { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeaderLogo Logo { get; set; }

        /// <summary>
        /// Life cycle method for when parameters from parent are set.
        /// </summary>
        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.Header = this;
        }

        /// <summary>
        /// Set values on options that need to be maintained when keeping state.
        /// </summary>
        internal void SetOptions(TopAppBar.Options options)
        {
            Logo?.SetOptions(options);
            Title?.SetOptions(options);
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
        internal async Task<bool> CheckState(TopAppBar.Options options)
        {
            bool logoStateChanged = await Logo?.CheckState(options);
            bool titleStateChanged = await Title?.CheckState(options);

            return logoStateChanged || titleStateChanged;
        }
    }
}