using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
   public class Tile : PictureBox
    {
        private bool clicked = false;
        public Tile()
        {
            BackColor = Color.Blue;
            Height = 40;
            Width = 40;
        }
        protected override void OnClick(EventArgs e)
        {
            clicked = true;
            BackColor = Color.Red;
            base.OnClick(e);
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
        public void opponentClick()
        {
            BackColor = Color.Red;
            clicked = true;
        }

    }
}
