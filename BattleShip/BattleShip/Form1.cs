using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {

        
        public BattleContainer bt;
        public Panel p = new Panel();
        public Button exitGame = new Button();
        public Button newGame = new Button();
        public Button continueGame = new Button();
        private bool firstStart = true;
        private bool keyPressed = false;
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p.Height = this.Height;
            p.Width = this.Width;
            Controls.Add(p);
            p.BackgroundImage = Properties.Resources.backgroundImg;
            p.BackgroundImageLayout = ImageLayout.Stretch;
            newGame.Location = new System.Drawing.Point(300, 200);
            newGame.Size = new System.Drawing.Size(300, 50);
            newGame.Font = new Font("Arial", 10, FontStyle.Italic);
            newGame.Text = "New Game";
            newGame.Click += newGame_click;
            p.Controls.Add(newGame);
            exitGame.Location = new System.Drawing.Point(300, 260);
            exitGame.Size = new System.Drawing.Size(300, 50);
            exitGame.Font = new Font("Arial", 10, FontStyle.Italic);
            exitGame.Text = "Exit Game";
            exitGame.Click += exitGame_click;
            p.Controls.Add(exitGame);
            bt = new BattleContainer(this.Height,this.Width);
            bt.BackColor = Color.Transparent;
            this.KeyPreview = true;


        }
        private void newGame_click(object sender, EventArgs e)
        {
            if (firstStart)
            {
                firstStart = false;
                p.Hide();
                continueGame.Location = new System.Drawing.Point(300, 140);
                continueGame.Size = new System.Drawing.Size(300, 50);
                continueGame.Font = new Font("Arial", 10, FontStyle.Italic);
                continueGame.Text = "Continue Game";
                continueGame.Click += continueGame_click;
                p.Controls.Add(continueGame);
            }
            else
            {
                p.Hide();
                bt.battleBoard.Dispose();
                bt.startSetup();
            }
        }
        private void continueGame_click(object sender, EventArgs e)
        {
            keyPressed = false;
            p.Hide();
        }
        private void exitGame_click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            bt.startSetup();
           
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Controls.Add(bt);


        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(bt);



        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!firstStart)
            {

                if (e.KeyCode == Keys.Escape)
                    if (!keyPressed)
                    {
                        p.Show();
                        keyPressed = true;
                    }
                    else
                    {
                        p.Hide();
                        keyPressed = false;
                    }
            }
        }
    }
}
