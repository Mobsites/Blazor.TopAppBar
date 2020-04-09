// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor child component that acts as a container for the application title and optional navigation button.
    /// </summary>
    public partial class TopAppBarNav
    {
        /// <summary>
        /// Whether to scroll to top of page when navigation icon is clicked.
        /// </summary>
        [Parameter] public bool ScrollToTop { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> ScrollToTopChanged { get; set; }

        /// <summary>
        /// A brand title to display.
        /// </summary>
        [Parameter] public string BrandTitle { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<string> BrandTitleChanged { get; set; }

        /// <summary>
        /// Whether to hide brand title on small devices.
        /// </summary>
        [Parameter] public bool HideBrandTitle { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> HideBrandTitleChanged { get; set; }

        /// <summary>
        /// Whether to hide icon on small devices.
        /// </summary>
        [Parameter] public bool HideIcon { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> HideIconChanged { get; set; }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarNav = this;
        }

        internal void SetOptions(TopAppBar.Options options)
        {
            options.ScrollToTop = this.ScrollToTop;
            options.BrandTitle = this.BrandTitle;
            options.HideBrandTitle = this.HideBrandTitle;
            options.UseIcon = this.UseIcon;
            options.HideIcon = this.HideIcon;
        }

        internal async Task CheckState(TopAppBar.Options options)
        {
            if (this.ScrollToTop != options.ScrollToTop)
            {
                await this.ScrollToTopChanged.InvokeAsync(options.ScrollToTop);
            }

            if (this.BrandTitle != options.BrandTitle)
            {
                await this.BrandTitleChanged.InvokeAsync(options.BrandTitle);
            }

            if (this.HideBrandTitle != options.HideBrandTitle)
            {
                await this.HideBrandTitleChanged.InvokeAsync(options.HideBrandTitle);
            }

            if (this.UseIcon != options.UseIcon)
            {
                await this.UseIconChanged.InvokeAsync(options.UseIcon);
            }

            if (this.HideIcon != options.HideIcon)
            {
                await this.HideIconChanged.InvokeAsync(options.HideIcon);
            }
        }
    }
}