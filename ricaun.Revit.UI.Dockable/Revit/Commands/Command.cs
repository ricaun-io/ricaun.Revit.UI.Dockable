using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.Dockable.Extensions;
using ricaun.Revit.UI.Dockable.Revit.Views;

namespace ricaun.Revit.UI.Dockable.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            var pane = App.dockablePaneCreatorService.Get(DockablePage.Guid);
            if (pane.TryIsShown())
            {
                pane.TryHide();
            }
            else
            {
                pane.TryShow();
            }

            return Result.Succeeded;
        }
    }
}
