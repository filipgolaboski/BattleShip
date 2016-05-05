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
            this.Controls.Add(battleBoard);
            
        }
        public void startSetup()
        {
            setUp = new setUpTwoPlayerBoard();
            setUp.Location = new System.Drawing.Point(0, 0);
            setUp.Height = this.Height;
            setUp.Width = this.Width;
            setUp.setBattleContainer(this);
            this.Controls.Add(setUp);
        }
    }
}
