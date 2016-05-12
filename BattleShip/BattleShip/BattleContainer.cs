using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class BattleContainer : Panel
    {
        public setUpTwoPlayerBoard setUp;
        public BattleBoard battleBoard;
        public BattleContainer(int h,int w)
        {
            Height = h;
            Width = w;
            
           
        }
        public void startGame()
        {
           
            battleBoard = new BattleBoard(setUp.playerBoard, setUp.opponentBoard, setUp.playerBoard.getBoatList(), setUp.opponentBoard.getListOfBoats());
            battleBoard.Location = new System.Drawing.Point(0,0);
            battleBoard.Height = this.Height;
            battleBoard.Width = this.Width;
            battleBoard.newGame.Click += newGame_click;
            this.Controls.Add(battleBoard);
            if (setUp != null)
            {
                setUp.Dispose();
            }
            
        }
        public void startSetup()
        {
              
            setUp = new setUpTwoPlayerBoard();
            setUp.Location = new System.Drawing.Point(0, 0);
            setUp.Height = this.Height;
            setUp.Width = this.Width;
            setUp.startGame.Click += startGame_click;
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
                setUp.Dispose();
            }
            else
                MessageBox.Show("You can't start the battle without your whole fleet in position", "Captain, please place all of your ships",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

        }
        private void newGame_click(object sender, EventArgs e)
        {
            if (!battleBoard.victoryCondition)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to start a new game? (You will not be able to continue the current game later)", "Start new game",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    battleBoard.Hide();
                    startSetup();
                }
            }
            else
            {
                battleBoard.Hide();
                startSetup();
            }
        }
    }
}
