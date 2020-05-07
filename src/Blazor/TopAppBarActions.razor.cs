// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI subcomponent for the <see cref="TopAppBarHeader" /> component 
    /// that acts as a container for action items or links.
    /// </summary>
    public sealed partial class TopAppBarActions
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
        /// Whether to show all actions on all device sizes. Default is to hide all but first on small devices.
        /// </summary>
        [Parameter] public bool ShowActionsAlways { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> ShowActionsAlwaysChanged { get; set; }



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Dom element reference passed into javascript representation.
        /// </summary>
        internal ElementReference ElemRef { get; set; }

        /// <summary>
        /// Life cycle method for when parameters from parent are set.
        /// </summary>
        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.Actions = this;
        }

        /// <summary>
        /// Set values on options that need to be maintained when keeping state.
        /// </summary>
        internal void SetOptions(TopAppBar.Options options)
        {
            options.ShowActionsAlways = this.ShowActionsAlways;
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
        internal async Task<bool> CheckState(TopAppBar.Options options)
        {
            bool stateChanged = false;

            if (this.ShowActionsAlways != options.ShowActionsAlways)
            {
                this.ShowActionsAlways = options.ShowActionsAlways;
                await this.ShowActionsAlwaysChanged.InvokeAsync(options.ShowActionsAlways);
                stateChanged = true;
            }

            return stateChanged;
        }
    }
}