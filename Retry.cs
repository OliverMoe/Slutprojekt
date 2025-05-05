using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Slutprojekt
{
    public class Retry
    {
        private Rectangle border;
        private Rectangle middle;
        public Retry(int x){
            border = new Rectangle(x, 300, 270, 150);
            middle = new Rectangle(x+10, 310, 250, 130);
        }
        public Rectangle Border{
            get { return border; }
        }
        public Rectangle Middle{
            get { return middle; }
        }
    }
}