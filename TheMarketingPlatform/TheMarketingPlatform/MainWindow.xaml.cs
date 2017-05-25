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
using System.Windows.Shapes;
using TheMarketingPlatfom.Client;

namespace TheMarketingPlatform
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsHandler settingsHandler;
        public MainWindow(SettingsHandler settingsHandler)
        {
            this.settingsHandler = settingsHandler;
            InitializeComponent();
            InitializeMenuItems();

            if (!(bool)settingsHandler["Initializes"])
                MainFrame.Navigate(ContenManager.GetPage("Settings", settingsHandler));
            else
                MainFrame.Navigate(ContenManager.GetPage("Tickets", settingsHandler));

            SizeChanged += (s, e) => Console.WriteLine($"Width: {Width}| Height: {Height}");
        }

        private void InitializeMenuItems()
        {
            foreach (MenuItem menuItem in MainMenu.Items)
                menuItem.Click += MenuItem_Click;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(
                ContenManager.GetPage((string)((MenuItem)sender).Tag, settingsHandler));
        }
    }
}
