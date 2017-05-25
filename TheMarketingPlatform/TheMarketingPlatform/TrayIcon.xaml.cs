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
using TheMarketingPlatform.Client;


namespace TheMarketingPlatform
{
    public partial class TrayIcon : Window
    {
        private NotifyIcon notifyIcon;
        private SettingsHandler settingsHandler;

        public TrayIcon(SettingsHandler settingsHandler)
        {
            this.settingsHandler = settingsHandler;
            InitializeComponent();
            InitializeNotifyIcon();

            settingsHandler.OnNotify += SettingsHandler_OnNotify; ;
        }

        private void SettingsHandler_OnNotify(object sender, Notification e) =>
            notifyIcon.ShowBalloonTip(e.Timeout, e.TipTitle, e.TipText, (ToolTipIcon)e.TipIcon);


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
