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
using TheMarketingPlatform.Client;
using TheMarketingPlatform.Core.JSON;

namespace TheMarketingPlatform.Views
{
    public partial class Settings : Page
    {
        private SettingsHandler settingsHandler;
        private Config serverConfig;
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
            if (settingsHandler.Client == null)
                return;

            var textBox = (TextBox)sender;
            var tag = (string)textBox.Tag;
            if (tag[0] == '+')
                serverConfig.Settings[tag.Substring(1)] = textBox.Text;
            else
                settingsHandler[tag] = textBox.Text;
            settingsHandler.ApplyChangesToFile();
            settingsHandler.SendConfigToServer(serverConfig);
        }

        private void InitializeFileSettings()
        {
            if (settingsHandler.Client == null)
                return;

            serverConfig = settingsHandler.GetServerSettings();
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

            ShowServerConfig(serverConfig);
        }

        private void ShowServerConfig(Config serverConfig)
        {
            if (serverConfig == null)
                return;

            LuisIdBox.Text = (string)serverConfig.Settings["LUISAppId"];
            LuisIdBox.Tag = "+LUISAppId";
            LuisKeyBox.Text = (string)serverConfig.Settings["LUISAppKey"];
            LuisKeyBox.Tag = "+LUISAppKey";
            ServerPortBox.Text = ((long)serverConfig.Settings["ServerPort"]).ToString();
            ServerPortBox.Tag = "+ServerPort";
            MailHandlingBox.Text = ((long)serverConfig.Settings["MailServiceHandlerPeriod"]).ToString();
            MailHandlingBox.Tag = "+MailServiceHandlerPeriod";
            MailCallBox.Text = ((long)serverConfig.Settings["MailServicePeriod"]).ToString();
            MailCallBox.Tag = "+MailServicePeriod";
            DatabaseBox.Text = (string)serverConfig.Settings["DatabaseConnectionString"];
            DatabaseBox.Tag = "+DatabaseConnectionString";
        }
    }
}
