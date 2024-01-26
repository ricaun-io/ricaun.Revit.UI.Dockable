using System;
using System.Windows;
using System.Windows.Controls;

namespace ricaun.Revit.UI.Dockable.Revit.Views
{
    public partial class DockablePage : Page
    {
        public static Guid Guid => new Guid("11111111-1111-1111-2222-111111111111");
        public DockablePage()
        {
            InitializeComponent();
        }

        public int Number { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.Content = ++Number;
        }
    }
}
