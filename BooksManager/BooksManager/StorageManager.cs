using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace BooksManager
{
    internal class StorageManager
    {
        public void SaveAsXML<T>(IEnumerable<T> data, string fileName)
        {
            var serial = new XmlSerializer(typeof(List<T>));
            using var sw = new StreamWriter(fileName);
            serial.Serialize(sw, data);
        }
        public IEnumerable<T> LoadFromXML<T>(string fileName)
        {
            using var sr = new StreamReader(fileName);
            var serial = new XmlSerializer(typeof(List<T>));
            return (IEnumerable<T>)serial.Deserialize(sr);
        }

    }
}
