using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
    /// Interaktionslogik für Tickets.xaml
    /// </summary>
    public partial class Tickets : Page
    {
        private SettingsHandler settingsHandler;
        private ObservableCollection<Ticket> tickets;

        public Tickets(SettingsHandler settingsHandler)
        {
            this.settingsHandler = settingsHandler;
            tickets = new ObservableCollection<Ticket>();
            InitializeComponent();
            InitializeDataGrid();
            MainDataGrid.SizeChanged += MainDataGrid_SizeChanged;
            MainGrid.SizeChanged += MainDataGrid_SizeChanged;
            SizeChanged += MainDataGrid_SizeChanged;
        }

        private void MainDataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var column in MainDataGrid.Columns)
            {
                if (MainDataGrid.Columns.Count != 0 &&
                    MainDataGrid.Width != 0 &&
                    !double.IsNaN(MainDataGrid.Width))
                {
                    column.Width = MainDataGrid.Width / MainDataGrid.Columns.Count;
                    MainDataGrid.ColumnWidth = MainDataGrid.Width / MainDataGrid.Columns.Count;
                    MainDataGrid.UpdateLayout();
                }
            }
        }

        private void InitializeDataGrid()
        {
            MainDataGrid.ItemsSource = tickets;
            
            Task.Run(() =>
            {
                tickets = settingsHandler.GetTickets();
                tickets.CollectionChanged += Tickets_CollectionChanged;

                MainDataGrid.Dispatcher.Invoke(() =>
                        MainDataGrid.ItemsSource = tickets);
            });
        }

        private void Tickets_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }
    }
}
