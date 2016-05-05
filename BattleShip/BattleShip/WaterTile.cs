using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class WaterTile : Tile
    {
        public new int i, j;
        public WaterTile(int i,int j) : base(i,j)
        {
            this.i = i;
            this.j = j;
            BackColor = Color.Blue;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            click();
            base.OnMouseClick(e);
        }
        public override void click()
        {
            clicked = true;
            Enabled = false;
            boatHere = false;
            BackColor = Color.Gray;
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
