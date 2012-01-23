using System;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private int BLOCK_WIDTH = 16;
        private int BLOCK_HEIGHT = 16;
        private int SCREEN_HEIGHT = 500;
        private int SCREEN_WIDTH = 500;
        private int tileIndex = 0;
        private int moveDirection = 0;
        private int moveSpeed = 5;

        private Spritesheet player;

        private Matrix view = Matrix.CreateLookAt(new Vector3(10, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);

        protected Song backgroundMusic;


        private Texture2D[] tiles;
        private float VERSION_NUMBER = 0.0f;
        private SpriteFont font;


        public Main()
        {
            this.IsMouseVisible = true;
            this.Window.Title = "Terriablo v." + VERSION_NUMBER.ToString() ;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;   
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Random random = new Random();
            tileIndex = random.Next(0, 5);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tiles = new Texture2D[5];
            tiles[0] = Content.Load<Texture2D>("images/terraria/Item_141");
            tiles[1] = Content.Load<Texture2D>("images/terraria/Item_129");
            tiles[1] = Content.Load<Texture2D>("images/terraria/Item_130");
            tiles[2] = Content.Load<Texture2D>("images/terraria/Item_131");
            tiles[3] = Content.Load<Texture2D>("images/terraria/Item_132");
            tiles[4] = Content.Load<Texture2D>("images/terraria/Item_133");


            Texture2D playerTexture = Content.Load<Texture2D>("images/SmileyWalk");
            player = new Spritesheet(playerTexture, 4, 4);

            backgroundMusic = Content.Load<Song>("sound/Not As It Seems");
            MediaPlayer.Play(backgroundMusic);

            font = Content.Load<SpriteFont>("mainFont");




            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here


            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Right) || (state.IsKeyDown(Keys.D)))
            {
                player.movePlayer(moveSpeed);
            }
            if (state.IsKeyDown(Keys.Left) || (state.IsKeyDown(Keys.A)))
            {
                player.movePlayer(-moveSpeed);
            }
            if (state.IsKeyDown(Keys.Up) || (state.IsKeyDown(Keys.W)))
            {
                player.movePlayer(0,-moveSpeed);
            }
            if (state.IsKeyDown(Keys.Down) || (state.IsKeyDown(Keys.S)))
            {
                player.movePlayer(0,moveSpeed);
            }






            player.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here






/*  Move this outside draw 
 *  Set up tile class
 *  World = Array of tiles
 *  Draw world
 */
            Vector2 loc = new Vector2(0, SCREEN_HEIGHT-BLOCK_HEIGHT);
            Rectangle sourceRectangle = new Rectangle(0,0, BLOCK_WIDTH,BLOCK_HEIGHT);
            Vector2 origin = new Vector2(0, 0);

            player.Draw(spriteBatch);

            spriteBatch.Begin();
            Random rand = new Random();

            for (int i = 0; i < 5; ++i)
            {
                loc.X = 0;

                for (int j = 0; j < SCREEN_WIDTH/BLOCK_WIDTH+1; ++j)
                {
                    spriteBatch.Draw(tiles[rand.Next(0,5)], loc, sourceRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 1);
                    loc.X += BLOCK_WIDTH;
                }
                loc.Y -= BLOCK_HEIGHT;
            }
            //spriteBatch.Draw(tile, new Vector2(100,100), new Rectangle(0, 0, 200, 200), Color.White, 0.0f, new Vector2(0, 0), 0.0f, SpriteEffects.None, 1);

            spriteBatch.DrawString(font, ": This is a game : ", new Vector2(0, 0), Color.Black);

            //Move player to its own class



            spriteBatch.End();


            base.Draw(gameTime);
        }




    }
}
