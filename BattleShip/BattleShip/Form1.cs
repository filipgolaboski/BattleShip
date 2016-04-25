using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        public setUpTwoPlayerBoard mainBoard = new setUpTwoPlayerBoard(3);
        
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            mainBoard.Location = new System.Drawing.Point(0, 0);
           mainBoard.Height = this.Height;
            mainBoard.Width = this.Width;
           this.Controls.Add(mainBoard);
        }
        


    }
}
