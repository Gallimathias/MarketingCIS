using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// <summary>
    /// Interaktionslogik für MailAccounts.xaml
    /// </summary>
    public partial class MailAccounts : Page
    {
        private SettingsHandler settingsHandler;
        private ObservableCollection<MailAccount> mailAccounts;

        public MailAccounts(SettingsHandler settingsHandler)
        {
            this.settingsHandler = settingsHandler;
            mailAccounts = new ObservableCollection<MailAccount>();
            InitializeComponent();
            InitializeMailAccount();
        }

        private void InitializeMailAccount()
        {
            AccountGrid.ItemsSource = mailAccounts;
            AccountGrid.IsEnabled = false;
            Task.Run(() =>
            {
                if (settingsHandler.GetMailAccountsCount() > 0)
                    mailAccounts = settingsHandler.GetMailAccounts();
                else
                    mailAccounts = new ObservableCollection<MailAccount>();

                mailAccounts.CollectionChanged += MailAccounts_CollectionChanged;
                AccountGrid.Dispatcher.Invoke(() =>
                {
                    AccountGrid.ItemsSource = mailAccounts;
                    AccountGrid.IsEnabled = true;
                });
            });
        }

        private void MailAccounts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Add:
                    settingsHandler.AddMailAccounts(e.NewItems, e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Remove:
                    settingsHandler.RemoveMailAccounts(e.NewItems, e.OldItems);
                    break;
                default:
                    break;
            }
        }
    }
}
