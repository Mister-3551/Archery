using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archery
{
    class SaveScoresXml
    {
        public void saveScores(string getXmlScore, int getScore, string getPlayerName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root;

            if (!File.Exists(getXmlScore))
            {
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                root = doc.CreateElement("game");

                XmlElement save = doc.CreateElement("save");
                XmlElement score = doc.CreateElement("score");
                XmlElement playerName = doc.CreateElement("player");

                score.InnerText = getScore.ToString();
                playerName.InnerText = getPlayerName.ToString();

                doc.AppendChild(declaration);
                doc.AppendChild(root);
                doc.AppendChild(root);
                save.AppendChild(score);
                save.AppendChild(playerName);
                root.AppendChild(save);

                doc.Save(getXmlScore);
            }
            else
            {
                doc = new XmlDocument();
                doc.Load(getXmlScore);
                root = doc.DocumentElement;
                XmlElement save = doc.CreateElement("save");
                XmlElement score = doc.CreateElement("score");
                XmlElement playerName = doc.CreateElement("player");

                score.InnerText = getScore.ToString();
                playerName.InnerText = getPlayerName.ToString();

                save.AppendChild(score);
                save.AppendChild(playerName);
                root.AppendChild(save);
                doc.Save(getXmlScore);
            }
        }
    }
}