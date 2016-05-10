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
        private Image img = null;
        public ShipTile(int i,int j) : base(i, j)
        {
            this.i = i;
            this.j = j;
            boatHere = true;
            clicked = false;
        }
        public override void click()
        {
            
            clicked = true;
            Enabled = false;
           Pen redPen = new Pen(Color.Red, 10);
            Bitmap img = new Bitmap(this.Image);
            this.Image = null;
            using(var grph = Graphics.FromImage(img))
            {
                
                grph.DrawLine(redPen, 0, 0, img.Width, img.Height);
                grph.DrawLine(redPen, img.Width, 0, 0, img.Height);
            }
            this.Image = img;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            click();
            base.OnMouseClick(e);
        }
       
    }
}
