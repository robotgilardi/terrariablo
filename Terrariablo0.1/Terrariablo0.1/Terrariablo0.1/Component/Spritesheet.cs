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
        int m_startFrame = 0;
        int m_endFrame = 0;
        public int m_frameWidth;
        public int m_frameHeight;
        public bool m_active;
        public bool m_looping = true;

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

            float var1 = (m_texture.Width & 2);
            float var2 = (m_texture.Height & 2);

            m_active = (((m_texture.Width & 2) == 0 && (m_texture.Height & 2) == 0)) ? true : false;

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

        public void Next(GameTime gameTime)
        {
            if (m_active == false)
                return;

            m_currentTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (m_currentTime > m_FPS)
            {
                m_frame++;
                if (m_frame == m_frames)
                {
                    m_frame = 0;
                    m_row = 0;
                    m_col = 0;
                    if (m_looping == false)
                        m_active = false;
                }
                else
                {
                    if (m_frame == 0)
                    {
                        m_col = 0;
                        m_row = 0;
                    }
                    else
                    {
                        m_col++;
                        if (m_col > m_columns - 1)
                        {
                            m_col = 0;
                            m_row++;
                        }
                        if (m_row > m_rows)
                        {
                            m_row = 0;
                        }
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
        }
        public override void Update(GameTime gameTime, Entity entity)
        {
            Next(gameTime);
            m_position = entity.m_position;
        }
    }
}
