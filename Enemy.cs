using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Slutprojekt
{
    public class Enemies
    {
        private Rectangle enemy;
        public Enemies(int y){
            enemy = new Rectangle(0,y,30,17);
        }
        public Rectangle Enemy{
            get { return enemy; }
        }
        public void MoveGreen(){
            enemy.X += 2;
        }
        public void MoveBlue(){
            enemy.X += 3;
        }
        public void MoveRed(){
            enemy.X +=5;
        }
    }
}