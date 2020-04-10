// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor component that utilizes the MDC Top App Bar library and acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    public partial class TopAppBar : IDisposable
    {
        /// <summary>
        /// Whether the component has been completely initialized, including its JavaScript representation.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeader TopAppBarHeader { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarActions TopAppBarActions { get; set; }

        /// <summary>
        /// Use this css class marker on content below the <see cref="TopAppBar"/> to prevent it from covering top part of said content.
        /// </summary>
        public const string AdjustmentMarkerClass = "blazor-topAppBar-adjustment";

        /// <summary>
        /// Use this as the id or as a class marker for the main content in your Blazor app.
        /// </summary>
        public const string MainContentMarker = "blazor-main-content";

        /// <summary>
        /// The variant state of <see cref="TopAppBar">.
        /// </summary>
        [Parameter] public Variants Variant { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<Variants> VariantChanged { get; set; }

        /// <summary>
        /// Whether to scroll to top of page when navigation icon is clicked.
        /// </summary>
        [Parameter] public bool ScrollToTop { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<bool> ScrollToTopChanged { get; set; }
        
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var options = this.KeepState 
                ? this.UseSessionStorageForState
                    ? await this.Storage.Session.GetAsync<Options>(nameof(TopAppBar))
                    : await this.Storage.Local.GetAsync<Options>(nameof(TopAppBar))
                : null;

            if (firstRender)
            {
                if (options is null)
                {
                    options = this.SetOptions();
                }
                else
                {
                    await this.CheckState(options);
                }

                // Destroy any lingering js representation.
                options.Destroy = true;
                
                this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                    "Mobsites.Blazor.TopAppBar.init",
                    options);
            }
            else
            {
                // Use current state if...
                if (this.initialized || options is null)
                {
                    options = this.SetOptions();
                }

                this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                    "Mobsites.Blazor.TopAppBar.refresh",
                    options);
            }

            // Clear destory before saving.
            options.Destroy = false;

            if (this.KeepState)
            {
                if (this.UseSessionStorageForState)
                {
                    await this.Storage.Session.SetAsync(nameof(TopAppBar), options);
                }
                else
                {
                    await this.Storage.Local.SetAsync(nameof(TopAppBar), options);
                }
            }
            else
            {
                await this.Storage.Session.RemoveAsync<Options>(nameof(TopAppBar));
                await this.Storage.Local.RemoveAsync<Options>(nameof(TopAppBar));
            }
        }

        private string GetAdjustment() => this.Variant switch
        {
            Variants.Standard => "mdc-top-app-bar--fixed-adjust",
            Variants.Fixed => "mdc-top-app-bar--fixed-adjust",
            Variants.Prominent => "mdc-top-app-bar--prominent-fixed-adjust",
            Variants.FixedProminent => "mdc-top-app-bar--prominent-fixed-adjust",
            Variants.Dense => "mdc-top-app-bar--dense-fixed-adjust",
            Variants.FixedDense => "mdc-top-app-bar--dense-fixed-adjust",
            Variants.ProminentDense => "mdc-top-app-bar--prominent-dense-fixed-adjust",
            Variants.FixedProminentDense => "mdc-top-app-bar--prominent-dense-fixed-adjust",
            Variants.Short => "mdc-top-app-bar--short-fixed-adjust",
            Variants.ShortAlways => "mdc-top-app-bar--short-fixed-adjust",
            _ => null
        };

        internal Options SetOptions()
        {
            var options = new Options 
            {
                Variant = this.Variant,
                Adjustment = GetAdjustment(),
                ScrollToTop = this.ScrollToTop
            };

            base.SetOptions(options);
            this.TopAppBarHeader?.SetOptions(options);
            this.TopAppBarActions?.SetOptions(options);

            return options;
        }

        internal async Task CheckState(Options options)
        {
            if (this.Variant != options.Variant)
            {
                await this.VariantChanged.InvokeAsync(options.Variant);
            }
            if (this.ScrollToTop != options.ScrollToTop)
            {
                await this.ScrollToTopChanged.InvokeAsync(options.ScrollToTop);
            }

            await base.CheckState(options);
            await this.TopAppBarHeader?.CheckState(options);
            await this.TopAppBarActions?.CheckState(options);
        }

        public void Dispose()
        {
            this.initialized = false;
        }
    }
}