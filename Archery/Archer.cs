using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Archery
{
    class Archer : Archery
    {
        private Archery sendWindow;

        public PictureBox objectArcher = new PictureBox();

        int windowHeight, setArcherX, setArcherY, speed, archerHeight, archerWidth;
        Boolean allow = true, choosePlayer, chooseArrow;

        int archerNewY1 = 20;
        int archerNewY2 = 150;

        public Archer(Archery window, Boolean getPlayer, Boolean getArrow, Panel getAll)
        {
            ReadXml xml = new ReadXml();
            xml.readFromXml(Archery.xmlFile);

            string[] value = Archery.value;

            speed = Int32.Parse(value[11]);           
            archerHeight = Int32.Parse(value[9]);
            archerWidth = Int32.Parse(value[10]);

            all = getAll;
            sendWindow = window;
            windowHeight = window.Height;

            chooseArrow = getArrow;
            choosePlayer = getPlayer;            
            objectArcher.Height = archerHeight;
            objectArcher.Width = archerWidth;

            if (choosePlayer == true)
                objectArcher.Image = Properties.Resources.Archer;
            else
                objectArcher.Image = Properties.Resources.char2;
            objectArcher.SizeMode = PictureBoxSizeMode.StretchImage;
            all.Controls.Add(objectArcher); 
            objectArcher.Location = new Point(50, 20);
        }

        public void archerMove(Archery window)
        {
            sendWindow = window;
            window.KeyDown += moveArcher;
        }

        public void moveArcher(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if(objectArcher.Location.Y > archerNewY1)
                        goUp();
                    break;
                case Keys.W:
                    if (objectArcher.Location.Y > archerNewY1)
                        goUp();
                    break;
                case Keys.Down:
                    if(objectArcher.Location.Y < windowHeight - archerNewY2)
                        goDown();
                    break;
                case Keys.S:
                    if (objectArcher.Location.Y < windowHeight - archerNewY2)
                        goDown();
                    break;
                case Keys.Space:
                    shootArrow();
                    break;
            }
        }

        private void goUp()
        {
            objectArcher.Location = new Point(objectArcher.Location.X, objectArcher.Location.Y - speed);
        }
        private void goDown()
        {
            objectArcher.Location = new Point(objectArcher.Location.X, objectArcher.Location.Y + speed);
        }
        private void shootArrow()
        {
            if (choosePlayer == true)
                objectArcher.Image = Properties.Resources.Archer2;
            else
                objectArcher.Image = Properties.Resources.char1;
            objectArcher.SizeMode = PictureBoxSizeMode.StretchImage;
            sendWindow.KeyUp += keyUp;
            allow = true;
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (choosePlayer == true)
                    objectArcher.Image = Properties.Resources.Archer1;
                else
                    objectArcher.Image = Properties.Resources.char1;
                objectArcher.SizeMode = PictureBoxSizeMode.StretchImage;
                if (allow == true)
                {
                    createArrow(sendWindow);
                    sendWindow.countArrows();
                }
                if (choosePlayer == true)
                    objectArcher.Image = Properties.Resources.Archer;
                else
                    objectArcher.Image = Properties.Resources.char2;
                objectArcher.SizeMode = PictureBoxSizeMode.StretchImage;
                allow = false;
            }
        }

        private void createArrow(Archery window)
        {
            setArcherX = objectArcher.Location.X;
            setArcherY = objectArcher.Location.Y;
            Arrow arrow = new Arrow();
            arrow.arrowMove(sendWindow);
            arrow.arrow(window, setArcherX, setArcherY, chooseArrow, all);
        }
    }
}