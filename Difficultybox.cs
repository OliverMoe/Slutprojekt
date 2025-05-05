using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Slutprojekt
{
    public class DifficultyBox
    {
        Rectangle box;
        public DifficultyBox(int y){
            box = new Rectangle(705, y, 40, 25);
        }
        public Rectangle Box{
            get { return box; }
        }
    }
}