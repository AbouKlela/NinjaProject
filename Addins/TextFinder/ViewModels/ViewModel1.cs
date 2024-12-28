using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Added for ObservableCollection
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NinjaProject.Common;

namespace NinjaProject.Addins.TextFinder.ViewModels
{
    public class ViewModel1 : ViewModelBaseClass
    {
        private Document doc = Static.StaticProp.Doc;
        private UIDocument uidoc = Static.StaticProp.UIDoc;

        private ObservableCollection<string> textFoundList = new ObservableCollection<string>() {"1","2"};
        public ObservableCollection<string> TextFoundList
        {
            get => textFoundList;
            set =>SetProperty(ref textFoundList, value);

        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                findText?.Execute(null);
            }
        }

        private RelayCommandBaseClass findText;
        public RelayCommandBaseClass FindText => findText ?? (findText = new RelayCommandBaseClass(FindTextFunc));

        private void FindTextFunc(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> elements = collector.OfCategory(BuiltInCategory.OST_TextNotes).ToElements().ToList();
            List<string> foundTexts = new List<string>();

            foreach (Element element in elements)
            {
                TextNote textNote = element as TextNote;
                if (textNote == null) continue;
                if (textNote.Text.Contains(searchText))
                {
                    foundTexts.Add(textNote.Text);
                }
            }

            // Update the TextFoundList property
            TextFoundList = new ObservableCollection<string>(foundTexts);
        }
    }
}