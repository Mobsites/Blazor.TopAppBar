// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public partial class TopAppBar
    {
        internal class Options : ParentOptions
        {
            public Variants Variant { get; set; }
            public string Adjustment { get; set; }
            public bool ScrollToTop { get; set; }
            public string Title { get; set; }
            public bool HideTitle { get; set; }
            public bool UseImage { get; set; }
            public bool HideImage { get; set; }
            public bool UseBackgroundImage { get; set; }
            public bool ShowActionsAlways { get; set; }
            public bool Destroy { get; set; }
        }
    }
}