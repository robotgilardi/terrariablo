using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace Terrariablo
{
    class Player : CEntity
    {
        public bool m_alive;
        public int m_health;
        //public Movement m_movement;
        
        public Player()
        {
            m_alive = true;
            m_health = 100;
            //m_movement = new Movement();
            m_spritesheet = new Spritesheet();
            //m_components.Add(m_movement);
            m_components.Add(m_spritesheet);
        }
        public void Initialize(ContentManager content)
        {
            m_position.X = 250;
            m_position.Y = 250;
            m_width = 64/4;
            m_height = 64/4;
            m_spritesheet.Initialize(content,"images/SmileyWalk", m_position, 64, 64, 4, 4);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            m_spritesheet.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component component in m_components)
            {
                component.Update(gameTime, this);
            }
            m_collision.SetBounds(m_spritesheet.m_destinationRectangle);
        }
    }
}
