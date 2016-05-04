using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShips
{
    public class SeaTile : PictureBox
    {
        public bool IsTaken { get; set; }
        public bool IsAttacked { get; set; }

        public SeaTile()
        {
            IsTaken = false;
            IsAttacked = false;
        }
    }
}
