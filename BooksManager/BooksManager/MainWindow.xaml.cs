using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Xml.Serialization;

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

        private void SaveXML(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "XML-Datei|*.xml|Alles Dateien|*.*" };
            if (dlg.ShowDialog().Value)
            {
                //List<Volumeinfo> data = ((IEnumerable<Volumeinfo>)myGrid.ItemsSource).ToList();
                //var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                //using var sw = new StreamWriter(dlg.FileName);
                //serial.Serialize(sw, data);
                var sm = new StorageManager();
                sm.SaveAsXML<Volumeinfo>(((IEnumerable<Volumeinfo>)myGrid.ItemsSource).ToList(), dlg.FileName);

            }
        }

        private void SaveStuff(object sender, RoutedEventArgs e)
        {
            var stuff = new List<string>();
            stuff.Add("Hund");
            stuff.Add("Katze");
            stuff.Add("Bier");
            var sm = new StorageManager();
            sm.SaveAsXML(stuff, "stuff.xml");
        }

        private void LoadXML(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "XML-Datei|*.xml|Alles Dateien|*.*" };
            if (dlg.ShowDialog().Value)
            {
                //using var sr = new StreamReader(dlg.FileName);
                //var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                //myGrid.ItemsSource = (List<Volumeinfo>)serial.Deserialize(sr);
                var sm = new StorageManager();
                myGrid.ItemsSource = sm.LoadFromXML<Volumeinfo>(dlg.FileName);
            }
        }

        private void LoadStuff(object sender, RoutedEventArgs e)
        {
            var sm = new StorageManager();
            var stuff = sm.LoadFromXML<string>("stuff.xml");
            MessageBox.Show(string.Join(", ", stuff));
        }
    }
}
