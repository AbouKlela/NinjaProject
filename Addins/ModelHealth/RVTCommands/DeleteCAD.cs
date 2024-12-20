using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NinjaProject.Common;

namespace NinjaProject.Addins.ModelHealth.RVTCommands
{
    [TransactionAttribute(TransactionMode.Manual)]
    internal class DeleteCAD : IExternalCommand
    {
        UIDocument uidoc;
        Document doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;


            FilteredElementCollector fec = new FilteredElementCollector(doc).OfClass(typeof(CADLinkType));
            int count = fec.Count();
            if (count == 0) { TaskDialog.Show("Info", "No more DWG Imports In The Project."); return Result.Succeeded; }
            else
            {
                TaskDialogResult dia = doc.YesNoMessage($"Are You Sure You Want To Delete {count} CAD Files?\nThis CAN NOT BE UNDONE!");
                if (dia == TaskDialogResult.No) return Result.Cancelled;
            }
            Transaction tr = new Transaction(doc, "Delete CAD Imports");
            tr.Start();
            doc.Delete(fec.Select(x => x.Id).ToArray());
            TaskDialog.Show("Done", $"Successfully deleted {count} CAD Files.");
            tr.Commit();
            tr.Dispose();
            return Result.Succeeded;
        }
    }
}
