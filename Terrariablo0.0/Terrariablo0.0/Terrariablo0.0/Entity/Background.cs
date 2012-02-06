using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Terrariablo0._0
{
    class Background : Entity
    {
        public Sprite sprite;

        public void Initialize(ContentManager content, String texture, Vector2 position)
        {
            Texture2D t = content.Load<Texture2D>(texture);
            sprite = new Sprite();
            this.Position = position;
            sprite.Initialize(t, position);
            Components.Add(sprite);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
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
