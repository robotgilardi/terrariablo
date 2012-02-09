using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Terrariablo
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        Player m_player;
        int m_screenHeight = 800;
        int m_screenWidth = 600;
        protected ArrayList m_entities = new ArrayList();
        Movement m_movement;

        protected ArrayList m_tiles = new ArrayList();
        protected String[] m_tileImages = new String[9];


        public Main()
        {
            //Game->Init->Load->Update->Draw  :Unload
            Content.RootDirectory = "Content";
            WindowProperties();
        }
        private void WindowProperties()
        {
            this.IsMouseVisible = true;
            this.Window.Title = "Terriablo";
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.PreferredBackBufferWidth = m_screenHeight;
            m_graphics.PreferredBackBufferHeight = m_screenWidth;
            m_graphics.IsFullScreen = false;
            m_graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            m_movement = new Movement(m_entities,m_graphics);
            m_player = new Player();
            m_player.AddMovement(m_movement); 

            m_entities.Add(m_player);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            m_player.Initialize(Content);
            LoadTiles(Content);
            //m_tile.Initialize(Content, "images/terraria/Item_170", Vector2.Zero);
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected void LoadTiles(ContentManager content)
        {
            // Basically this is draw tiles, can split up to tile logic later
            m_tileImages[0] = "images/terraria/Item_138";
            m_tileImages[1] = "images/terraria/Item_137";
            Vector2 tilePosition = new Vector2();

            String curentTileImage = m_tileImages[0];

            tilePosition.Y = 600 - 5 * 16;
            for (int i = 0; i < 5; i++)
            {
                //Whitepapers, GO!
                for (int j = 0; j < 50; j++)
                {
                    if (j == 0 || j == 49 || i == 0 || i ==4)
                    {
                        curentTileImage = m_tileImages[1];
                    }
                    else
                    {
                        curentTileImage = m_tileImages[0];

                    }
                    Tile t = new Tile();
                    t.Initialize(content, curentTileImage, tilePosition);
                    m_tiles.Add(t);
                    m_entities.Add(t);
                    tilePosition.X += 16;
                }
                tilePosition.Y += 16;
                tilePosition.X = 0;
            }
            tilePosition.X = 0;
            tilePosition.Y = 0;
        }
        protected void DrawTiles(ContentManager content)
        {

        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || (Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();
            foreach (Entity ent in m_entities)
            {
                ent.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_spriteBatch.Begin();
            foreach (Entity ent in m_entities)
            {
                ent.Draw(m_spriteBatch);
            }
            m_spriteBatch.End();
            base.Draw(gameTime);
        }
        public void UpdateCollision()
        {
            foreach (CEntity ent in m_entities)
            {
                if (ent != m_player)
                {
                        
                }
            }
        }
    }
}
