// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class TopAppBar
    {
        /// <summary>
        /// Use this css class marker on content below the <see cref="TopAppBar"/> to prevent it from covering top part of said content.
        /// </summary>
        public const string AdjustmentMarkerClass = "blazor-topAppBar-adjustment";

        /// <summary>
        /// Use this as the id or as a class marker for the main content in your Blazor app.
        /// </summary>
        public const string MainContentMarker = "blazor-main-content";
    }
}