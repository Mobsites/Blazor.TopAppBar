// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { MDCTopAppBar } from "@material/top-app-bar";

if (!window.Mobsites) {
    window.Mobsites = {
        Blazor: {

        }
    };
}

window.Mobsites.Blazor.TopAppBars = {
    store: [],
    init: function (dotNetObjRef, elemRefs, options) {
        try {
            const index = this.add(new Mobsites_Blazor_TopAppBar(dotNetObjRef, elemRefs, options));
            dotNetObjRef.invokeMethodAsync('SetIndex', index);
            return true;
        } catch (error) {
            console.log(error);
            return false;
        }
    },
    add: function (appBar) {
        for (let i = 0; i < this.store.length; i++) {
            if (this.store[i] == null) {
                this.store[i] = appBar;
                return i;
            }
        }
        const index = this.store.length;
        this.store[index] = appBar;
        return index;
    },
    update: function (index, options) {
        if (options.variant !== this.store[index].dotNetObjOptions.variant) {
            var dotNetObjRef = this.store[index].dotNetObjRef;
            var elemRefs = this.store[index].elemRefs;
            this.destroy(index);
            this.init(dotNetObjRef, elemRefs, options);
        } else {
            this.store[index].update(options);
        }
    },
    destroy: function (index) {
        this.store[index].destroy();
        this.store[index] = null;
    }
}

class Mobsites_Blazor_TopAppBar extends MDCTopAppBar {
    constructor(dotNetObjRef, elemRefs, options) {
        super(elemRefs.appBar);
        this.dotNetObjRef = dotNetObjRef;
        this.elemRefs = elemRefs;
        this.dotNetObjOptions = options;
        this.determineActionsVisibility();
        this.determineNavTriggerVisibility();
        this.assignAdjustment();
        this.initScrollEvents();
    }
    update(options) {
        this.dotNetObjOptions = options;
        this.determineActionsVisibility();
        this.assignAdjustment();
    }
    assignAdjustment() {
        const adjustment_content = document.querySelectorAll(
            ".mdc-top-app-bar--adjustment"
        );
        if (adjustment_content) {
            for (let index = 0; index < adjustment_content.length; index++) {
                adjustment_content[index].classList.remove(
                    "mdc-top-app-bar--fixed-adjust"
                );
                adjustment_content[index].classList.remove(
                    "mdc-top-app-bar--prominent-fixed-adjust"
                );
                adjustment_content[index].classList.remove(
                    "mdc-top-app-bar--dense-fixed-adjust"
                );
                adjustment_content[index].classList.remove(
                    "mdc-top-app-bar--prominent-dense-fixed-adjust"
                );
                adjustment_content[index].classList.remove(
                    "mdc-top-app-bar--short-fixed-adjust"
                );
                adjustment_content[index].classList.add(
                    this.dotNetObjOptions.adjustment
                );
            }
        }
    }
    determineNavTriggerVisibility() {
        if (this.elemRefs.navTrigger && Mobsites.Blazor.AppDrawers) {
            // Cannot know which App Drawer this is associated with, so...
            window.Mobsites.Blazor.AppDrawers.store.forEach(drawer => {
                if (drawer)
                    drawer.determineTriggerVisibility();
            });
        }
    }
    determineActionsVisibility() {
        // Every child but first in specified container gets class assignment or un-assignment.
        if (this.elemRefs.actions && this.elemRefs.actions.children) {
            for (let index = 1; index < this.elemRefs.actions.children.length; index++) {
                if (this.dotNetObjOptions.showActionsAlways) {
                    this.elemRefs.actions.children[index].classList.remove(
                        "mdc-top-app-bar--hide"
                    );
                } else {
                    this.elemRefs.actions.children[index].classList.add("mdc-top-app-bar--hide");
                }
            }
        }
    }
    initScrollEvents() {
        var self = this;
        if (self.elemRefs.navTrigger) {
            self.elemRefs.navTrigger.addEventListener("click", () => {
                if (self && self.dotNetObjOptions.scrollToTop) {
                    window.scrollTo(0, 0);
                }
            });
        }
        const mainContent =
            document.getElementById("mobsites-blazor-main-content") ||
            document.querySelector(".mobsites-blazor-main-content");
        if (mainContent) {
            self.setScrollTarget(mainContent);
        }
    }
}