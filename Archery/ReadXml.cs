using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Archery
{
    class ReadXml
    {
        List<string> values = new List<string>();

        public List<string> readFromXml(string getXmlFile)
        {
            XmlTextReader xtr = new XmlTextReader(getXmlFile);

            do
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "windowHeight")
                    values.Add(xtr.ReadElementString()); // {0}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "windowWidth")
                    values.Add(xtr.ReadElementString()); // {1}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "windowName")
                    values.Add(xtr.ReadElementString()); // {2}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "windowText")
                    values.Add(xtr.ReadElementString()); // {3}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "score")
                    values.Add(xtr.ReadElementString()); // {4}  
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "arrows")
                    values.Add(xtr.ReadElementString()); // {5}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "duration")
                    values.Add(xtr.ReadElementString()); // {6}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "timeInterval")
                    values.Add(xtr.ReadElementString()); // {7}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "showScoreText")
                    values.Add(xtr.ReadElementString()); // {8}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "archerHeight")
                    values.Add(xtr.ReadElementString()); // {9}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "archerWidth")
                    values.Add(xtr.ReadElementString()); // {10}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "archerSpeed")
                    values.Add(xtr.ReadElementString()); // {11}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "arrowHeight")
                    values.Add(xtr.ReadElementString()); // {12}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "arrowWidth")
                    values.Add(xtr.ReadElementString()); // {13}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "arrowSpeed")
                    values.Add(xtr.ReadElementString()); // {14}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesHeight")
                    values.Add(xtr.ReadElementString()); // {15}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesWidth")
                    values.Add(xtr.ReadElementString()); // {16}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesSpeed1")
                    values.Add(xtr.ReadElementString()); // {17}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesSpeed2")
                    values.Add(xtr.ReadElementString()); // {18}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesSpeed3")
                    values.Add(xtr.ReadElementString()); // {19}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesSpeed4")
                    values.Add(xtr.ReadElementString()); // {20}
                else if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "enemiesSpeed5")
                    values.Add(xtr.ReadElementString()); // {21}
            } while (xtr.Read());
            xtr.Close();
        return values;
        }
     
        List<string> scores { get; set; } = new List<string>();

        public List<string> readXmlScores(string getXmlScore, string getPlayerName)
        { 
            var xDoc = XDocument.Load(getXmlScore);

            var gameData = from file in xDoc.Root.Elements("save")
                           select new
                          {
                              playerScore = (string)file.Element("score"),
                              playerName = (string)file.Element("player")
                          };

            int game = 1;
            foreach (var data in gameData)
            {
                scores.Add(game + ")" + " => " + "(Score: " + data.playerScore + ") " + "=> " + "(Played: " + data.playerName + ")" + Environment.NewLine);
                game++;
            }
            scores.Reverse();

            return scores;
        }
    }
}