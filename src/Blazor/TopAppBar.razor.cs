// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Blazor component that utilizes the MDC Top App Bar component and acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    public partial class TopAppBar : IDisposable
    {
        [Inject] protected IJSRuntime jsRuntime { get; set; }

        /// <summary>
        /// Use this css class marker on content below the <see cref="TopAppBar"/> to prevent it from covering top part of said content.
        /// </summary>
        public static string AdjustmentMarkerClass => "blazor-topAppBar-adjustment";

        /// <summary>
        /// Use this as the id or as a class marker for the main content in your Blazor app.
        /// </summary>
        public static string MainContentMarker => "blazor-main-content";

        /// <summary>
        /// All html attributes outside of the class attribute go here. Use the Class attribute property to add css classes.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; }

        /// <summary>
        /// The <see cref="TopAppBarNav"> and (optional) <see cref="TopAppBarActions">.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Css classes for affecting this component go here.
        /// </summary>
        [Parameter] public string Class { get; set; }

        /// <summary>
        /// Whether to scroll to top of page when navigation icon is clicked.
        /// </summary>
        internal bool ScrollToTop { get; set; }

        /// <summary>
        /// Whether to show all actions on all device sizes. Default is to hide all but first on small devices.
        /// </summary>
        internal bool ShowActionsAlways { get; set; }

        /// <summary>
        /// The type of <see cref="TopAppBar"> to display.
        /// </summary>
        [Parameter] public Types Type { get; set; }

        /// <summary>
        /// The various MDC supported types of the <see cref="TopAppBar">.
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Standard <see cref="TopAppBar"> scrolls up with the rest of the content and immediately reappears when scrolling down.
            /// </summary>
            Standard,

            /// <summary>
            /// Fixed <see cref="TopAppBar"> stays at the top of the page and elevates above the content when scrolling.
            /// </summary>
            Fixed,

            /// <summary>
            /// Prominent <see cref="TopAppBar"> appears taller than standard but functions the same.
            /// </summary>
            Prominent,

            /// <summary>
            /// Fixed Prominent <see cref="TopAppBar"> appears taller than fixed but functions the same.
            /// </summary>
            FixedProminent,

            /// <summary>
            /// Dense <see cref="TopAppBar"> appears shorter than standard but functions the same.
            /// </summary>
            Dense,

            /// <summary>
            /// Fixed Dense <see cref="TopAppBar"> appears shorter than fixed but functions the same.
            /// </summary>
            FixedDense,

            /// <summary>
            /// Prominent Dense <see cref="TopAppBar"> appears taller than standard and shorter than prominent but functions the same.
            /// </summary>
            ProminentDense,

            /// <summary>
            /// Fixed Prominent Dense <see cref="TopAppBar"> appears taller than fixed and shorter than fixed prominent but functions the same.
            /// </summary>
            FixedProminentDense,

            /// <summary>
            /// Short <see cref="TopAppBar"> collapses to the navigation icon side when scrolling.
            /// </summary>
            Short,

            /// <summary>
            /// Short Always <see cref="TopAppBar"> stays collapsed to the navigation icon side regardless of scrolling.
            /// </summary>
            ShortAlways
        }
        
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            var options = new {
                Type,
                Adjustment = GetAdjustment(),
                ScrollToTop,
                ShowActionsAlways
            };

            if (firstRender)
            {
                await jsRuntime.InvokeVoidAsync(
                    "Blazor.TopAppBar.init",
                    options);
            }
            else
            {
                await jsRuntime.InvokeVoidAsync(
                    "Blazor.TopAppBar.refresh",
                    options);
            }
        }

        private string GetAdjustment() => Type switch
        {
            Types.Standard => "mdc-top-app-bar--fixed-adjust",
            Types.Fixed => "mdc-top-app-bar--fixed-adjust",
            Types.Prominent => "mdc-top-app-bar--prominent-fixed-adjust",
            Types.FixedProminent => "mdc-top-app-bar--prominent-fixed-adjust",
            Types.Dense => "mdc-top-app-bar--dense-fixed-adjust",
            Types.FixedDense => "mdc-top-app-bar--dense-fixed-adjust",
            Types.ProminentDense => "mdc-top-app-bar--prominent-dense-fixed-adjust",
            Types.FixedProminentDense => "mdc-top-app-bar--prominent-dense-fixed-adjust",
            Types.Short => "mdc-top-app-bar--short-fixed-adjust",
            Types.ShortAlways => "mdc-top-app-bar--short-fixed-adjust",
            _ => null
        };

        public void Dispose()
        {

        }
    }
}