# ricaun.Revit.UI.Dockable

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)
[![Release](https://img.shields.io/nuget/v/ricaun.Revit.UI.Dockable?logo=nuget&label=release&color=blue)](https://www.nuget.org/packages/ricaun.Revit.UI.Dockable)

Package to help create `DockablePane` for Revit API developers.

This project was generated by the [ricaun.AppLoader](https://ricaun.com/AppLoader/) Revit plugin.

## DockablePaneCreatorService

The `DockablePaneCreatorService` class implements some methods to `Register` a `FrameworkElement` object and `Get` the `DockablePane` registered in Revit.

```C#
public class App : IExternalApplication
{
    public static DockablePaneCreatorService dockablePaneCreatorService;
    public Result OnStartup(UIControlledApplication application)
    {
        dockablePaneCreatorService = new DockablePaneCreatorService(application);
        dockablePaneCreatorService.Initialize();
        dockablePaneCreatorService.Register(DockablePage.Guid, new DockablePage());

        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        dockablePaneCreatorService.Dispose();
        return Result.Succeeded;
    }
}
```
```C#
public partial class DockablePage : Page, IDockablePaneProvider, IDockablePaneDocumentProvider
{
    public void SetupDockablePane(DockablePaneProviderData data)
    {
        data.InitialState = new DockablePaneState
        {
            DockPosition = DockPosition.Tabbed,
        };
    }
    public void DockablePaneChanged(DockablePaneDocumentData data)
    {
        // Hide DockablePane if Document is FamilyDocument
        var isFamilyDocument = data.Document?.IsFamilyDocument == true;
        if (isFamilyDocument)
        {
            data.DockablePane.TryHide();
        }
    }
}
```

### Initialize / Dispose

The `Initialize` method is used to initialize the `DockablePaneCreatorService` and the `Dispose` method is used to dispose the `DockablePaneCreatorService`. The `Initialize` register the events `Idling` and `DockableFrameVisibilityChanged`.

### IDockablePaneProvider

The `IDockablePaneProvider` is the Revit UI interface to gather information about a dockable pane initialization.

### IDockablePaneDocumentProvider

The `IDockablePaneDocumentProvider` is the interface to detect when the `DockablePaneChanged` with information about the `DockablePaneId`, `FrameworkElement`, `Document` and `DockablePane`.

### Register

To `Register` a `DockablePane` in Revit, you need to provide the `Guid` of the `DockablePane` and the `Page`, the best place to do this is in the `IExternalApplication.OnStartup` method.

**The `Register` of a `DockablePane` only works before Revit finish initialize.**

```C#
dockablePaneCreatorService.Register(DockablePage.Guid, new DockablePage());
```

#### Register with title

To `Register` a `DockablePane` in Revit with a title, you need to provide the `Guid` of the `DockablePane`, the `Page` and the `title`, by default the if no title is provide the `DockablePane` will be registered with title of the `Page` if exists.
```C#
dockablePaneCreatorService.Register(DockablePage.Guid, "Dockable Title", new DockablePage());
```

#### Register with IDockablePaneProvider

To `Register` a `DockablePane` in Revit with a `IDockablePaneProvider`, you need to provide the `Guid` of the `DockablePane` and the `Page` with the interface `IDockablePaneProvider`, by default if the `Page` contain the interface that gonna be register.
```C#
dockablePaneCreatorService.Register(DockablePage.Guid, "Dockable Title", new DockablePage(), new DockablePaneProvider());
```

#### Register with IDockablePaneDocumentProvider

To `Register` a `DockablePane` in Revit with a `IDockablePaneDocumentProvider`, you need to provide the `Guid` of the `DockablePane` and the `Page` with the interface `IDockablePaneDocumentProvider`, by default if the `Page` contain the interface that gonna be register.
```C#
dockablePaneCreatorService.Register(DockablePage.Guid, "Dockable Title", new DockablePage(), new DockablePaneDocumentProvider());
```

### Get

To `Get` a `DockablePane` in Revit, you need to provide the `Guid` of the `DockablePane`.
```C#
DockablePane dockablePane = App.dockablePaneCreatorService.Get(DockablePage.Guid);
```

### GetFrameworkElement

To `GetFrameworkElement` of a `DockablePane` in Revit, you need to provide the `Guid` of the `DockablePane`.
```C#
FrameworkElement frameworkElement = App.dockablePaneCreatorService.GetFrameworkElement(DockablePage.Guid);
```

### Extensions

The `DockablePaneExtension` class implements some extensions methods to `TryShow`, `TryHide`, `TryGetTitle` and `TryIsShown` the `DockablePane` registered in Revit. 

## Release

* [Latest release](../../releases/latest)

## Video

Video in English about this project.

[![VideoIma1]][Video1]
[![VideoIma2]][Video2]

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!

[Video1]: https://youtu.be/6Vh0yhkVrxI
[VideoIma1]: https://img.youtube.com/vi/6Vh0yhkVrxI/mqdefault.jpg
[Video2]: https://youtu.be/GX35wW-RO6w
[VideoIma2]: https://img.youtube.com/vi/GX35wW-RO6w/mqdefault.jpg