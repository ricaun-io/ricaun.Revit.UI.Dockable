using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Dockable.Extensions;
using ricaun.Revit.UI.Dockable.Revit.Views;
using System;

namespace ricaun.Revit.UI.Dockable.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public static DockablePaneCreatorService dockablePaneCreatorService;
        public Result OnStartup(UIControlledApplication application)
        {
            dockablePaneCreatorService = new DockablePaneCreatorService(application);
            dockablePaneCreatorService.Initialize();
            application.ControlledApplication.ApplicationInitialized += (sender, args) =>
            {
                dockablePaneCreatorService.Register(DockablePage.Guid, new DockablePage(), new DockablePaneProvider(), new DockablePaneHideWhenFamilyDocument());
            };

            ribbonPanel = application.CreatePanel("Dockable");
            ribbonPanel.CreatePushButton<Commands.Command>("Show/Hide")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            dockablePaneCreatorService.Dispose();

            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

    public class DockablePaneProvider : IDockablePaneProvider
    {
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.VisibleByDefault = true;
            data.InitialState = new DockablePaneState()
            {
                DockPosition = DockPosition.Bottom,
            };
        }
    }

    public class DockablePaneHideWhenFamilyDocument : IDockablePaneDocumentProvider
    {
        public void DockablePaneChanged(DockablePaneDocumentData data)
        {
            var isFamilyDocument = data.Document?.IsFamilyDocument == true;

            if (isFamilyDocument)
            {
                data.DockablePane.TryHide();
            }
        }
    }
}
