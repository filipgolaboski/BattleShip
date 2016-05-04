using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleShips
{
    public class GFX
    {
        private Graphics gObject;

        public GFX(Graphics g)
        {
            gObject = g;
            setUpSea();
        }

        public void setUpSea()
        {
            Brush br = new SolidBrush(Color.RoyalBlue);
            Pen pn = new Pen(Color.Black, 2);

            gObject.FillRectangle(br, new Rectangle(0, 0, 400, 400));   // Izboj go celoto pole/more

            gObject.DrawLine(pn, new Point(0, 0), new Point(0, 400));       //
            gObject.DrawLine(pn, new Point(80, 0), new Point(80, 400));     //
            gObject.DrawLine(pn, new Point(160, 0), new Point(160, 400));   //
            gObject.DrawLine(pn, new Point(240, 0), new Point(240, 400));   //  Vertikalnite linii
            gObject.DrawLine(pn, new Point(320, 0), new Point(320, 400));   //
            gObject.DrawLine(pn, new Point(400, 0), new Point(400, 400));   //  

            gObject.DrawLine(pn, new Point(0, 0), new Point(400, 0));       //
            gObject.DrawLine(pn, new Point(0, 80), new Point(400, 80));     //
            gObject.DrawLine(pn, new Point(0, 160), new Point(400, 160));   //
            gObject.DrawLine(pn, new Point(0, 240), new Point(400, 240));   //  Horizontalnite linii
            gObject.DrawLine(pn, new Point(0, 320), new Point(400, 320));   //
            gObject.DrawLine(pn, new Point(0, 400), new Point(400, 400));   //

            br.Dispose();
            pn.Dispose();
        }
    }
}
