// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI child component for containing a navigation trigger, such as an icon or button, in the <see cref="TopAppBarHeader" /> component.
    /// </summary>
    public partial class TopAppBarHeaderNavTrigger
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

        internal ElementReference ElemRef { get; set; }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.NavTrigger = this;
        }
    }
}