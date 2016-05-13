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

        
    
        public setUpTwoPlayerBoard setUp;
        public BattleBoard battleBoard;
        public Panel p = new Panel();
        public Button exitGame = new Button();
        public Button newGame = new Button();
        public Button continueGame = new Button();
        private bool firstStart = true;
        private bool keyPressed = false;
        private Label lbMenuTitle = new Label();
        private Button btnInfo = new Button();
        private bool gameInProgress = false;
        private AboutForm instructionsForm = new AboutForm();

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
            newGame.Font = new Font("Arial", 20, FontStyle.Italic | FontStyle.Bold);
            newGame.Text = "New Game";
            newGame.Click += newGame_click;
            p.Controls.Add(newGame);
            exitGame.Location = new System.Drawing.Point(300, 260);
            exitGame.Size = new System.Drawing.Size(300, 50);
            exitGame.Font = new Font("Arial", 10, FontStyle.Italic);
            exitGame.Text = "Exit Game";
            exitGame.Click += exitGame_click;
            p.Controls.Add(exitGame);
            startSetup();
            this.KeyPreview = true;
            p.BackColor = Color.Transparent;
            lbMenuTitle.Text = "B A T T L E S H I P S";
            lbMenuTitle.Location = new Point(0, 30);
            lbMenuTitle.Font = new Font("Comic Sans", 60, FontStyle.Bold | FontStyle.Italic);
            lbMenuTitle.ForeColor = Color.AntiqueWhite;
            lbMenuTitle.BackColor = Color.Transparent;
            lbMenuTitle.TextAlign = ContentAlignment.MiddleCenter;
            lbMenuTitle.Size = new Size(920, 200);
            p.Controls.Add(lbMenuTitle);

            btnInfo.Text = "How to play";
            btnInfo.Font = new Font("Arial", 10);
            btnInfo.Size = new Size(300, 50);
            btnInfo.Location = new Point(300, 320);
            btnInfo.Click += btnInfo_Click;
            p.Controls.Add(btnInfo);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            instructionsForm.ShowDialog();
        }

        private void newGame_click(object sender, EventArgs e)
        {
            if (firstStart)
            {
                firstStart = false;
                gameInProgress = true;
                p.Hide();
                exitGame.Hide();
                continueGame.Location = new Point(300, 260);
                continueGame.Size = new Size(300, 50);
                continueGame.Font = new Font("Arial", 16, FontStyle.Italic);
                continueGame.Text = "Continue Game";
                continueGame.Click += continueGame_click;
                p.Controls.Add(continueGame);
            }
            else
            {
                if (gameInProgress && battleBoard != null)
                {
                    battleBoard.newGame.PerformClick();
                    p.Hide();
                    keyPressed = false;
                }
                else
                {

                    keyPressed = false;
                    if (setUp != null)
                    {
                        setUp.Dispose();
                    }
                    startSetup();
                    p.Hide();
                }
            }
        }
        private void continueGame_click(object sender, EventArgs e)
        {
            keyPressed = false;
            p.Hide();
        }
        private void exitGame_click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to exit?", "Exit game",
                MessageBoxButtons.YesNo);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            

         if (e.KeyCode == Keys.Escape)
                if (!firstStart)
                {
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
        public void startGame()
        {

            battleBoard = new BattleBoard(setUp.playerBoard, setUp.opponentBoard, setUp.playerBoard.getBoatList(), setUp.opponentBoard.getListOfBoats());
            battleBoard.Location = new Point(0, 0);
            battleBoard.Height = this.Height;
            battleBoard.Width = this.Width;
            battleBoard.newGame.Click += newGameB_click;
            battleBoard.BackColor = Color.Transparent;
            this.Controls.Add(battleBoard);
            if (setUp != null)
            {
                setUp.Dispose();
            }

        }
        public void startSetup()
        { 
            setUp = new setUpTwoPlayerBoard();
            setUp.Location = new Point(0, 0);
            setUp.Height = this.Height;
            setUp.Width = this.Width;
            setUp.startGame.Click += startGame_click;
            setUp.BackColor = Color.Transparent;
            this.Controls.Add(setUp);
            if (battleBoard != null)
            {
                battleBoard.Dispose();
            }
        }

        private void startGame_click(object sender, EventArgs e)
        {
            if (setUp.gameReady())
            {
                startGame();
                
            }
            else
                MessageBox.Show("You can't start the battle without your whole fleet in position", "Captain, please place all of your ships",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

        }
        private void newGameB_click(object sender, EventArgs e)
        {
            if (!battleBoard.victoryCondition)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to start a new game? (You will not be able to continue the current game later)", "Start new game",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    startSetup();
                }
            }
            else
            {

                startSetup();
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
