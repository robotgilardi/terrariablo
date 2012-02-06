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

namespace Terrariablo0._0
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        // Game
        private float VERSION_NUMBER = 0.0f;
        private SpriteFont font;

        //Entities
        protected ArrayList Entities = new ArrayList();
        protected ArrayList CEntities = new ArrayList();

        // Player
        Player player;

        // Rendering
        SpriteBatch spriteBatch;

        // Screen properties
        GraphicsDeviceManager graphics;
        private int SCREEN_HEIGHT = 500;
        private int SCREEN_WIDTH = 500;
        
        // Tiles
        private int BLOCK_WIDTH = 16;
        private int BLOCK_HEIGHT = 16;
        public ArrayList tile_image_locations = new ArrayList();
        private ArrayList tiles = new ArrayList();
        // Background
        //Texture2D background;
        Background bg1;

        // Music
        protected Song backgroundMusic;

        // Debug
        public String msg = ": This is a game : ";

        public Main()
        {
            // Window properties
            WindowProperties();
            Content.RootDirectory = "Content";

        }
        private void WindowProperties()
        {
            this.IsMouseVisible = true;
            this.Window.Title = "Terriablo v." + VERSION_NUMBER.ToString();
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            bg1 = new Background();
            Entities.Add(bg1);
            
            player = new Player();
            player.movement.setBounds(GraphicsDevice);
            CEntities.Add(player);


            tile_image_locations.Add("images/terraria/item_140");
            tile_image_locations.Add("images/terraria/item_141");
            tile_image_locations.Add("images/terraria/item_142");
            tile_image_locations.Add("images/terraria/item_143");
            Vector2 tPosition = new Vector2();
            tPosition.X = 235;
            tPosition.Y = 235;
            for (int i = 0; i < 5; i++)
            {
                Random rand = new Random();
                tPosition.X -= 16;
                tPosition.Y -= 16;
                Tile t = new Tile();
                t.Initialize(Content, (string)tile_image_locations[rand.Next(0, 3)], tPosition);
                tiles.Add(t);
                CEntities.Add(t);
			}
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            player.Initialize(Content);
            

            /// Onscreen font
            font = Content.Load<SpriteFont>("mainFont");

            ///Draw content
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ///Music
            backgroundMusic = Content.Load<Song>("sound/Not As It Seems");
            MediaPlayer.Play(backgroundMusic);

            ///Background images
            //background = Content.Load<Texture2D>("images/background");
            Vector2 testPosition = new Vector2();
            testPosition.X = 33;
            testPosition.Y = 33;
            bg1.Initialize(Content, "images/background", testPosition);


            ///Tiles
            tile_image_locations.Add("images/terraria/item_140");
            tile_image_locations.Add("images/terraria/item_141");
            tile_image_locations.Add("images/terraria/item_142");
            tile_image_locations.Add("images/terraria/item_143");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            foreach (Entity ent in Entities)
            {
                ent.Update(gameTime);
            }
            foreach (Entity ent in CEntities)
            {
                ent.Update(gameTime);
            }
            CollisionTest();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Vector2 loc = new Vector2(0, SCREEN_HEIGHT-BLOCK_HEIGHT);
            Rectangle sourceRectangle = new Rectangle(0,0, BLOCK_WIDTH,BLOCK_HEIGHT);
            Vector2 origin = new Vector2(0, 0);
          
            spriteBatch.Begin();
            //spriteBatch.Draw(background, Vector2.Zero, Color.White);

            foreach (Entity ent in Entities)
            {
                ent.Draw(spriteBatch);
            }
            foreach (Entity ent in CEntities)
            {
                ent.Draw(spriteBatch);
            }
            Random rand = new Random();

            DebugPrint(spriteBatch, msg);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected void CollisionTest()
        {
            foreach (Tile t in tiles)
            {
                if (player.collision.Collides(t.collision))
                {
                    msg = "waaah";
                }
            }
        }
        /// DEBUG PRINT
        protected void rprint(String message)
        {
            this.msg = message;
        }
        protected void DebugPrint(SpriteBatch spriteBatch,String message)
        {
            spriteBatch.DrawString(font, message, new Vector2(0, 0), Color.Black);
        }
    }
}
