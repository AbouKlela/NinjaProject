using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NinjaProject.Common;

namespace NinjaProject.Addins.TextFinder.ViewModels
{
    public class ViewModel1 :ViewModelBaseClass
    {

		private Document doc = Static.StaticProp.Doc;
        private UIDocument uidoc = Static.StaticProp.UIDoc;

        private List<string> textFoundList;

        public List<string> TextFoundList
        {
            get => textFoundList;
            set => SetProperty(ref textFoundList, value);
        }



        private RelayCommandBaseClass findText;
        public RelayCommandBaseClass FindText => findText ?? (findText = new RelayCommandBaseClass(FindTextFunc));
        private void FindTextFunc(object obj)
        {
           FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> elements = collector.OfCategory(BuiltInCategory.OST_TextNotes).ToElements().ToList();
            List<Element> foundElements = new List<Element>();
            foreach (Element element in elements)
            {
                TextNote textNote = element as TextNote;
                if (textNote == null) continue;
                if (textNote.Text.Contains("Ninja"))
                {
                    foundElements.Add(element);
                }
            }

        }




    }
}
