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
using TheMarketingPlatfom.Client;

namespace TheMarketingPlatform.Views
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private SettingsHandler settingsHandler;
        public Settings(SettingsHandler settingsHandler)
        {
            this.settingsHandler = settingsHandler;
            InitializeComponent();

            InitializeFileSettings();
            InitializeEvents(MainGrid);            
        }

        private void InitializeEvents(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (!(child is TextBox))
                    InitializeEvents(child);
                else if (child is TextBox)
                    ((TextBox)child).TextChanged += TextBox_TextChanged;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            settingsHandler[(string)textBox.Tag] = textBox.Text;
            settingsHandler.ApplyChangesToFile();
        }

        private void InitializeFileSettings()
        {

            HostBox.Text = (string)settingsHandler["Host"];
            HostBox.Tag = "Host";
            PortBox.Text = ((long)settingsHandler["Port"]).ToString();
            PortBox.Tag = "Port";
            RetrieBox.Text = ((long)settingsHandler["MaxConnectionRetries"]).ToString();
            RetrieBox.Tag = "MaxConnectionRetries";

            if (!(bool)settingsHandler["Initializes"])
            {
                settingsHandler["Initializes"] = true;
                settingsHandler.ApplyChangesToFile();
            }
        }
    }
}
