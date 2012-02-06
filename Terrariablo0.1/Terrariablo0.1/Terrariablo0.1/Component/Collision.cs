using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terrariablo
{
    class Collision : Component
    {
        private Rectangle m_bounds;
        public Collision()
        {
        }
        public void SetBounds(Rectangle rectangle)
        {
            m_bounds = rectangle;
        }
        public Rectangle GetBounds()
        {
            return m_bounds;
        }
        public bool Intersects(Rectangle bounds)
        {
            return m_bounds.Intersects(bounds)?true:false;
        }
    }
}
