// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Child component of the <see cref="TopAppBarActions" /> component 
    /// that acts as a container for a single action item or link.
    /// </summary>
    public partial class TopAppBarAction
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
    }
}