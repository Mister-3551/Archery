using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Archery
{
    class Arrow : Archery
    {
        private Archery sendWindow;

        Boolean chooseArrow;
        public PictureBox objectArrow = new PictureBox();

        int arrowX, arrowY, speed, arrowHeight, arrowWidth, arrowNewX;

        public void arrow(Archery window, int getArcherX, int getArcherY, Boolean getArrow, Panel all1)
        {
            ReadXml xml = new ReadXml();
            xml.readFromXml(Archery.xmlFile);

            string[] value = Archery.value;

            speed = Int32.Parse(value[14]);
            arrowHeight = Int32.Parse(value[12]);
            arrowWidth = Int32.Parse(value[13]);        
            arrowNewX = 100;

            all = all1;
            chooseArrow = getArrow;
            objectArrow.Height = arrowHeight;
            objectArrow.Width = arrowWidth;
            if (chooseArrow == true)
                objectArrow.Image = Properties.Resources.Arrow;
            else
                objectArrow.Image = Properties.Resources.chararrow;
            objectArrow.SizeMode = PictureBoxSizeMode.StretchImage;
            all.Controls.Add(objectArrow);
            arrowX = getArcherX;
            arrowY = getArcherY;
            if (chooseArrow == true)
                objectArrow.Location = new Point(arrowX + 40, arrowY + 40);
            else
                objectArrow.Location = new Point(arrowX + 40, arrowY + 25);
        }

        public void arrowMove(Archery window)
        {
            sendWindow = window;
            Timer shoot = new Timer();
            shoot.Tick += shootArrow;
            shoot.Start();
        }

        private void shootArrow(object sender, EventArgs e)
        {
            if (objectArrow.Location.X < sendWindow.Width - arrowNewX)
            {
                if (chooseArrow == true)
                    objectArrow.Location = new Point(objectArrow.Location.X + speed, arrowY + 40);
                else
                    objectArrow.Location = new Point(objectArrow.Location.X + speed, arrowY + 25);
                sendWindow.checkCollision(objectArrow);
            }
            else
            {         
                all.Controls.Remove(objectArrow);
                objectArrow.Dispose();                
            }

        }
    }
}