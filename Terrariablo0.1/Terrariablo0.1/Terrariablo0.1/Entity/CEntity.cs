using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terrariablo
{
    /// <summary>
    /// Collision entity
    /// </summary>
    class CEntity :Entity
    {
        public Collision m_collision;
        public Spritesheet m_spritesheet;
        public CEntity()
        {
            m_collision = new Collision();
            m_components.Add(m_collision);
        }
        public void AddMovement(Movement movement)
        {
            AddComponent(movement);
            movement.Initialize(this);
        }
    }
}
