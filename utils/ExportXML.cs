using org.tyaa.e14_440fileviewernet.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace E14_440FileViewer.NET.utils
{
    class ExportXML
    {
        //файл трансформации
        private const String STYLESHEET_URI_STRING = "xml-html.xsl";

        public XDocument getChannelsMetadata(ref ChannelsBundle _ChannelsBundle)
        {

            //Корневой узел-элемент
            XElement root = new XElement("bundle");
            //Элемент - коллекция каналов
            XElement channels = new XElement("channels");

            //Число каналов
            int channelsLength = _ChannelsBundle.length();

            //Сколько каналов - столько дочерних элементов
            //добавляем в корневой элемент
            for (int i = 0; i < channelsLength; i++)
            {
                XElement channel = new XElement("channel");
                //Первый дочерний узел в элементе "канал" - атрибут "номер канала"
                channel.SetAttributeValue("number", _ChannelsBundle.numbersArray[i]);
                //Второй дочерний узел в элементе "канал" - элемент "усиление канала"
                XElement amp = new XElement("amplification");
                amp.Add(_ChannelsBundle.ampsArray[i]);
                channel.Add(amp);

                //Третий дочерний узел в элементе "канал" - элемент "данные"
                XElement data = new XElement("data");
                foreach (var itemValue in _ChannelsBundle[i].dataArrayList)
                {
                    XElement itemElement = new XElement("item");
                    itemElement.Add(itemValue);
                    data.Add(itemElement);
                }
                channel.Add(data);

                //Добавляем элемент "канал" в элемент-коллекцию каналов
                channels.Add(channel);
            }

            //Создаем узел-элемент с информацией о частоте            
            XElement frequency = new XElement("frequency");
            frequency.Add(_ChannelsBundle.frequency);

            //...и добавляем его корню
            root.Add(frequency);
            //также в корень добавляем элемент-коллекцию элементов каналов
            root.Add(channels);

            //Создаем и возвращаем объект xml-документа с заголовком и корневым элементом
            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), root);
        }

        public void saveChannelsSettings(string savePath, string _dataFileName)
        {

            //Корневой узел-элемент
            XElement root = new XElement("setting");
            //Элемент - коллекция каналов
            XElement channels = new XElement("channelsSettings");

            //Число каналов
            /*int channelsLength = _ChannelsBundle.length();

            //Сколько каналов - столько дочерних элементов
            //добавляем в корневой элемент
            for (int i = 0; i < channelsLength; i++)
            {
                XElement channel = new XElement("channel");
                //Первый дочерний узел в элементе "канал" - атрибут "номер канала"
                channel.SetAttributeValue("number", _ChannelsBundle.numbersArray[i]);
                //Второй дочерний узел в элементе "канал" - элемент "усиление канала"
                XElement amp = new XElement("amplification");
                amp.Add(_ChannelsBundle.ampsArray[i]);
                channel.Add(amp);

                //Третий дочерний узел в элементе "канал" - элемент "данные"
                XElement data = new XElement("data");
                foreach (var itemValue in _ChannelsBundle[i].dataArrayList)
                {
                    XElement itemElement = new XElement("item");
                    itemElement.Add(itemValue);
                    data.Add(itemElement);
                }
                channel.Add(data);

                //Добавляем элемент "канал" в элемент-коллекцию каналов
                channels.Add(channel);
            }*/

            //Создаем узел-элемент с информацией о частоте            
            XElement frequency = new XElement("dataFileName");
            frequency.Add(_dataFileName);

            //...и добавляем его корню
            root.Add(frequency);
            //также в корень добавляем элемент-коллекцию элементов каналов
            root.Add(channels);

            //Создаем и возвращаем объект xml-документа с заголовком и корневым элементом
            new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), root)
                .Save(savePath);
        }

        public void saveChannelsMetadata(ref ChannelsBundle _ChannelsBundle, ReportTypes _reportType)
        {
            XDocument xdoc = getChannelsMetadata(ref _ChannelsBundle);
            switch (_reportType)
            {
                case ReportTypes.JSON:
                    break;
                case ReportTypes.XML:
                    //Сохраняем xml-документ в файл
                    xdoc.Save("ChannelsMetadata.xml");
                    break;
                case ReportTypes.HTML:
                    //создаем объект файла стилей xsl
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    //заполняем его из файла
                    xslt.Load(STYLESHEET_URI_STRING);
                    //создаем обект записи, указываем ему имя выходного файла
                    XmlTextWriter xmlTextWriter =
                        new XmlTextWriter("ChannelsMetadata.html", Encoding.UTF8);
                    //устанавливаем сохранение форматирования
                    xmlTextWriter.Formatting = Formatting.Indented;
                    //запускаем трансформацию с выводом в файл, который можно будет открыть в MS Word
                    using (var xmlReader = xdoc.CreateReader())
                    {
                        xslt.Transform(xmlReader, xmlTextWriter);
                    }
                    Console.WriteLine("HTML file was saved!");
                    break;
                default:
                    break;
            }
        }
    }
}
