using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;


namespace Terrariablo0._0
{
    class Sprite : Component
    {
        public Texture2D Texture;
        public Vector2 position;
        public float scale = 1;
        public Color color;
        Rectangle displayRectangle = new Rectangle();

        public Sprite()
        {
            this.color = Color.White;
        }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            this.position = position;
            setRectangle();
        }
        public void Initialize(ContentManager content, String texturePath, Vector2 position, Color color, float scale = 1)
        {
            Texture = content.Load<Texture2D>(texturePath);
            this.position = position;
            this.scale = scale;
            this.color = color;
            setRectangle();
        }
        protected void setRectangle()
        {
            this.displayRectangle = new Rectangle();
            this.displayRectangle.Width = (int)((Texture.Width) * scale);
            this.displayRectangle.Height = (int)((Texture.Height) * scale);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, displayRectangle, color);

        }
        public override void Update(GameTime gameTime, Entity entity)
        {
            this.position = entity.Position;
        }
    }
}
