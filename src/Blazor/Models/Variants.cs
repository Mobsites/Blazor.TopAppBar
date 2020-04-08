// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class TopAppBar
    {
        /// <summary>
        /// The supported MDC variants of the <see cref="TopAppBar">.
        /// </summary>
        public enum Variants
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
    }
}