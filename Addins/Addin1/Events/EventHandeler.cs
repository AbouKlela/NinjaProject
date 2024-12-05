using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace NinjaProject.Addins.Addin1.Events
{
    internal class EventHandeler : IExternalEventHandler
    {
        public RequestEnum Request { get; set; }
        public void Execute(UIApplication app)
        {
            switch (Request)
            {
                case RequestEnum.Request1:
                    TaskDialog.Show("Request 1", "Request 1 was executed");
                    break;
                case RequestEnum.Request2:
                    // Do something
                    break;
                case RequestEnum.Request3:
                    // Do something
                    break;
            }
        }

        public string GetName()
        {
            return "EventHandeler";
        }
    }
}


