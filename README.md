[![Build Status](https://dev.azure.com/Mobsites-US/Blazor%20Top%20App%20Bar/_apis/build/status/Build?branchName=master)](https://dev.azure.com/Mobsites-US/Blazor%20Top%20App%20Bar/_build/latest?definitionId=24&branchName=master)

# Blazor Top App Bar

by <a href="https://www.mobsites.com"><img align="center" src="./src/assets/mobsites-logo.png" width="36" height="36" style="padding-top: 20px;" />obsites</a>

A Blazor component that utilizes the [MDC Top App Bar](https://material.io/develop/web/components/top-app-bar/) library and acts as a container for items such as application title, navigation icon, and action items.

## [Demo](https://topappbar.mobsites.com)

Tap the link above to go to a live demo. Try some of the options to get an idea of what's possible. Then reload the app in the browser and watch how the state was kept! 

Check out its source code [here](./samples).

![Gif of Demo](src/assets/demo.gif)

## For

* Blazor WebAssembly
* Blazor Server

## Dependencies

###### .NETStandard 2.0

* Mobsites.Blazor.BaseComponents (>= 1.0.2)

## Design and Development

The design and development of this Blazor component was heavily guided by Microsoft's [Steve Sanderson](https://blog.stevensanderson.com/). He outlines a superb approach to building and deploying a reusable component library in this [presentation](https://youtu.be/QnBYmTpugz0) and [example](https://github.com/SteveSandersonMS/presentation-2020-01-NdcBlazorComponentLibraries).

As for the non-C# implementation of this library, obviously Google's MDC Navigation Top App Bar [docs](https://material.io/develop/web/components/top-app-bar/) were consulted.

All of its variants were included in this component.

## Getting Started

1. Add [Nuget](https://www.nuget.org/packages/Mobsites.Blazor.TopAppBar/) package:

```shell
dotnet add package Mobsites.Blazor.TopAppBar
```

Next check out our new [docs](https://www.mobsites.com/blazor/top-app-bar) to help you get started.
