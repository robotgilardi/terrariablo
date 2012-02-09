using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Terrariablo
{
    class Spritesheet : Sprite
    {

        int m_currentTime = 0;
        int m_FPS;
        int m_frames;
        int m_rows;
        int m_columns;
        int m_frame = 0;
        int m_row = 0;
        int m_col = 0;
        public bool m_cycle = true;
        public Rectangle m_sourceRect = new Rectangle();
        public Rectangle m_destinationRectangle = new Rectangle();
        int m_startFrame = 4;
        int m_endFrame = 0;
        public int m_frameWidth;
        public int m_frameHeight;
        public bool m_active;
        public bool m_looping = true;
        public bool m_useFrameblend = false;
        // Set this to have specific frames for:
        State m_state = State.Idle;


        // Should these be s_vars?
        public String FORWARD = "Forward";
        public String BACKWARD = "Backward";
        public String RUNNING = "Running";
        public String JUMPING = "Jumping";
        public String CROUCHING = "Crouching";
        public String IDLE = "Idle";

        enum State
        {
            Idle,
            Forwards,
            Backwards,
            Running,
            Crouching,
            Jumping
        }
        public void SetState(String state)
        {
            // Switch doesn't work for some reason
            if (state == BACKWARD)
	        {
		        m_state = State.Backwards;
	        }
            else if (state == FORWARD)
	        {
		        m_state = State.Forwards;
	        }
            else if (state == RUNNING)
	        {
		        m_state = State.Running;
	        }
            else if (state == JUMPING)
	        {
		        m_state = State.Jumping;
	        }
            else if (state == CROUCHING)
            {
                m_state = State.Crouching;
            }
            else
            {
                m_state = State.Idle;
            }

        }
        public void UpdateFrameRanges()
        {
            switch (m_state)
            {
                case State.Backwards:
                    setRange(0, 0);
                    break;
                case State.Forwards:
                    setRange(3, 3);
                    break;
                case State.Jumping:
                    setRange(6, 6);
                    break;
                case State.Idle:
                    setRange(9, 9);
                    break;
                case State.Running:
                    setRange(11, 11);
                    break;
                case State.Crouching:
                    setRange(14, 14);
                    break;
                default:
                    setRange(0, m_frames);
                    break;
            }
        }
        public void SetRunning()
        {
            setRange(0, 1);
        }
        public void SetIdle()
        {
            setRange(2, 3);
        }
        public void SetForward()
        {
            setRange(6, 7);
        }
        public void SetBackwards()
        {
            setRange(5, 9);
        }
        public void SetJumping()
        {
            setRange(10, 10);
        }


        public void setRange(int start, int end)
        {
            m_startFrame = start;
            m_endFrame = end;
        }
        public void Initialize(ContentManager content, String texture,Vector2 position, int frameWidth, int frameHeight, int rows, int columns, int framerate = 24, int row = 0, int col = 0, float scale = 1, bool looping = true)
        {
            m_texture = content.Load<Texture2D>(texture);

            m_frameWidth = frameWidth;
            m_frameHeight = frameHeight;
            m_frames = columns * rows;
            m_FPS = framerate;
            m_scale = scale;
            m_rows = rows;
            m_columns = columns;
            m_row = row;
            m_col = col;
            m_looping = looping;
            m_position = position;
            m_startFrame = 0;
            m_endFrame = m_frames;
            m_frame = m_startFrame;
            float var1 = (m_texture.Width & 2);
            float var2 = (m_texture.Height & 2);

            m_active = (((m_texture.Width & 2) == 0 && (m_texture.Height & 2) == 0)) ? true : false;


            m_sourceRect = new Rectangle(
                m_frameWidth * m_col,
                m_frameHeight * m_row,
                m_frameWidth,
                m_frameHeight);
            m_destinationRectangle =
                new Rectangle(
                    (int)m_position.X - (int)(m_frameWidth * m_scale) / 2,
                    (int)m_position.Y - (int)(m_frameHeight * m_scale) / 2,
                    (int)(m_frameWidth * m_scale),
                    (int)(m_frameHeight * m_scale));

        }
        public void SetPosition(Vector2 pos)
        {
            m_position = pos;
        }
        public new void Draw(SpriteBatch spriteBatch)
        {
            if (m_active)
                spriteBatch.Draw(m_texture, m_destinationRectangle, m_sourceRect,Color.White);
        }
        public void SetMatrixPosition(int frame)
        {
            m_row = 0;
            m_col = 0;
            for (int i = 0; i < frame; i++)
            {
                m_col++;
                if (m_col==m_columns)
                {
                    m_col = 0;
                    m_row++;
                    if (m_row == m_rows)
                    {
                        m_row = 0;
                    }
                }
            }
        }

        public void Next(GameTime gameTime)
        {
            if (m_active == false)
                return;

            m_currentTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (m_currentTime > m_FPS)
            {
                m_frame++;
                if (m_frame > m_endFrame)
                {
                    m_frame = m_startFrame;
                    SetMatrixPosition(m_startFrame);
                    if (m_looping == false)
                        m_active = false;
                }
                else
                {
                    m_col++;
                    if (m_col >= m_columns)
                    {
                        m_col = 0;
                        m_row++;
                    }
                    if (m_row >= m_rows)
                    {
                        m_row = 0;
                    }
                }
                m_currentTime = 0;
            }

            
            m_sourceRect = new Rectangle(
                m_frameWidth * m_col,
                m_frameHeight * m_row,
                m_frameWidth,
                m_frameHeight);
            
             m_destinationRectangle =
                new Rectangle(
                    (int)m_position.X - (int)(m_frameWidth * m_scale) / 2,
                    (int)m_position.Y - (int)(m_frameHeight * m_scale) / 2,
                    (int)(m_frameWidth * m_scale),
                    (int)(m_frameHeight * m_scale));
            //Doesn't work:
            //m_sourceRect.X = m_col;
            //m_sourceRect.Y = m_row;
            //m_destinationRectangle.X = (int)m_position.X - (int)(m_frameWidth * m_scale) / 2;
            //m_destinationRectangle.Y = (int)m_position.Y - (int)(m_frameHeight * m_scale) / 2;
        }
        public override void Update(GameTime gameTime, Entity entity)
        {
            Next(gameTime);
            UpdateFrameRanges();
            if (m_state == State.Jumping)
                Console.WriteLine(m_state);
            m_position = entity.m_position;
        }
    }
}
