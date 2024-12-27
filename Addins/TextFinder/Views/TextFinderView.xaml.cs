using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NinjaProject.Addins.TextFinder.Views
{
    /// <summary>
    /// Interaction logic for TextFinderView.xaml
    /// </summary>
    public partial class TextFinderView : Window
    {
        public TextFinderView()
        {
            
            InitializeComponent();
            DataContext = new NinjaProject.Addins.TextFinder.ViewModels.ViewModel1();
        }

       
       

        private void TextFinderView_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}
