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
        private Image img=null;
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
            Pen redPen = new Pen(Color.Gray, 8);
            Bitmap img = new Bitmap(this.Image);
            this.Image = null;
            using (var grph = Graphics.FromImage(img))
            {

                grph.DrawLine(redPen, 0, 0, img.Width, img.Height);
                grph.DrawLine(redPen, img.Width, 0, 0, img.Height);
            }
            this.Image = img;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!clicked)
            {
                this.img = this.Image;
                Pen redPen = new Pen(Color.Green, 8);
                Bitmap img = new Bitmap(this.Image);
                this.Image = null;
                using (var grph = Graphics.FromImage(img))
                {

                    grph.DrawLine(redPen, 0, 0, img.Width, img.Height);
                    grph.DrawLine(redPen, img.Width, 0, 0, img.Height);
                }
                this.Image = img;
                
               // BackColor = Color.Yellow;
            }
            base.OnMouseEnter(e);

        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!clicked)
            {
               this.Image = img;
                //BackColor = Color.Blue;
            }
            base.OnMouseLeave(e);
        }
    }
}
