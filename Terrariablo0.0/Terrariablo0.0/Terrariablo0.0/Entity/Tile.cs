using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Terrariablo0._0
{
    class Tile : Entity
    {
        //display
        public Sprite sprite;
        public Collision collision;
        public Tile()
        {
            sprite = new Sprite();
            collision = new Collision();
            Components.Add(sprite);
            Components.Add(collision);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public void Initialize(ContentManager content,String imagePath, Vector2 position)
        {
            this.Position = position;
            Texture2D playerTexture = content.Load<Texture2D>(imagePath);
            sprite.Initialize(playerTexture,Position);
        }
        public override void Update(GameTime gameTime)
        {
            foreach (Component component in Components)
            {
                component.Update(gameTime, this);
            }
        }
    }
}
