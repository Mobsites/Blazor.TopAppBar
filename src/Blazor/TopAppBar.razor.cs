// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI component for containing items such as the application title, navigation icon, and action items.
    /// </summary>
    public sealed partial class TopAppBar : IDisposable
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
        /// Content to render.
        /// </summary>
        [JSInvokable]
        public void SetIndex(int index)
        {
            if (Index < 0)
            {
                Index = index;
            }
        }

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
        /// Whether component environment is Blazor WASM or Server.
        /// </summary>
        internal bool IsWASM => RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY"));

        private DotNetObjectReference<TopAppBar> self;

        /// <summary>
        /// Net reference passed into javascript representation.
        /// </summary>
        internal DotNetObjectReference<TopAppBar> Self
        {
            get => self ?? (Self = DotNetObjectReference.Create(this));
            set => self = value;
        }

        /// <summary>
        /// The index to this object's javascript representation in the object store.
        /// </summary>
        internal int Index { get; set; } = -1;

        /// <summary>
        /// Dom element reference passed into javascript representation.
        /// </summary>
        internal ElementReference ElemRef { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarHeader Header { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarActions Actions { get; set; }

        /// <summary>
        /// Life cycle method for when component has been rendered in the dom and javascript interopt is fully ready.
        /// </summary>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Initialize();
            }
            else
            {
                await Update();
            }
        }

        /// <summary>
        /// Initialize state and javascript representations.
        /// </summary>
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

            this.initialized = await this.jsRuntime.InvokeAsync<bool>(
                "Mobsites.Blazor.TopAppBars.init",
                Self,
                new
                {
                    AppBar = this.ElemRef,
                    NavTrigger = this.Header?.NavTrigger?.ElemRef,
                    Actions = this.Actions?.ElemRef
                },
                options);

            await this.Save<TopAppBar, Options>(options);
        }

        /// <summary>
        /// Update state.
        /// </summary>
        private async Task Update()
        {
            var options = await this.GetState<TopAppBar, Options>();

            // Use current state if...
            if (this.initialized || options is null)
            {
                options = this.GetOptions();
            }

            await this.jsRuntime.InvokeVoidAsync(
                "Mobsites.Blazor.TopAppBars.update",
                Index,
                options);

            await this.Save<TopAppBar, Options>(options);
        }

        /// <summary>
        /// Get current or storage-saved options for keeping state.
        /// </summary>
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
            this.Header?.SetOptions(options);
            this.Actions?.SetOptions(options);

            return options;
        }

        /// <summary>
        /// Check whether storage-retrieved options are different than current
        /// and thereby need to notify parents of change when keeping state.
        /// </summary>
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
            bool headerStateChanged = await this.Header?.CheckState(options);
            bool actionsStateChanged = await this.Actions?.CheckState(options);

            if (stateChanged
                || baseStateChanged
                || headerStateChanged
                || actionsStateChanged)
                StateHasChanged();
        }

        /// <summary>
        /// Get adjustment css class according to variant.
        /// </summary>
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

        /// <summary>
        /// Called by GC.
        /// </summary>
        public override void Dispose()
        {
            jsRuntime.InvokeVoidAsync("Mobsites.Blazor.TopAppBars.destroy", Index);
            self?.Dispose();
            base.Dispose();
        }
    }
}