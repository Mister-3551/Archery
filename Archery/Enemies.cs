using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Archery
{
    class Enemies : Archery
    {
        private Archery sendWindow;

        int enemyHeight, enemyWidth, b_1x, b_2x, b_3x, b_4x, b_5x, b_1Speed, b_2Speed, b_3Speed, b_4Speed, b_5Speed, enemyY, enemyYSpecial;

        int less = 20;
        int randomX = 200;
        int randomY = 750;
        int enemyStartPosition = 500;

        Random rnd = new Random();

        public Enemies(Archery window)
        {
            sendWindow = window;
            enemyY = sendWindow.Height - 50;

            ReadXml xml = new ReadXml();
            xml.readFromXml(Archery.xmlFile);

            string[] value = Archery.value;

            enemyHeight = Int32.Parse(value[15]);
            enemyWidth = Int32.Parse(value[16]);
            b_1Speed = Int32.Parse(value[17]);
            b_2Speed = Int32.Parse(value[18]);
            b_3Speed = Int32.Parse(value[19]);
            b_4Speed = Int32.Parse(value[20]);
            b_4Speed = Int32.Parse(value[20]);
            b_5Speed = Int32.Parse(value[21]);
        }

        PictureBox b_1 = new PictureBox();
        PictureBox b_2 = new PictureBox();
        PictureBox b_3 = new PictureBox();
        PictureBox b_4 = new PictureBox();
        PictureBox b_5 = new PictureBox();

        public Timer spawn = new Timer();

        internal void makeEnemies(Archery window, Panel all1)
        {
            all = all1;
            enemyYSpecial = window.Height + 1000;

            b_1.Height = enemyHeight;
            b_1.Width = enemyWidth;
            b_1.Image = Properties.Resources.balloon_1;

            b_2.Height = enemyHeight;
            b_2.Width = enemyWidth;
            b_2.Image = Properties.Resources.balloon_2;

            b_3.Height = enemyHeight;
            b_3.Width = enemyWidth;
            b_3.Image = Properties.Resources.balloon_3;

            b_4.Height = enemyHeight;
            b_4.Width = enemyWidth;
            b_4.Image = Properties.Resources.Alien;
            b_4.SizeMode = PictureBoxSizeMode.StretchImage;

            b_5.Height = enemyHeight;
            b_5.Width = enemyWidth;
            b_5.Image = Properties.Resources.Add;
            b_5.SizeMode = PictureBoxSizeMode.StretchImage;

            spawn.Tick += timer_Tick;
            spawn.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (b_1.Top < less)
            {
                b_1.Top = enemyStartPosition;
                b_1x = rnd.Next(randomX, randomY);
                b_1.Location = new Point(b_1x, enemyY);
                all.Controls.Add(b_1);
                sendWindow.Enemies.Add(b_1);
            }
            if (b_2.Top < less)
            {
                b_2.Top = enemyStartPosition;
                b_2x = rnd.Next(randomX, randomY);
                b_2.Location = new Point(b_2x, enemyY);
                all.Controls.Add(b_2);
                sendWindow.Enemies.Add(b_2);
            }
            if (b_3.Top < less)
            {
                b_3.Top = enemyStartPosition;
                b_3x = rnd.Next(randomX, randomY);
                b_3.Location = new Point(b_3x, enemyY);
                all.Controls.Add(b_3);
                sendWindow.Enemies.Add(b_3);
            }

            if (b_4.Top < less)
            {
                b_4.Top = enemyStartPosition;
                b_4x = rnd.Next(randomX, randomY);
                b_4.Location = new Point(b_4x, enemyY);
                all.Controls.Add(b_4);
                sendWindow.Enemies.Add(b_4);
            }

            if (b_5.Top < less)
            {
                b_5.Top = enemyStartPosition;
                b_5x = rnd.Next(randomX, randomY);
                b_5.Location = new Point(b_5x, enemyYSpecial);
                all.Controls.Add(b_5);
                sendWindow.getArrows.Add(b_5);
            }

            else
            {
                b_1.Top -= b_1Speed;
                b_2.Top -= b_2Speed;
                b_3.Top -= b_3Speed;
                b_4.Top -= b_4Speed;
                b_5.Top -= b_5Speed;
            }

        }
    }
}