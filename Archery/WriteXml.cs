using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archery
{
    class WriteXml
    {
        XmlWriterSettings settings = new XmlWriterSettings();

        private void xmlSettings()
        {
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.OmitXmlDeclaration = false;
            settings.Encoding = Encoding.UTF8;
        }

        public void createXml(string getXmlFile)
        {
            if (!File.Exists(getXmlFile))
            {
                xmlSettings();
                using (XmlWriter writer = XmlWriter.Create(getXmlFile, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("game");

                    writer.WriteStartElement("archery");
                        writer.WriteStartElement("data");
                            writer.WriteElementString("windowHeight", "500"); // {0}
                            writer.WriteElementString("windowWidth", "900");    // {1}
                            writer.WriteElementString("windowName", "Archery"); // {2}
                            writer.WriteElementString("windowText", "Archery"); // {3}
                            writer.WriteElementString("score", "0"); // {4}
                            writer.WriteElementString("arrows", "50"); // {5}
                            writer.WriteElementString("duration", "60"); // {6}
                            writer.WriteElementString("timeInterval", "1000"); // {7}                     
                            writer.WriteElementString("showScoreText", "No games have been played yet"); // {8}
                        writer.WriteEndElement();            
                    writer.WriteEndElement();

                    writer.WriteStartElement("archer");
                        writer.WriteStartElement("data");
                            writer.WriteElementString("archerHeight", "100"); // {9}
                            writer.WriteElementString("archerWidth", "90"); // {10}
                            writer.WriteElementString("archerSpeed", "10"); // {11}
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteStartElement("arrow");
                        writer.WriteStartElement("data");
                            writer.WriteElementString("arrowHeight", "20"); // {12}
                            writer.WriteElementString("arrowWidth", "60"); // {13}
                            writer.WriteElementString("arrowSpeed", "30"); // {14}
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteStartElement("enemies");
                        writer.WriteStartElement("data");
                            writer.WriteElementString("enemiesHeight", "50"); // {15}
                            writer.WriteElementString("enemiesWidth", "50"); // {16}
                            writer.WriteElementString("enemiesSpeed1", "6"); // {17}
                            writer.WriteElementString("enemiesSpeed2", "8"); // {18}
                            writer.WriteElementString("enemiesSpeed3", "10"); // {19}
                            writer.WriteElementString("enemiesSpeed4", "20"); // {20}
                            writer.WriteElementString("enemiesSpeed5", "30"); // {21}
                        writer.WriteEndElement();
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.Close();
                }
            }
        }
    }
}