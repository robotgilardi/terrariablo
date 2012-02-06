using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terrariablo0._0
{
    class Entity
    {
        public Vector2 Position;
        public int Width = new int();
        public int Height = new int();
        protected ArrayList Components = new ArrayList();
        public Entity()
        {
            Position = new Vector2();
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (Component component in Components)
            {
                component.Update(gameTime, this);
            }
        }
    }
}
