using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace NinjaProject.Addins.ProjectCoordinates.Events
{
    internal class EventHandeler : IExternalEventHandler
    {
        public RequestEnum Enum { get; set; }
        public void Execute(UIApplication app)
        {
            switch (Enum)
            {
                case RequestEnum.Request1:
                    TaskDialog.Show("Request 1", "Request 1 was executed");
                    break;
                case RequestEnum.Request2:
                    // Do something
                    break;
            }

        }

        public string GetName()
        {
            return "EventHandeler";
        }
    }

    public enum RequestEnum
    {
        Request1,
        Request2
    }
}
