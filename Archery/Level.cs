using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Archery
{
    class Level : Archery
    {
        private Archery sendWindow;

        public void game(Archery window, Panel all1)
        {
            all = all1;
            sendWindow = window;
            createEnemies();

        }
        private void createEnemies()
        {
            int i = 0;
            while (i < 1) { 
                for (int j = 0; j < 1; j++)
                {
                    Enemies enemies = new Enemies(sendWindow);
                    enemies.makeEnemies(sendWindow, all);
                }
                i++;
            }
        }
    }
}