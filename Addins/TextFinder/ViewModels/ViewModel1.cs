using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Added for ObservableCollection
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using NinjaProject.Addins.TextFinder.Static;
using NinjaProject.Common;
using static System.Net.Mime.MediaTypeNames;

namespace NinjaProject.Addins.TextFinder.ViewModels
{
    public class ViewModel1 : ViewModelBaseClass
    {
        private Document doc = Static.StaticProp.Doc;
        private UIDocument uidoc = Static.StaticProp.UIDoc;

        public ViewModel1()
        {
                doc.Application.DocumentChanged += Application_documentChanged;

        }


        private ObservableCollection<string> textFoundList;
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
                FindTextFunc();
               
            }
        }

        private void Application_documentChanged(object sender, DocumentChangedEventArgs e)
        {
            FindTextFunc();
        }


        private void FindTextFunc()
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
                if (textNote.Text.Contains(searchText) || textNote.Text.Contains(searchText.ToLower()) || textNote.Text.Contains(searchText.ToUpper()))
                {
                    foundTexts.Add(textNote.Text);
                }
            }

            // Update the TextFoundList property
            TextFoundList = new ObservableCollection<string>(foundTexts);
        }
    }
}