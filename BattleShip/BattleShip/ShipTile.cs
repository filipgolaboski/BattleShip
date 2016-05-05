using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    class ShipTile : Tile
    {
        public new int i, j;
        public ShipTile(int i,int j) : base(i, j)
        {
            this.i = i;
            this.j = j;
            BackColor = Color.Green;
        }
        public override void click()
        {
            clicked = true;
            boatHere = true;
            Enabled = false;
            BackColor = Color.HotPink;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            click();
            base.OnMouseClick(e);
        }
    }
}
