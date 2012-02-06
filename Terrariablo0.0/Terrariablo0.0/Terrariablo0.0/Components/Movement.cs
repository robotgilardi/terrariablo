using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Terrariablo0._0
{
    /// <summary>
    ///  
    /// </summary>
    class Movement :Component
    {
        private Rectangle bounds = new Rectangle();
        KeyboardState currentKeyboardState;
        public int speed = 8;
        public Movement()
        {
            currentKeyboardState = new KeyboardState();
        }
        public void setBounds(GraphicsDevice gfxDevice)
        {
            bounds.Width = gfxDevice.Viewport.Width;
            bounds.Height = gfxDevice.Viewport.Height;
        }
        public override void Update(GameTime gameTime,Entity entity)
        {
            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
                entity.Position.X -= speed;
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                entity.Position.X += speed;
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
                entity.Position.Y -= speed;
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.D))
                entity.Position.Y += speed;

            entity.Position.X = (int)(MathHelper.Clamp((float)entity.Position.X, 0f, (float)bounds.Width - (float)entity.Width));
            entity.Position.Y = (int)(MathHelper.Clamp((float)entity.Position.Y, 0f, (float)bounds.Height - (float)entity.Height));
        }
    }
}
