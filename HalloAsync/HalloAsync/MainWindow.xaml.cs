using Microsoft.Data.SqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

            }
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                await Task.Delay(100);
            }

            ((Button)sender).IsEnabled = true;

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
            cts = new CancellationTokenSource();

            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (cts.Token.IsCancellationRequested)
                        break;
                    //alternative
                    //cts.Token.ThrowIfCancellationRequested();

                    Task.Factory.StartNew(() =>
                        pb1.Value = i,
                        cts.Token,
                        TaskCreationOptions.None,
                        ts);
                    Thread.Sleep(100);
                }

            }).ContinueWith(t => ((Button)sender).IsEnabled = true,
                                 CancellationToken.None,
                                 TaskContinuationOptions.None,
                                 ts);
        }
        CancellationTokenSource cts;

        private void Abort(object sender, RoutedEventArgs e)
        {
            cts?.Cancel(); 
        }

        private void LoadFromDB(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
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

                cts.Token.ThrowIfCancellationRequested();

                return count;
            });
            t.ContinueWith(t => MessageBox.Show($"{t.Result} DBs wurden gefunden"), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, ts);
            t.ContinueWith(t =>
            {
                pb1.IsIndeterminate = false;
                ((Button)sender).IsEnabled = true;
            }, ts);
            t.ContinueWith(t =>
            {
                if (t.Exception.InnerException is OperationCanceledException)
                    MessageBox.Show(this, $"Der Vorgang wurde erfolgreich abgebrochen");
                else
                    MessageBox.Show(this, $"Fehler {t.Exception.InnerException.Message}");

            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts);
        }

        private async void LoadFromDBAsyncAwait(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            pb1.IsIndeterminate = true;
            ((Button)sender).IsEnabled = false;

            try
            {
                var conString = "Server=(localdb)\\Mssqllocaldb;Database=master;Trusted_Connection=true;";
                var con = new SqlConnection(conString);
                await con.OpenAsync(cts.Token);
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT count(*) FROM sys.Databases; WAITFOR DELAY '0:0:10'";
                var count = await cmd.ExecuteScalarAsync(cts.Token);

                MessageBox.Show($"{count} DBs wurden gefunden");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show($"Aborted");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler {ex.Message}");
            }

            pb1.IsIndeterminate = false;
            ((Button)sender).IsEnabled = true;
        }

        private async void StartAltUndLangsam(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{AlteUndLangsam(3)}");
            MessageBox.Show($"{await AlteUndLangsamAsync(3)}");
        }

        public Task<long> AlteUndLangsamAsync(int zahl)
        {
            return Task.Run(() => AlteUndLangsam(zahl));
        }


        public long AlteUndLangsam(int zahl)
        {
            Thread.Sleep(5000);
            return 90847893459785 * zahl;
        }
    }
}
