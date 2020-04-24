// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI component for containing items such as the application title, navigation icon, and action items.
    /// </summary>
    public partial class TopAppBar : IDisposable
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
        /// The MDC Top App Bar variant to render.
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

        /// <summary>
        /// Whether this component is both used and should appear above (in markup) our Blazor App Drawer.
        /// </summary>
        [Parameter] public bool AboveAppDrawer { get; set; }

        /// <summary>
        /// Clear all state for this UI component and any of its dependents from browser storage.
        /// </summary>
        public ValueTask ClearState() => this.ClearState<TopAppBar, Options>();



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeader TopAppBarHeader { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarActions TopAppBarActions { get; set; }
        
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Initialize();
            }
            else
            {
                await Refresh();
            }
        }

        private async Task Initialize()
        {
            var options = await this.GetState<TopAppBar, Options>();

            if (options is null)
            {
                options = this.GetOptions();
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

            await this.Save<TopAppBar, Options>(options);
        }

        private async Task Refresh()
        {
            var options = await this.GetState<TopAppBar, Options>();

            // Use current state if...
            if (this.initialized || options is null)
            {
                options = this.GetOptions();
            }

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.TopAppBar.refresh",
                options);

            await this.Save<TopAppBar, Options>(options);
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

        internal Options GetOptions()
        {
            var options = new Options 
            {
                Variant = this.Variant,
                Adjustment = GetAdjustment(),
                AboveAppDrawer = this.AboveAppDrawer,
                ScrollToTop = this.ScrollToTop
            };

            base.SetOptions(options);
            this.TopAppBarHeader?.SetOptions(options);
            this.TopAppBarActions?.SetOptions(options);

            return options;
        }

        internal async Task CheckState(Options options)
        {
            bool stateChanged = false;

            if (this.Variant != (options.Variant ?? TopAppBar.Variants.Standard))
            {
                this.Variant = options.Variant ?? TopAppBar.Variants.Standard;
                await this.VariantChanged.InvokeAsync(this.Variant);
                stateChanged = true;
            }
            if (this.ScrollToTop != options.ScrollToTop)
            {
                this.ScrollToTop = options.ScrollToTop;
                await this.ScrollToTopChanged.InvokeAsync(this.ScrollToTop);
                stateChanged = true;
            }

            bool baseStateChanged = await base.CheckState(options);
            bool headerStateChanged = await this.TopAppBarHeader?.CheckState(options);
            bool actionsStateChanged = await this.TopAppBarActions?.CheckState(options);

            if (stateChanged 
                || baseStateChanged
                || headerStateChanged
                || actionsStateChanged)
                StateHasChanged();
        }
        
        public override void Dispose()
        {
            this.initialized = false;
        }
    }
}