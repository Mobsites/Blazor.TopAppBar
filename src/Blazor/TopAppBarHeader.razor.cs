// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI subcomponent for the <see cref="TopAppBarHeader" /> component 
    /// that acts as a container for the application title, logo, and optional navigation button.
    /// </summary>
    public partial class TopAppBarHeader
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
        internal TopAppBarHeaderTitle TopAppBarHeaderTitle { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeaderLogo TopAppBarHeaderLogo { get; set; }
        
        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarHeader = this;
        }

        internal void SetOptions(TopAppBar.Options options)
        {
            options.TopAppBarHeader = new Options
            {

            };

            base.SetOptions(options.TopAppBarHeader);
            TopAppBarHeaderLogo?.SetOptions(options);
            TopAppBarHeaderTitle?.SetOptions(options);
        }

        internal async Task<bool> CheckState(TopAppBar.Options options)
        {
            bool baseStateChanged = await base.CheckState(options.TopAppBarHeader);
            bool logoStateChanged = await TopAppBarHeaderLogo?.CheckState(options);
            bool titleStateChanged = await TopAppBarHeaderTitle?.CheckState(options);

            return baseStateChanged
                || logoStateChanged 
                || titleStateChanged;
        }
    }
}