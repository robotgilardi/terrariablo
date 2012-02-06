using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Terrariablo0._0
{
    /// <summary>
    /// Change this to more complex collision if needed
    /// Needs to update
    /// </summary>
    class Collision : Component
    {
        public Rectangle boundingbox = new Rectangle();
        public Collision()
        {

        }
        public void Initialize(Rectangle rectangle)
        {
            boundingbox = rectangle;
        }
        public override void Update(GameTime gameTime, Entity entity)
        {
            
        }
        public bool Collides(Collision object2)
        {
            if (boundingbox.Intersects(object2.boundingbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
