using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(1000);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(100);
                }
                pb1.Dispatcher.Invoke(() => ((Button)sender).IsEnabled = true);
            });
        }

        private void StartTaskmitTS(object sender, RoutedEventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() =>
                        pb1.Value = i,
                        CancellationToken.None,
                        TaskCreationOptions.None,
                        ts);
                    Thread.Sleep(100);
                }

            }).ContinueWith(t => ((Button)sender).IsEnabled = true,
                                 CancellationToken.None,
                                 TaskContinuationOptions.None,
                                 ts);
        }

        private void LoadFromDB(object sender, RoutedEventArgs e)
        {
            pb1.IsIndeterminate = true;
            ((Button)sender).IsEnabled = false;
            var ts = TaskScheduler.FromCurrentSynchronizationContext();

            var t = Task.Run(() =>
            {
                var conString = "Server=(localdb)\\Mssqllocaldb;Database=master;Trusted_Connection=true;";
                var con = new SqlConnection(conString);
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT count(*) FROM sys.Databases; WAITFOR DELAY '0:0:04'";
                var count = cmd.ExecuteScalar();

                return count;

            });
            t.ContinueWith(t => MessageBox.Show($"{t.Result} DBs wurden gefunden"), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, ts);
            t.ContinueWith(t =>
            {
                pb1.IsIndeterminate = false;
                ((Button)sender).IsEnabled = true;
            }, ts);
            t.ContinueWith(t => MessageBox.Show(this, $"Fehler {t.Exception.InnerException.Message}"),
                                 CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts);

        }
    }
}
