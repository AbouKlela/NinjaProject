using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NinjaProject.Addins.Addin1.RVTCommands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    internal class ExtCommand1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var eventHandler = new Events.EventHandeler();
            eventHandler.Request = Events.RequestEnum.Request1;
            var ExternalEvent = Autodesk.Revit.UI.ExternalEvent.Create(eventHandler);
            ExternalEvent.Raise();
            TaskDialog.Show("Revit", "External Event Raised");
            return Result.Succeeded;
        }
    }
}
