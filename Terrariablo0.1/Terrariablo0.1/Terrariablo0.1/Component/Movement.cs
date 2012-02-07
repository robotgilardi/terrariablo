using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Terrariablo
{
    /// <summary>
    ///  Temp movment functions
    /// </summary>
    class Movement : Component
    {
        enum State{
            Walking,
            Running,
            Jumping

        }
        State m_currentState = State.Walking;

        KeyboardState m_currentKeyboardState;
        public int m_speed = 8;
        public Rectangle m_bounds;
        private ArrayList m_entityList;
        private CEntity m_ent;
        public Movement(ArrayList ents,GraphicsDeviceManager gfxManager)
        {
            m_entityList = ents;
            m_bounds = new Rectangle(0, 0, gfxManager.PreferredBackBufferWidth, gfxManager.PreferredBackBufferHeight);
            m_currentKeyboardState = new KeyboardState();
        }
        public void Initialize(CEntity ent)
        {
            m_ent = ent;
        }
        public void SetBounds(Rectangle rectangle)
        {
            m_bounds = rectangle;
        }
        public override void Update(GameTime gameTime, Entity entity)
        {
            m_currentKeyboardState = Keyboard.GetState();
            UpdateMovement(m_currentKeyboardState);
            UpdateJumping(gameTime, m_currentKeyboardState);
            UpdateGravity();

            
            entity.m_position.X = (int)(MathHelper.Clamp((float)entity.m_position.X, 0f, (float)m_bounds.Width - (float)entity.m_width));
            entity.m_position.Y = (int)(MathHelper.Clamp((float)entity.m_position.Y, 0f, (float)m_bounds.Height - (float)entity.m_height));
        }
        public void UpdateMovement(KeyboardState keyboardState)
        {
            if (m_currentKeyboardState.IsKeyDown(Keys.Left) || m_currentKeyboardState.IsKeyDown(Keys.A))
            {
                m_ent.GetComponent<Spritesheet>().setBackwards();
                Collides(-m_speed, 0);
            }
            if (m_currentKeyboardState.IsKeyDown(Keys.Right) || m_currentKeyboardState.IsKeyDown(Keys.D))
            {
                m_ent.GetComponent<Spritesheet>().setForward();
                Collides(m_speed, 0);
            }
            if (m_currentKeyboardState.IsKeyDown(Keys.Up) || m_currentKeyboardState.IsKeyDown(Keys.W))
                Collides(0, -m_speed);
            if (m_currentKeyboardState.IsKeyDown(Keys.Down) || m_currentKeyboardState.IsKeyDown(Keys.S))
                Collides(0, m_speed);
        }
        int m_jumpheight = 0;
        int m_maxJumpHeight = 15;
        int m_currentTime = 0;
        int m_jumpForce = 10;
        public void UpdateJumping(GameTime gameTime, KeyboardState keyboardState)
        {
            if (m_currentKeyboardState.IsKeyDown(Keys.Space) && m_currentState != State.Jumping)
            {
                m_currentState = State.Jumping;
            }
            if (m_currentState == State.Jumping)
            {
                if (m_jumpheight < m_maxJumpHeight)
                {
                    m_currentTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (m_currentTime > 30)
                    {
                        m_jumpheight++;
                        m_ent.m_position.Y -= m_jumpForce;
                        m_currentTime = 0;
                    }
                }
                else
                {
                    m_jumpheight = 0;
                    m_currentState = State.Walking;
                }
            }
        }
        int m_gravityStrength = 2;
        public void UpdateGravity()
        {
            
            Collides(0,m_gravityStrength);
        }
        private void Collides(float xOffset, float yOffset)
        {
            /*
            float oldX = m_ent.m_position.X;
            float oldY = m_ent.m_position.Y;

            m_ent.m_position.X += xOffset;
            m_ent.m_position.Y += yOffset;
            */
            bool move = true;

            foreach (CEntity ent in m_entityList)
            {

                if (ent != m_ent)
                {
                    Rectangle box1 = new Rectangle(
                        (int)(m_ent.m_position.X+xOffset),
                        (int)(m_ent.m_position.Y+yOffset),
                        m_ent.m_width,
                        m_ent.m_height);
                    Rectangle box2 = ent.m_collision.GetBounds();

                    if (ent.m_collision.Intersects(box1))
                    {
                        move = false;
                        break;
                    }
                }
            }
            if (move)
            {
                m_ent.m_position.X += xOffset;
                m_ent.m_position.Y += yOffset;

            }
        }
    }
}
