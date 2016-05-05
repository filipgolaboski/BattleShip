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
       
        
        public BattleContainer bt;
       
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bt = new BattleContainer(this.Height,this.Width);
            bt.startSetup();
            this.Controls.Add(bt);
        }
        


    }
}
