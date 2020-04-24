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
	init: function (options) {
		window.Mobsites.Blazor.TopAppBar.options = options;
		// SPA loads this file once on start, so check for existence before new-ing up another.
		if (!window.Mobsites.Blazor.TopAppBar.initialized || options.destroy) {
			if (window.Mobsites.Blazor.TopAppBar.self) {
				window.Mobsites.Blazor.TopAppBar.self.destroy();
			}
			this.assignMissingMDCClasses();
			this.assignAdjustment();
			window.Mobsites.Blazor.TopAppBar.self = new MDCTopAppBar(
				document.querySelector(".mdc-top-app-bar")
			);
			window.Mobsites.Blazor.TopAppBar.initialized = true;
			this.initEvents();
		}
		else {
			this.assignMissingMDCClasses();
			this.assignAdjustment();
		}
		return true;
	},
	refresh: function (options) {
		if (options.variant !== window.Mobsites.Blazor.TopAppBar.options.variant) {
			options.destroy = true;
		}
		return this.init(options);
	},
	assignMissingMDCClasses: function () {
		if (window.Mobsites.Blazor.AppDrawer) {
			if (window.Mobsites.Blazor.TopAppBar.options.aboveAppDrawer) {
				const drawer = document.querySelector(
					".mdc-drawer"
				);
				drawer.classList.add("blazor-topAppBar-adjustment");
			}
			const app_content = document.querySelector(
				".mdc-drawer-app-content"
			);
			if (app_content) {
				const self = document.querySelector(
					".mdc-top-app-bar"
				);
				if (app_content.contains(self)) {
					self.nextElementSibling.classList.add("blazor-topAppBar-adjustment", "blazor-main-content");
				}
				else {
					app_content.classList.add("blazor-topAppBar-adjustment", "blazor-main-content");
				}
			}
		}
		const navigation_icon = document.querySelector(
			".mdc-top-app-bar__navigation-icon"
		);
		if (!navigation_icon) {
			// Only first child in specified container gets class assignment.
			const start_container = document.querySelector(
				".mdc-top-app-bar__section.mdc-top-app-bar__section--align-start"
			);
			if (start_container) {
				if (start_container.firstElementChild) {
					start_container.firstElementChild.classList.add(
						"mdc-top-app-bar__navigation-icon"
					);
					if (window.Mobsites.Blazor.AppDrawer) {
						window.Mobsites.Blazor.AppDrawer.determineDrawerButtonVisibility();
					}
				}
			}
		}
		const end_container = document.querySelector(
			".mdc-top-app-bar__section.mdc-top-app-bar__section--align-end"
		);
		const action_item = document.querySelector(".mdc-top-app-bar__action-item");
		if (!action_item) {
			// Every child in specified container gets class assignment.
			if (end_container && end_container.children) {
				for (let index = 0; index < end_container.children.length; index++) {
					end_container.children[index].classList.add(
						"mdc-top-app-bar__action-item"
					);
				}
			}
		}
		// Every child but first in specified container gets class assignment or un-assignment.
		if (end_container && end_container.children) {
			for (let index = 1; index < end_container.children.length; index++) {
				if (window.Mobsites.Blazor.TopAppBar.options.showActionsAlways) {
					end_container.children[index].classList.remove(
						"mdc-top-app-bar-hide"
					);
				} else {
					end_container.children[index].classList.add("mdc-top-app-bar-hide");
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
					window.Mobsites.Blazor.TopAppBar.options.adjustment
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
			window.Mobsites.Blazor.TopAppBar.self.listen("MDCTopAppBar:nav", this.toggleAppDrawerClickEvent);
			const mainContent =
				document.getElementById("blazor-main-content") ||
				document.querySelector(".blazor-main-content");
			if (mainContent) {
				window.Mobsites.Blazor.TopAppBar.self.setScrollTarget(mainContent);
			}
		}
	},
	initScrollToEvent: function () {
		const navigation_icon = document.querySelector(
			".mdc-top-app-bar__navigation-icon"
		);
		if (navigation_icon) {
			navigation_icon.addEventListener("click", this.scrollToEvent);
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