using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class OpponentShipTile : Tile
    {
        public new int i, j;
        public OpponentShipTile(int i,int j) : base(i, j)
        {
            this.i = i;
            this.j = j;
            clicked = false;
            BackColor = Color.Blue;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            clicked = true;
            Enabled = false;
            BackColor = Color.HotPink;
            base.OnMouseClick(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!clicked)
            {
                BackColor = Color.Yellow;
            }
            base.OnMouseEnter(e);

        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!clicked)
            {
                BackColor = Color.Blue;
            }
            base.OnMouseLeave(e);
        }
    }
}
