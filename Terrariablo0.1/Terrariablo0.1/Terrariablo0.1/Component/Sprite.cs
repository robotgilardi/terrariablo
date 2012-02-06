using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

//gittest2
namespace Terrariablo
{
    class Sprite : Component
    {
        public Texture2D m_texture;
        public Vector2 m_position;
        public float m_scale = 1;
        public Color m_color;

        public Rectangle m_displayRectangle = new Rectangle();

        public Sprite()
        {
            m_color = Color.White;
        }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            m_texture = texture;
            m_position = position;
            setRectangle();
        }
        public void Initialize(ContentManager content, String texturePath, Vector2 position, Color color, float scale = 1)
        {
            m_texture = content.Load<Texture2D>(texturePath);
            m_position = position;
            m_scale = scale;
            m_color = color;
            setRectangle();
        }
        protected void setRectangle()
        {
            m_displayRectangle = new Rectangle();
            m_displayRectangle.Width = (int)((m_texture.Width) * m_scale);
            m_displayRectangle.Height = (int)((m_texture.Height) * m_scale);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_position, m_displayRectangle, m_color);

        }
        public override void Update(GameTime gameTime, Entity entity)
        {
            m_position = entity.m_position;
        }
    }
}
