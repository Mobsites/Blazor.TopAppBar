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
        internal TopAppBarNav TopAppBarNav { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal TopAppBarActions TopAppBarActions { get; set; }

        /// <summary>
        /// The variant state of <see cref="TopAppBar">.
        /// </summary>
        [Parameter] public Variants Variant { get; set; }
        [Parameter] public EventCallback<Variants> VariantChanged { get; set; }
        
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var options = KeepState 
                ? UseSessionStorageForState
                    ? await Storage.Session.GetAsync<Options>(nameof(TopAppBar))
                    : await Storage.Local.GetAsync<Options>(nameof(TopAppBar))
                : null;

            if (firstRender)
            {
                Console.WriteLine("firstRender");
                if (options is null)
                {
                    options = SetOptions();
                }
                else
                {
                    await CheckState(options);
                }

                // Destroy any lingering js representation.
                options.Destroy = true;
                
                initialized = await jsRuntime.InvokeAsync<bool>(
                    "Mobsites.Blazor.TopAppBar.init",
                    options);
            }
            else
            {
                Console.WriteLine("Re-render");
                // Use current state if...
                if (initialized || options is null)
                {
                    options = SetOptions();
                }

                initialized = await jsRuntime.InvokeAsync<bool>(
                    "Mobsites.Blazor.TopAppBar.refresh",
                    options);
            }

            // Clear destory before saving.
            options.Destroy = false;

            if (KeepState)
            {
                if (UseSessionStorageForState)
                {
                    await Storage.Session.SetAsync(nameof(TopAppBar), options);
                }
                else
                {
                    await Storage.Local.SetAsync(nameof(TopAppBar), options);
                }
            }
            else
            {
                await Storage.Local.RemoveAsync<Options>(nameof(TopAppBar));
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

        private Options SetOptions()
        {
            return  new Options 
            {
                Variant = this.Variant,
                Adjustment = GetAdjustment(),
                ScrollToTop = this.TopAppBarNav?.ScrollToTop ?? false,
                BrandTitle = this.TopAppBarNav?.BrandTitle,
                HideBrandTitle = this.TopAppBarNav?.HideBrandTitle ?? false,
                UseBrandImage = this.TopAppBarNav?.UseBrandImage ?? false,
                HideBrandImage = this.TopAppBarNav?.HideBrandImage ?? false,
                ShowActionsAlways = this.TopAppBarActions?.ShowActionsAlways ?? false
            };
        }

        private async Task CheckState(Options options)
        {
            // If state has changed...
            if (this.Variant != options.Variant)
            {
                await VariantChanged.InvokeAsync(options.Variant);
            }
            if (this.TopAppBarActions != null)
            {
                if (this.TopAppBarActions.ShowActionsAlways != options.ShowActionsAlways)
                {
                    await this.TopAppBarActions.ShowActionsAlwaysChanged.InvokeAsync(options.ShowActionsAlways);
                }
            }
            if (this.TopAppBarNav != null)
            {
                if (this.TopAppBarNav.ScrollToTop != options.ScrollToTop)
                {
                    await this.TopAppBarNav.ScrollToTopChanged.InvokeAsync(options.ScrollToTop);
                }

                if (this.TopAppBarNav.BrandTitle != options.BrandTitle)
                {
                    await this.TopAppBarNav.BrandTitleChanged.InvokeAsync(options.BrandTitle);
                }

                if (this.TopAppBarNav.HideBrandTitle != options.HideBrandTitle)
                {
                    await this.TopAppBarNav.HideBrandTitleChanged.InvokeAsync(options.HideBrandTitle);
                }

                if (this.TopAppBarNav.UseBrandImage != options.UseBrandImage)
                {
                    await this.TopAppBarNav.UseBrandImageChanged.InvokeAsync(options.UseBrandImage);
                }

                if (this.TopAppBarNav.HideBrandImage != options.HideBrandImage)
                {
                    await this.TopAppBarNav.HideBrandImageChanged.InvokeAsync(options.HideBrandImage);
                }
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed");
            initialized = false;
        }
    }
}