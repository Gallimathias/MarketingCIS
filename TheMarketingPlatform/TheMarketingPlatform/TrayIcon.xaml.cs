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
using System.Windows.Forms;
using TheMarketingPlatfom.Client;


namespace TheMarketingPlatform
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class TrayIcon : Window
    {
        private NotifyIcon notifyIcon;
        private SettingsHandler settingsHandler;

        public TrayIcon(SettingsHandler settingsHandler)
        {
            this.settingsHandler = settingsHandler;
            InitializeComponent();
            InitializeNotifyIcon();
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon()
            {
                Visible = true,
                Icon = new System.Drawing.Icon("Speech-Bubble-Icons.ico")
            };


            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            var closeItem = new System.Windows.Forms.MenuItem("Close");
            var mainItem = new System.Windows.Forms.MenuItem("Show MainWindow");
            mainItem.Click += (s, e) => new MainWindow(settingsHandler).Show();
            closeItem.Click += (s, e) => Close();
            notifyIcon.ContextMenu.MenuItems.Add(mainItem);
            notifyIcon.ContextMenu.MenuItems.Add(closeItem);
        }
        
    }
}
