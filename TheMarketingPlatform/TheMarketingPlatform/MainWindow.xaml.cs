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
using TheMarketingPlatform.Client;

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

            MainFrame.SizeChanged += (s, e) => { Console.WriteLine(); };

            if (!(bool)settingsHandler["Initializes"])
                MainFrame.Navigate(ContenManager.GetPage("Settings", settingsHandler));
            else
                MainFrame.Navigate(ContenManager.GetPage("Tickets", settingsHandler));

        }

        private void InitializeMenuItems()
        {
            foreach (MenuItem menuItem in MainMenu.Items)
                menuItem.Click += MenuItem_Click;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                MainFrame.Navigate(
                    ContenManager.GetPage((string)((MenuItem)sender).Tag, settingsHandler));
            }
            catch (Exception) { }
        }
    }
}
