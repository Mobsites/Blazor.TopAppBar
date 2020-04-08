// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class TopAppBar
    {
        public class Options
        {
            public Variants Variant { get; set; }
            public string Adjustment { get; set; }
            public bool ScrollToTop { get; set; }
            public string BrandTitle { get; set; }
            public bool HideBrandTitle { get; set; }
            public bool UseBrandImage { get; set; }
            public bool HideBrandImage { get; set; }
            public bool ShowActionsAlways { get; set; }
            public bool Destroy { get; set; }
        }
    }
}