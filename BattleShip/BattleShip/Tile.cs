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
        public bool opBoatHere = false;
        public bool clicked = false;
        public bool isHighLighted = false;
        public bool currentOver = false;
       
        public Tile( int i, int j)
        {
            
            Height = 40;
            Width = 40;
            this.i = i;
            this.j = j;
            BackColor = Color.Blue;
        }
        public void setOpBoat()
        {
            opBoatHere = true;
        }
        public void setBoat()
        {
            boatHere = true;
            BackColor = Color.Green;
            Enabled = false;
            isHighLighted = false;
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
