using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class mainTwoPlayerBoard : Panel
    {
        public Board playerBoard = new Board();
        public Board opponentBoard = new Board();
        public mainTwoPlayerBoard() {
            Height = 2 * 337 + 200;
            Width = 1024;
            opponentBoard.Location = new System.Drawing.Point(5, 5);
            this.Controls.Add(opponentBoard);
            playerBoard.Location =new System.Drawing.Point (5,365);
            this.Controls.Add(playerBoard);
            playerBoard.Enabled = false;
        }
    }
}
