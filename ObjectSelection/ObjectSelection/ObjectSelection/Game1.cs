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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ObjectSelection
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model asteroid;
        Model smallShip;
        Model largeShip;

        Matrix asteroidWorld;
        Matrix smallShipWorld;
        Matrix largeShipWorld;

        string message = "Picking does not work yet.";
        string fps = "FPS";
        SpriteFont font;

        private Matrix view = Matrix.CreateLookAt(new Vector3(10, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);
        private SoundEffect soundFX;
        protected Song backgroundMusic;
        public Game1()
        {
            this.IsMouseVisible = true;
            this.Window.Title = "TERRARIABLO";
            graphics = new GraphicsDeviceManager(this);
            //graphics.PreferredBackBufferWidth = 500;
            //graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }
        public Ray CalculateRay(Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 0.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 farPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 1.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }
        public float? IntersectDistance(BoundingSphere sphere, Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(sphere);
        }
        public bool Intersects(Vector2 mouseLocation,
            Model model, Matrix world,
            Matrix view, Matrix projection,
            Viewport viewport)
        {
            for (int index = 0; index < model.Meshes.Count; index++)
            {
                BoundingSphere sphere = model.Meshes[index].BoundingSphere;
                sphere = sphere.Transform(world);
                float? distance = IntersectDistance(sphere, mouseLocation, view, projection, viewport);

                if (distance != null)
                {
                    return true;
                }
            }

            return false;
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

            font = Content.Load<SpriteFont>("font");

            asteroid = Content.Load<Model>("LargeAsteroid");
            smallShip = Content.Load<Model>("Ship");
            largeShip = Content.Load<Model>("ship2");
            soundFX = Content.Load<SoundEffect>("The Path of the Goblin King");
            backgroundMusic = Content.Load<Song>("Not As It Seems");
            MediaPlayer.Play(backgroundMusic);
            //soundFX.Play(1.0f,0.0f,0.0f);
            asteroidWorld = Matrix.CreateTranslation(new Vector3(5, 0, 0));
            smallShipWorld = Matrix.CreateTranslation(new Vector3(0, 0, 5));
            largeShipWorld = Matrix.CreateTranslation(new Vector3(-30, -30, -30));
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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            Vector2 mouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Viewport viewport = this.GraphicsDevice.Viewport;

            bool mouseOverSomething = false;

            fps = "FPS:"+ (1 / (float)gameTime.ElapsedGameTime.TotalSeconds).ToString();
            if (Intersects(mouseLocation, asteroid, asteroidWorld, view, projection, viewport))
            {
                message = "Mouse Over:  Asteroid";
                mouseOverSomething = true;
            }
            if (Intersects(mouseLocation, smallShip, smallShipWorld, view, projection, viewport))
            {
                message = "Mouse Over:  Small Ship";
                mouseOverSomething = true;
            }
            if (Intersects(mouseLocation, largeShip, largeShipWorld, view, projection, viewport))
            {
                message = "Mouse Over:  Large Ship";
                mouseOverSomething = true;
            }

            if (!mouseOverSomething)
            {
                message = "Mouse Over:  None";
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawModel(asteroid, asteroidWorld, view, projection);
            DrawModel(smallShip, smallShipWorld, view, projection);
            DrawModel(largeShip, largeShipWorld, view, projection);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.DrawString(font, message, new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, fps, new Vector2(100, 200), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
    }
}