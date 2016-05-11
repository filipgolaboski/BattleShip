using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {

        
        public BattleContainer bt;
        public Panel p = new Panel();
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bt = new BattleContainer(this.Height,this.Width);
            bt.BackColor = Color.Transparent;
            
           

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            bt.startSetup();
           
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            this.Controls.Add(bt);
        
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(bt);



        }
    }
}
