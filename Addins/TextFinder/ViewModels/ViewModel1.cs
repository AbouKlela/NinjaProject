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
        private Document _doc = Static.StaticProp.Doc;
        private UIDocument _uidoc = Static.StaticProp.UIDoc;
        private UIView _uiView;
        public ViewModel1()
        {
            _doc.Application.DocumentChanged += Application_documentChanged;
            _uiView = _uidoc.GetOpenUIViews().FirstOrDefault(x => x.ViewId == _doc.ActiveView.Id);
        }

        private int selectedIndex = 0;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                SetProperty(ref selectedIndex, value);
                TaskDialog.Show("Index", selectedIndex.ToString());
            }
        }


        private ObservableCollection<string> textFoundList;
        public ObservableCollection<string> TextFoundList
        {
            get => textFoundList;
            set => SetProperty(ref textFoundList, value);

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

            FilteredElementCollector collector = new FilteredElementCollector(_doc);
            List<Element> elements = collector.OfCategory(BuiltInCategory.OST_TextNotes).ToElements().ToList();
            List<string> foundTexts = new List<string>();
            List<TextNote> foundTextsNotes = new List<TextNote>();


            foreach (Element element in elements)
            {
                TextNote textNote = element as TextNote;
                if (textNote == null) continue;
                if (textNote.Text.Contains(searchText) || textNote.Text.Contains(searchText.ToLower()) || textNote.Text.Contains(searchText.ToUpper()))
                {
                    foundTexts.Add(textNote.Text);
                    foundTextsNotes.Add(textNote);
                }
            }

            // Update the TextFoundList property
            TextFoundList = new ObservableCollection<string>(foundTexts);
        }
    }
}