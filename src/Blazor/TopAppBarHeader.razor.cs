// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor child component that acts as a container for the application title and optional navigation button.
    /// </summary>
    public partial class TopAppBarHeader
    {
        /// <summary>
        /// A title to display.
        /// </summary>
        [Parameter] public string Title { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<string> TitleChanged { get; set; }

        /// <summary>
        /// Whether to hide title on small devices.
        /// </summary>
        [Parameter] public bool HideTitle { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> HideTitleChanged { get; set; }

        /// <summary>
        /// Whether to use image.
        /// </summary>
        [Parameter] public bool UseImage { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> UseImageChanged { get; set; }

        private string image;
        
        /// <summary>
        /// Image source.
        /// </summary>
        [Parameter] public string Image 
        { 
            get => image; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    image = value;
                } 
            } 
        }

        private int imageWidth = 36;
        
        /// <summary>
        /// Image width. Defaults to 36px.
        /// </summary>
        [Parameter] public int ImageWidth 
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
        /// Image height. Defaults to 36px.
        /// </summary>
        [Parameter] public int ImageHeight 
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

        /// <summary>
        /// Whether to hide image on small devices.
        /// </summary>
        [Parameter] public bool HideImage { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> HideImageChanged { get; set; }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.TopAppBarHeader = this;
        }

        internal void SetOptions(TopAppBar.Options options)
        {
            options.Title = this.Title;
            options.HideTitle = this.HideTitle;
            options.UseImage = this.UseImage;
            options.HideImage = this.HideImage;
        }

        internal async Task CheckState(TopAppBar.Options options)
        {
            if (this.Title != options.Title)
            {
                await this.TitleChanged.InvokeAsync(options.Title);
            }

            if (this.HideTitle != options.HideTitle)
            {
                await this.HideTitleChanged.InvokeAsync(options.HideTitle);
            }

            if (this.UseImage != options.UseImage)
            {
                await this.UseImageChanged.InvokeAsync(options.UseImage);
            }

            if (this.HideImage != options.HideImage)
            {
                await this.HideImageChanged.InvokeAsync(options.HideImage);
            }
        }
    }
}