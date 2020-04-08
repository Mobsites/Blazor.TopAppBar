// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
        [Parameter] public EventCallback<bool> ScrollToTopChanged { get; set; }

        /// <summary>
        /// A brand title to display.
        /// </summary>
        [Parameter] public string BrandTitle { get; set; }
        [Parameter] public EventCallback<string> BrandTitleChanged { get; set; }

        /// <summary>
        /// Whether to hide brand title on small devices.
        /// </summary>
        [Parameter] public bool HideBrandTitle { get; set; }
        [Parameter] public EventCallback<bool> HideBrandTitleChanged { get; set; }

        /// <summary>
        /// Whether to show a brand image.
        /// </summary>
        [Parameter] public bool UseBrandImage { get; set; }
        [Parameter] public EventCallback<bool> UseBrandImageChanged { get; set; }

        /// <summary>
        /// Whether to hide brand image on small devices.
        /// </summary>
        [Parameter] public bool HideBrandImage { get; set; }
        [Parameter] public EventCallback<bool> HideBrandImageChanged { get; set; }

        private string imageSource = "_content/Mobsites.Blazor.TopAppBar/blazor.png";
        
        /// <summary>
        /// Image source override. Defaults to '_content/Mobsites.Blazor.TopAppBar/blazor.png'.
        /// </summary>
        [Parameter] public string BrandImageSource 
        { 
            get => imageSource; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    imageSource = value;
                } 
            } 
        }

        private int imageWidth = 36;
        
        /// <summary>
        /// Image width (px) override. Defaults to 36px.
        /// </summary>
        [Parameter] public int BrandImageWidth 
        { 
            get => imageWidth; 
            set 
            { 
                if (value > 0)
                {
                    imageWidth = value;
                } 
            } 
        }

        private int imageHeight = 36;
        
        /// <summary>
        /// Image height (px) override. Defaults to 36px.
        /// </summary>
        [Parameter] public int BrandImageHeight 
        { 
            get => imageHeight; 
            set 
            { 
                if (value > 0)
                {
                    imageHeight = value;
                } 
            } 
        }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarNav = this;
        }
    }
}