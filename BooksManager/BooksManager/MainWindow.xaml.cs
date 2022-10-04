using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows;

namespace BooksManager
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

        private async void Such(object sender, RoutedEventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={suchTb.Text}";

            var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            BooksResult result = JsonSerializer.Deserialize<BooksResult>(json);

            myGrid.ItemsSource = result.items.Select(x => x.volumeInfo);
        }

        private void SaveJson(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "JSON-Datei|*.json|Alles Dateien|*.*" };
            if (dlg.ShowDialog().Value)
            {
                IEnumerable<Volumeinfo> data = (IEnumerable<Volumeinfo>)myGrid.ItemsSource;
                var json = JsonSerializer.Serialize(data);
                File.WriteAllText(dlg.FileName, json);
            }
        }

        private void LoadJson(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "JSON-Datei|*.json|Alles Dateien|*.*" };
            if (dlg.ShowDialog().Value)
            {
                var json = File.ReadAllText(dlg.FileName);
                myGrid.ItemsSource = JsonSerializer.Deserialize<List<Volumeinfo>>(json);
            }
        }
    }
}
