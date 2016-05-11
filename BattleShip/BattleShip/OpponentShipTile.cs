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
        private Image imgs = null;
        public OpponentShipTile(int i,int j) : base(i, j)
        {
            this.i = i;
            this.j = j;
            clicked = false;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            clicked = true;
            Enabled = false;
            
            Pen redPen = new Pen(Color.Red, 13);
            Bitmap img = new Bitmap(this.img);
            this.Image = null;
            using (var grph = Graphics.FromImage(img))
            {

                grph.DrawLine(redPen, 0, 0, img.Width, img.Height);
                grph.DrawLine(redPen, img.Width, 0, 0, img.Height);
            }
            this.Image = img;
            base.OnMouseClick(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!clicked)
            {
                this.imgs = this.Image;
                Pen greenPen = new Pen(Color.LightGreen, 13);
                Bitmap img = new Bitmap(this.Image);
                this.Image = null;
                using (var grph = Graphics.FromImage(img))
                {

                    grph.DrawLine(greenPen, 0, 0, img.Width, img.Height);
                    grph.DrawLine(greenPen, img.Width, 0, 0, img.Height);
                }
                this.Image = img;

                
            }
            base.OnMouseEnter(e);

        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!clicked)
            {
                this.Image = imgs;
            }
            base.OnMouseLeave(e);
        }
    }
}
