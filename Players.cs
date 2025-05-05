using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Slutprojekt
{
    public class Players
    {
        private Rectangle player;
        public Players(int x, int y, int b, int h){
            player = new Rectangle(x,y,b,h);
        }
        public Rectangle Player{
            get { return player; }
        }
        public void MoveLeft(){
            player.X -= 5;
        }
        public void MoveRight(){
            player.X += 5;
        }
        public void Reset(){
            player.X = 385;
        }
    }
}