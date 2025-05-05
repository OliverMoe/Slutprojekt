using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Slutprojekt
{
    public class Lives
    {
        private Rectangle lives;
        public Lives(int x){
            lives = new Rectangle(x,20,10,10);
        }
        public Rectangle Life{
            get { return lives; }
        }
    }
}