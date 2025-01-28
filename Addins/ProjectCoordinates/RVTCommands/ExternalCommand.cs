using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NinjaProject.Addins.ProjectCoordinates.RVTCommands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class ExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIApplication uiApp = commandData.Application;

            var ProjectBasePoint = new FilteredElementCollector(doc).OfClass(typeof(BasePoint)).OfCategory(BuiltInCategory.OST_ProjectBasePoint).Cast<BasePoint>().First();
            var SurveyPoint = new FilteredElementCollector(doc).OfClass(typeof(BasePoint)).OfCategory(BuiltInCategory.OST_SharedBasePoint).Cast<BasePoint>().First();
            double rotation = 20*Math.PI/ 180;
            double newEastWest = 30.0/304.8;
            double newNorthSouth = 20.0/304.8;
            double newElevation = 10.0 / 304.8;

            Transaction t = new Transaction(doc, "Clipping Base Points");

            t.Start();
            if (SurveyPoint.Clipped == true)
            {
                SurveyPoint.Clipped = false;
                SurveyPoint.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM)
                    .Set(newEastWest);
                SurveyPoint.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM)
                    .Set(newNorthSouth);
                SurveyPoint.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM)
                    .Set(newElevation);
                TaskDialog.Show("OK", "Survey Point is clipped");
                SurveyPoint.Clipped = true;

            }
            else
            {
                TaskDialog.Show("OK", "Survey Point is not clipped");
                SurveyPoint.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM)
                    .Set(newEastWest);
                SurveyPoint.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM)
                    .Set(newNorthSouth);
                SurveyPoint.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM)
                    .Set(newElevation);
                SurveyPoint.Clipped = true;
            }


            ProjectBasePoint.get_Parameter(BuiltInParameter.BASEPOINT_ANGLETON_PARAM).Set(rotation);
            ProjectBasePoint.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM)
                .Set(newEastWest);
            ProjectBasePoint.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM)
                .Set(newNorthSouth);
            ProjectBasePoint.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM)
                .Set(newElevation);


            t.Commit();



            return Result.Succeeded;
        }
    }
}
