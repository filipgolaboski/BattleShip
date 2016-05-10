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
    {  //prilichno ednostavna klasa sodrzhi picture box i osnovni promenlivi kako dali e kliknata dali si vrz nejze dali e brod i sl
        public int i, j;
        public bool boatHere = false;
        public bool isHighLighted = false;
        public bool clicked = false;
        public Image img = null;
      
        public Tile( int i, int j)
        {
            
            Height = 40;
            Width = 40;
            this.i = i;
            this.j = j;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }
        public void setImage(Bitmap img)
        {
            this.Image = img;
            this.img = img;
        }
        public virtual void click() {
          

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        public void setOpBoat(Image img)
        {
            boatHere = true;
            this.img = img;
        }
        public void setBoat()
        {
            boatHere = true;
            this.Image = null;
            Enabled = false;
            isHighLighted = false;
            
            
        }
        public void highLight()
        {
            if (!boatHere)
            {
                isHighLighted = true;
                this.Image = null;
                BackColor = Color.Yellow;
            }
        }
        public void unhighLight()
        {
            if (!boatHere)
            {
                isHighLighted = false;
                this.Image = img;
            }

        }
      


    }
}
