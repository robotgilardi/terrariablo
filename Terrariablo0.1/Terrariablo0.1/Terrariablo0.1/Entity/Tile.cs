using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Terrariablo
{
    class Tile : CEntity
    {
        //display
        public Sprite m_sprite;
        public Tile()
        {

            m_sprite = new Sprite();
            m_components.Add(m_sprite);

        }
        public void Initialize(ContentManager content, String imagePath, Vector2 position)
        {
            m_position = position;
            Texture2D texture = content.Load<Texture2D>(imagePath);
            m_sprite.Initialize(texture, m_position);

            Rectangle bounds = new Rectangle((int)m_position.X, (int)m_position.Y, (int)texture.Bounds.Width, (int)texture.Bounds.Height);
            m_collision.SetBounds(bounds);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            m_sprite.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component component in m_components)
            {
                component.Update(gameTime, this);
            }
        }
    }
}
