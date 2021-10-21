using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Archery
{
    public partial class Archery : Form
    {
        internal List<Control> Enemies { get; set; } = new List<Control>();
        internal List<Control> getArrows { get; set; } = new List<Control>();

        int score, arrow, duration, timeInterval, wHeight, wWidth, startImageHeight, StartImageWidth;

        Boolean makePlayer = true;
        Boolean choosePlayer = true;
        Boolean chooseArrow = true;
        Boolean checkGameOver = true;

        string showScoreText, playerNameText;

        public static string xmlFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archery", "Archery.xml");
        public static string xmlScores = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archery", "Scores.xml");
        string folderName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archery");

        Label playerArrow = new Label();
        Label playerScore = new Label();
        Label timeLabel = new Label();
        Label resulTxt = new Label();
        Label nameOfGame = new Label();
        Label playerNameLabel = new Label();

        TextBox showScore = new TextBox();
        TextBox playerName = new TextBox();

        PictureBox startImage = new PictureBox();
        PictureBox explosion = new PictureBox();

        Button startGame = new Button();
        Button chooseCharacter1 = new Button();
        Button chooseCharacter2 = new Button();

        Timer time = new Timer();

        internal Panel all = new Panel();

        string getIntValues;
        public static string[] value;

        public Archery()
        {
            InitializeComponent();

            Directory.CreateDirectory(Path.Combine(folderName));

            WriteXml xml = new WriteXml();
            xml.createXml(xmlFile);

            ReadXml rxml = new ReadXml();
            getIntValues = String.Join(",", rxml.readFromXml(xmlFile));
            value = getIntValues.Split(',');

            score = Int32.Parse(value[4]);
            arrow = Int32.Parse(value[5]);
            duration = Int32.Parse(value[6]);
            wHeight = Int32.Parse(value[0]);
            wWidth = Int32.Parse(value[1]);
            timeInterval = Int32.Parse(value[7]);
            showScoreText = value[8];

            startImageHeight = 300;
            StartImageWidth = 300;

            this.Icon = Properties.Resources.icon;
            this.Name = value[2];
            this.Text = value[3];
            this.Size = new Size(wWidth, wHeight);
            this.MaximumSize = new Size(wWidth, wHeight);
            this.MinimumSize = new Size(wWidth, wHeight);
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            firstWindow();

            all.Height = this.Height;
            all.Width = this.Width;

        }

        public void firstWindow()
        {
            startImage.Height = startImageHeight;
            startImage.Width = StartImageWidth;

            startImage.BackgroundImage = Properties.Resources.splashScreen;
            startImage.BackgroundImageLayout = ImageLayout.Stretch;
            startImage.Location = new Point(this.Width - startImage.Width - 50, this.Height / 2 - startImage.Height / 2);
            this.Controls.Add(startImage);

            nameOfGame.Height = 100;
            nameOfGame.Width = 300;
            nameOfGame.Text = "Archery";
            nameOfGame.Font = new Font("Arial", 55);
            nameOfGame.Location = new Point(this.Width / 2 - (nameOfGame.Width / 3) - 80, 40);
            this.Controls.Add(nameOfGame);

            startGame.Height = 60;
            startGame.Width = 170;
            startGame.Click += allowStartGame;
            startGame.Location = new Point(this.Width / 2 - (startGame.Width / 2) - 45, this.Height / 2 - (startGame.Height / 2) - 40);
            startGame.Text = "Start";
            startGame.Font = new Font("Arial", 30);
            startGame.TabStop = false;
            this.Controls.Add(startGame);

            showScore.Height = this.Height - 250;
            showScore.Width = 260;
            showScore.Font = new Font("Arial", 10);
            showScore.AcceptsReturn = true;
            showScore.ReadOnly = true;
            showScore.Multiline = true;
            showScore.ScrollBars = ScrollBars.Both;
            showScore.WordWrap = false;
            showScore.Location = new Point(0, this.Height / 2 - (showScore.Height / 2) + 15);
            this.Controls.Add(showScore);

            playerName.Height = 50;
            playerName.Width = 130;
            playerName.Text = "Player1";
            playerName.TabStop = false;
            playerName.Font = new Font("Arial", 15);
            playerName.Location = new Point(130, 90);
            playerName.ForeColor = Color.FromArgb(255, 0, 0);
            this.Controls.Add(playerName);

            playerNameLabel.Height = 50;
            playerNameLabel.Width = 130;
            playerNameLabel.Text = "Player Name: ";
            playerNameLabel.TabStop = false;
            playerNameLabel.Font = new Font("Arial", 15);
            playerNameLabel.Location = new Point(0, 90);
            playerNameLabel.ForeColor = Color.FromArgb(255, 0, 0);
            this.Controls.Add(playerNameLabel);

            chooseCharacter1.Height = 120;
            chooseCharacter1.Width = 120;
            chooseCharacter1.BackColor = Color.FromArgb(255, 0, 0);
            chooseCharacter1.Click += setCharacter1;
            chooseCharacter1.TabStop = true;
            chooseCharacter1.TabIndex = 1;
            chooseCharacter1.Image = Properties.Resources.Archer;
            chooseCharacter1.Location = new Point(this.Width / 2 - (chooseCharacter1.Width / 2) - 120, this.Height / 2 - (startGame.Height / 2) + 50);
            this.Controls.Add(chooseCharacter1);

            chooseCharacter2.Height = 120;
            chooseCharacter2.Width = 120;
            chooseCharacter2.BackColor = Color.FromArgb(0, 195, 0);
            chooseCharacter2.Click += setCharacter2;
            chooseCharacter2.TabStop = true;
            chooseCharacter2.TabIndex = 2;
            chooseCharacter2.Image = Properties.Resources.char1;
            chooseCharacter2.Location = new Point(this.Width / 2 - (chooseCharacter2.Width / 2) + 40, this.Height / 2 - (startGame.Height / 2) + 50);
            this.Controls.Add(chooseCharacter2);

            explosion.Height = 50;
            explosion.Width = 50;
            explosion.Image = Properties.Resources.Explosion;

            if (!File.Exists(xmlScores))
                showScore.Text = showScoreText;
            else
            {
                ReadXml xml = new ReadXml();

                var scores = String.Join("", xml.readXmlScores(xmlScores, playerNameText));

                if (scores == null)
                    showScore.Text = showScoreText;
                else
                    showScore.Text = scores.ToString();
            }
        }

        private void setCharacter1(object sender, EventArgs e)
        {
            choosePlayer = true;
            chooseArrow = true;
            chooseCharacter1.BackColor = Color.FromArgb(255, 0, 0);
            chooseCharacter2.BackColor = Color.FromArgb(0, 195, 0);
        }

        private void setCharacter2(object sender, EventArgs e)
        {
            choosePlayer = false;
            chooseArrow = false;
            chooseCharacter1.BackColor = Color.FromArgb(0, 195, 0);
            chooseCharacter2.BackColor = Color.FromArgb(255, 0, 0); ;
        }

        private void allowStartGame(object sender, EventArgs e)
        {
            this.Controls.Remove(startGame);
            this.Controls.Remove(startImage);
            this.Controls.Remove(nameOfGame);
            this.Controls.Remove(showScore);
            this.Controls.Remove(chooseCharacter1);
            this.Controls.Remove(chooseCharacter2);
            this.Controls.Remove(resulTxt);
            this.Controls.Remove(playerName);
            this.Controls.Remove(playerNameLabel);

            playerNameText = playerName.Text;

            playerScore.Text = "Score: " + score.ToString();
            playerScore.Width = 80;
            playerScore.Location = new Point(0, 0);
            all.Controls.Add(playerScore);

            playerArrow.Text = "Arrows Left: " + arrow.ToString();
            playerArrow.Width = 100;
            playerArrow.Location = new Point(80, 0);
            all.Controls.Add(playerArrow);

            timeLabel.Text = "Time Left: " + duration.ToString();
            timeLabel.Width = 100;
            timeLabel.Location = new Point(180, 0);
            all.Controls.Add(timeLabel);

            this.Controls.Add(all);

            time = new Timer();
            time.Tick += new EventHandler(countDown);
            time.Interval = timeInterval;
            time.Start();

            if (makePlayer == true)
            {
                this.BackColor = Color.FromArgb(0, 195, 0);
                Archer archer = new Archer(this, choosePlayer, chooseArrow, all);
                archer.archerMove(this);

                Level level = new Level();
                level.game(this, all);
                makePlayer = false;
            }
        }

        private void gameOver()
        {
            if (checkGameOver == true)
            {
                checkGameOver = false;

                SaveScoresXml xml = new SaveScoresXml();
                xml.saveScores(xmlScores, score, playerNameText);

                DialogResult dialogResult = MessageBox.Show("Are you ready to play again?", "Game Over", MessageBoxButtons.OK);
                if (dialogResult == DialogResult.OK)
                {
                    this.Hide();
                    Archery newGame = new Archery();
                    newGame.ShowDialog();
                    this.Close();
                    time.Stop();
                }
            }
        }

        private void countDown(object sender, EventArgs e)
        {
            if (duration == 0)
            {
                gameOver();
            }
            else if (duration > 0)
            {
                duration--;
                timeLabel.Text = "Time Left: " + duration.ToString();
            }
        }

        public void countArrows()
        {
            arrow--;
            playerArrow.Text = "Arrows Left: " + arrow.ToString();
        }

        Timer timer = new Timer();

        internal void checkCollision(PictureBox objArrow)
        {
            Random x = new Random();
            foreach (Control baloon in Enemies.ToArray())
            {
                if (baloon.Bounds.IntersectsWith(objArrow.Bounds))
                {
                    explosion.Location = new Point(objArrow.Location.X + 20, objArrow.Location.Y);
                    all.Controls.Add(explosion);
                    int x1 = x.Next(100, 300);
                    score += x1;
                    all.Controls.Remove(objArrow);             
                    all.Controls.Remove(baloon);
                    this.Enemies.Remove(baloon);
                    playerScore.Text = "Score: " + score.ToString();

                    timer.Tick += new EventHandler(countDownDelete);
                    timer.Interval = 200;
                    timer.Start();
                }
            }
            foreach (Control add in getArrows.ToArray())
            {
                if (add.Bounds.IntersectsWith(objArrow.Bounds))
                {
                    int x1 = x.Next(3, 10);
                    arrow += x1;
                    all.Controls.Remove(objArrow);
                    objArrow.Dispose();
                    all.Controls.Remove(add);
                    this.getArrows.Remove(add);
                    playerArrow.Text = "Arrows Left: " + arrow.ToString();                
                }
            }
            if (arrow == 0)
            {
                all.Controls.Remove(objArrow);
                gameOver();
                time.Stop();
            }
        }

        private void countDownDelete(object sender, EventArgs e)
        {
            all.Controls.Remove(explosion);
            timer.Stop();
        }
    }
}