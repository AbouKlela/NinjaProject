using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace NinjaProject.Addins.TextFinder.Events
{
    public class EventHandeler : IExternalEventHandler
    {
        public static RequestEnum Request { get; set; }
        public void Execute(UIApplication app)
        {
            switch (Request)
            {
                case RequestEnum.Request1:
                    // Do something
                    break;
                case RequestEnum.Request2:
                    // Do something
                    break;

            }
        }

        public string GetName()
        {
            return "TextFinder";
        }
    }
}
