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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
 
        private Texture2D background;
        private Texture2D shuttle;
        private Texture2D earth;
        private Texture2D arrow;
        private SpriteFont font;
        private uint score = 0;
        private float angle = 0;
        private AnimatedSprite animatedSprite;
        ParticleEngine particleEngine;
        //private RotatedSprite rotatedSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            background = Content.Load<Texture2D>("images/stars"); // change these names to the names of your images
            shuttle = Content.Load<Texture2D>("images/shuttle");  // if you are using your own images.
            earth = Content.Load<Texture2D>("images/earth");
            Texture2D texture = Content.Load<Texture2D>("images/SmileyWalk");
            arrow = Content.Load<Texture2D>("images/arrow");
            animatedSprite = new AnimatedSprite(texture, 4, 4);


            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("images/circle"));
            textures.Add(Content.Load<Texture2D>("images/star"));
            textures.Add(Content.Load<Texture2D>("images/diamond"));
            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));




            font = Content.Load<SpriteFont>("kootenay");
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

            // TODO: Add your update logic here
            base.Update(gameTime);
            score += 4431551;
            angle+=0.01f;
            animatedSprite.Update();
            //rotatedSprite.Update();

            particleEngine.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            particleEngine.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here




            base.Draw(gameTime);



            spriteBatch.Begin();
            Vector2 loc = new Vector2(400, 240);
            Rectangle sourceRectangle = new Rectangle(0, 0, 200, 200);
            Vector2 origin = new Vector2(0, 0);

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(earth, new Vector2(400, 250), Color.White);
            spriteBatch.Draw(arrow, loc, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
            //spriteBatch.Draw(shuttle, new Vector2(450, 250), Color.White);
            //spriteBatch.DrawString(font, "Score: " + score, new Vector2(100, 100), Color.Red);

            spriteBatch.End();

            animatedSprite.Draw(spriteBatch, new Vector2(400, 200));
            //rotatedSprite.Draw(spriteBatch, new Vector2(400, 200));
            particleEngine.Draw(spriteBatch);
        }
    }
}
