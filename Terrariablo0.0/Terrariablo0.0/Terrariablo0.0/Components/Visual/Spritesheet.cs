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
///
/*
 Add error checking for non uniform dimensions
*/
namespace Terrariablo0._0
{
    class Spritesheet : Sprite
    {
        Texture2D spritesheet;

        int currentTime = 0;
        int FPS;
        int Frames;
        int Rows;
        int Columns;
        int Frame = 0;
        int Row= 0;
        int Col = 0 ;

        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRectangle = new Rectangle();
        public int FrameWidth;
        public int FrameHeight;
        public bool Active;
        public bool Looping;

        public void Initialize(ContentManager content, String texture, Vector2 position, int frameWidth, int frameHeight, int rows, int columns, int framerate = 24, int row = 0, int col = 0, float scale = 1, bool looping = true)
        {
            Texture2D t = content.Load<Texture2D>(texture);
            Initialize(t, position, frameWidth, frameHeight, rows, columns, framerate, row, col, scale, looping);
        }
   
        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int rows, int columns, int framerate = 24, int row = 0, int col = 0, float scale = 1, bool looping = true)
        {
            this.Texture = texture;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.Frames = columns*rows;
            this.FPS = framerate;
            this.scale = scale;
            this.Rows = rows;
            this.Columns = columns;
            this.Row = row;
            this.Col= col;
            this.Looping = looping;
            this.position= position;
            spritesheet = texture;

            float var1 = (spritesheet.Width & 2);
            float var2 = (spritesheet.Height & 2);

            Active = (((spritesheet.Width & 2) == 0 && (spritesheet.Height & 2) == 0)) ?  true : false;
            
        }
        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }
        public new void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
                spriteBatch.Draw(spritesheet, destinationRectangle, sourceRect, color);
        }

        /// <summary>
        /// Constant Frame Animation, does not have sequence yet.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Next(GameTime gameTime)
        {
            if (Active == false)
                return;

            currentTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentTime > FPS)
            {
                Frame++;
                if (Frame== Frames)
                {
                    Frame= 0;
                    Row= 0;
                    Col= 0;
                    if (Looping == false)
                        Active = false;
                }
                else 
                {
                    if (Frame== 0)
                    {
                        Col = 0;
                        Row = 0;
                    }
                    else
                    {
                        Col++;
                        if (Col > Columns - 1)
                        {
                            Col = 0;
                            Row++;
                        }
                        if (Row > Rows)
                        {
                            Row = 0;
                        }
                    }
                }
                currentTime = 0;
            }
            
            sourceRect = new Rectangle(
                FrameWidth*Col,
                FrameHeight * Row,
                FrameWidth,
                FrameHeight);
            destinationRectangle = 
                new Rectangle(
                    (int)position.X-(int)(FrameWidth*scale)/2,
                    (int)position.Y - (int)(FrameHeight * scale) / 2,
                    (int)(FrameWidth*scale),
                    (int)(FrameHeight*scale));

        }
    }
}
