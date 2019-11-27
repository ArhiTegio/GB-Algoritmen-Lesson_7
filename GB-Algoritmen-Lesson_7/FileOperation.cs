using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using static System.Console;

namespace GB_Algoritmen_Lesson_7
{
    /// <summary>
    /// Класс наследник для Dictionary с реализацией интерфейса XML сериализации
    /// </summary>
    /// <typeparam name="TKey">Ключ</typeparam>
    /// <typeparam name="TValue">Значение</typeparam>
    [XmlRoot("dictionary")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Чтение из XML
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty) return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                var key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                var value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// запись из XML
        /// </summary>
        /// <param name="reader"></param>
        public void WriteXml(XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }

    public static class FileOperation<T>
    {
        public static void SaveAsXmlFormat(T obj, string fileName)
        {
            // Сохранить объект класса Student в файле fileName в формате XML
            // typeof(Student) передает в XmlSerializer данные о классе.
            // Внутри метода Serialize происходит большая работа по постройке
            // графа зависимостей для последующего создания xml-файла.
            // Процесс получения данных о структуре объекта называется рефлексией.
            XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
            // Создаем файловый поток(проще говоря, создаем файл)
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            // В этот поток записываем сериализованные данные(записываем xml-файл)
            xmlFormat.Serialize(fStream, obj);
            fStream.Close();
        }


        public static T LoadFromXmlFormat(string fileName)
        {
            // Считать объект Student из файла fileName формата XML
            XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            T obj = default(T);
            try
            {
                obj = (T)xmlFormat.Deserialize(fStream);
            }
            catch
            {

            }
            fStream.Close();
            return obj;
        }
        public static void SaveAsXmlCollectionFormat(List<T> obj, string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write); ;
            try
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<T>));
                xmlFormat.Serialize(fStream, obj);
                fStream.Close();
            }
            catch (Exception e)
            {
                WriteLine($"Ошибка при записе. {e.Message}");
                fStream.Close();
            }

        }
        public static List<T> LoadFromXmlCollectionFormat(string fileName)
        {
            List<T> obj = new List<T>();
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<T>));
                obj = (xmlFormat.Deserialize(fStream) as List<T>);
                fStream.Close();
            }
            catch (Exception e)
            {
                WriteLine($"Ошибка при записе. {e.Message}");
                fStream.Close();
            }
            return obj;
        }

        public static void ConvertCSVtoXML(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var xml = new XElement("TopElement",
               lines.Select(line => new XElement("Item",
                  line.Split(';')
                      .Select((column, index) => new XElement("Column" + index, column)))));

            xml.Save($"{fileName}.xml");
        }
    }
}
