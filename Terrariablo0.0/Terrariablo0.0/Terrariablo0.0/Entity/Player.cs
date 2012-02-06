using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace Terrariablo0._0
{
    class Player : Entity
    {
        public bool alive;
        public int health;
        public Spritesheet spritesheet;
        public Movement movement;
        public Collision collision;
        public void Initialize(ContentManager content)
        {
            Texture2D playerTexture = content.Load<Texture2D>("images/SmileyWalk");
            Position.X = 0f + (float)(64 / 2);
            Position.Y = 0f + (float)(64 / 2);

            spritesheet.Initialize(playerTexture,Position,64,64,4,4);
        }

        public Player()
        {
            alive = true;
            health = 100;
            collision = new Collision();
            movement = new Movement();
            spritesheet = new Spritesheet();
            Components.Add(movement);
            Components.Add(spritesheet);
            Components.Add(collision);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spritesheet.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component component in Components)
            {
                component.Update(gameTime, this);
            }
            spritesheet.SetPosition(Position);
            spritesheet.Next(gameTime);
        }
    }
}
