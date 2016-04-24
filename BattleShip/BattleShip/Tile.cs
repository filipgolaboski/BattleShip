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
        private bool clicked = false;
        public bool isSetup = false;
        public bool isHighLighted = false;
        public bool currentOver = false;
       
        public Tile(bool isSetup, int i, int j)
        {
            
            Height = 40;
            Width = 40;
            this.i = i;
            this.j = j;
            this.isSetup = isSetup;
            BackColor = Color.Blue;
        }
        protected override void OnClick(EventArgs e)
        {
            clicked = true;
            if (isSetup)
            {
                boatHere = true;
                BackColor = Color.Green;
            }
            else {
                BackColor = Color.Gray;
            }
            base.OnClick(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            currentOver = true;
            highLight();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            currentOver = false;
            unhighLight();
            base.OnMouseLeave(e);
        }
        public void setBoat()
        {
            if (isSetup)
            {
                BackColor = Color.Green;
            }
            else
            {
                BackColor = Color.Gray;
            }
            
            clicked = true;
        }
        public void highLight()
        {
            if (!clicked)
            {
                isHighLighted = true;
                BackColor = Color.Yellow;
            }
        }
        public void unhighLight()
        {
            if (!clicked)
            {
                isHighLighted = false;
                BackColor = Color.Blue;
            }

        }


    }
}
