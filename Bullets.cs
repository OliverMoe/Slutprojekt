using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Slutprojekt
{
    public class Bullets
    {
        private Rectangle bullet;
        public Bullets(int x){
            bullet = new Rectangle(x+10, 420, 10, 10);
        }
        public Rectangle Bullet{
            get { return bullet; }
        }
        public void MoveUp(){
            bullet.Y -= 10;
        }
        public void Shoot(int x){
            bullet.Y = 420;
            bullet.X = x+10;
        }
    }
}