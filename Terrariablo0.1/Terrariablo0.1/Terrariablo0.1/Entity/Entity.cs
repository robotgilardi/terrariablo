﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terrariablo
{
    class Entity
    {
        public Vector2 m_position;
        public int m_width = new int();
        public int m_height = new int();
        protected ArrayList m_components = new ArrayList();
        public Entity()
        {
            m_position = new Vector2();
        }
        public void AddComponent(Component component)
        {
            m_components.Add(component);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (Component component in m_components)
            {
                component.Update(gameTime, this);
            }
        }
    }
}