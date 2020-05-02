// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { MDCTopAppBar } from "@material/top-app-bar";

if (!window.Mobsites) {
	window.Mobsites = {
		Blazor: {

		}
	};
}

window.Mobsites.Blazor.TopAppBar = {
	init: function (elemRefs, options) {
        this.elemRefs = elemRefs;
        this.options = options;
		// SPA loads this file once on start, so check for existence before new-ing up another.
		if (!this.initialized || this.options.destroy) {
			if (this.self) {
				this.self.destroy();
			}
			this.assignMissingMDCClasses();
			this.assignAdjustment();
			this.self = new MDCTopAppBar(this.elemRefs.appBar);
			this.initialized = true;
			this.initEvents();
		}
		else {
			this.assignMissingMDCClasses();
			this.assignAdjustment();
		}
		return true;
	},
	refresh: function (elemRefs, options) {
		if (options.variant !== this.options.variant) {
			options.destroy = true;
		}
		return this.init(elemRefs, options);
	},
	assignMissingMDCClasses: function () {
        const appDrawer = window.Mobsites.Blazor.AppDrawer;
		if (appDrawer) {
			if (this.options.aboveAppDrawer) {
                // May not be fully initialized yet depending on the page layout.
                const drawer = appDrawer.elemRefs
                    ? appDrawer.elemRefs.drawer
                    : document.querySelector(".mdc-drawer");
                if (drawer) {
                    drawer.classList.add("blazor-topAppBar-adjustment");
                }
			}
			const app_content = document.querySelector(
				".mdc-drawer-app-content"
			);
			if (app_content) {
				if (app_content.contains(this.elemRefs.appBar)) {
					this.elemRefs.appBar.nextElementSibling.classList.add("blazor-topAppBar-adjustment", "blazor-main-content");
				}
				else {
					app_content.classList.add("blazor-topAppBar-adjustment", "blazor-main-content");
				}
			}
		}
		if (this.elemRefs.navTrigger && appDrawer) {
			appDrawer.determineDrawerButtonVisibility();
		}
		// Every child but first in specified container gets class assignment or un-assignment.
		if (this.elemRefs.actions && this.elemRefs.actions.children) {
			for (let index = 1; index < this.elemRefs.actions.children.length; index++) {
				if (this.options.showActionsAlways) {
					this.elemRefs.actions.children[index].classList.remove(
						"mdc-top-app-bar-hide"
					);
				} else {
					this.elemRefs.actions.children[index].classList.add("mdc-top-app-bar-hide");
				}
			}
		}
	},
	assignAdjustment: function () {
		const adjustment_content = document.querySelectorAll(
			".blazor-topAppBar-adjustment"
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
					this.options.adjustment
				);
			}
		}
	},
	initEvents: function () {
		this.initAppDrawerToggleEvent();
		this.initScrollToEvent();
	},
	initAppDrawerToggleEvent: function () {
		if (window.Mobsites.Blazor.AppDrawer) {
			this.self.listen("MDCTopAppBar:nav", this.toggleAppDrawerClickEvent);
			const mainContent =
				document.getElementById("blazor-main-content") ||
				document.querySelector(".blazor-main-content");
			if (mainContent) {
				this.self.setScrollTarget(mainContent);
			}
		}
	},
	initScrollToEvent: function () {
		if (this.elemRefs.navTrigger) {
			this.elemRefs.navTrigger.addEventListener("click", this.scrollToEvent);
		}
	},
	toggleAppDrawerClickEvent: function () {
		window.Mobsites.Blazor.AppDrawer.self.open = !window.Mobsites.Blazor.AppDrawer.self.open;
	},
	scrollToEvent: function () {
		if (window.Mobsites.Blazor.TopAppBar.options.scrollToTop) {
			window.scrollTo(0, 0);
		}
	}
};