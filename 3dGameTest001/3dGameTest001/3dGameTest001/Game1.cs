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

namespace _3dGameTest001
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Model model;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private float mainRotorAngle = 0;
        private float tailRotorAngle = 0;
        private Vector3 position = new Vector3(0, 0, 0);
        private float angle = 0f;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(10, 10, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 600f, 0.1f, 100f);
        private Vector3 lastPosition = new Vector3(0, 0, 0);
        private Vector3 newPosition = new Vector3(0, 0, 0);
        private Vector3 moveSpeed = new Vector3(.1f, 0, 0);
        private int moveDirection = 0;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            position = new Vector3(0, 0, 0);

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
            model = Content.Load<Model>("models/Helicopter");
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
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right) || (state.IsKeyDown(Keys.D)))
            {
                angle -=0.02f;
                tailRotorAngle -= 0.15f;
                mainRotorAngle -= 0.15f;
            }

            else if (state.IsKeyDown(Keys.Left) || (state.IsKeyDown(Keys.A)))
            {
                
                angle +=0.02f;
                tailRotorAngle += 0.15f;
                mainRotorAngle += 0.15f;
            }
            moveDirection = 0;
            if (state.IsKeyDown(Keys.Up) || (state.IsKeyDown(Keys.W)))
            {
                moveDirection = 1;
            }
            else if (state.IsKeyDown(Keys.Down) || (state.IsKeyDown(Keys.S)))
            {
                moveDirection = -1;
            }


            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                tailRotorAngle -= 0.15f;
                mainRotorAngle -= 0.15f;
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {

                tailRotorAngle += 0.15f;
                mainRotorAngle += 0.15f;
            }

            newPosition = lastPosition + moveSpeed * moveDirection;
            position = Vector3.Transform(lastPosition, Matrix.CreateRotationY(MathHelper.ToRadians(90) + angle));
            lastPosition = newPosition;


            world = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);
        }



        private void DrawModel(Model model, Matrix objectWorldMatrix, Matrix[] meshWorldMatrices, Matrix view, Matrix projection)
        {
            for (int i = 0; i< model.Meshes.Count; ++i)
            {
                ModelMesh mesh = model.Meshes[i];
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.TextureEnabled = true;
                    effect.FogEnabled = false;
                    effect.FogColor = Color.CornflowerBlue.ToVector3(); // For best results, ake this color whatever your background is.
                    effect.FogStart = 9.75f;
                    effect.FogEnd = 10.25f;
                    effect.LightingEnabled = true; // turn on the lighting subsystem.
                    effect.PreferPerPixelLighting = true;
                    //effect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0, 0); // a red light
                    //effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                    //effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights
                    //effect.DirectionalLight0.Enabled = false;
                    //effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
                    //effect.EmissiveColor = new Vector3(1, 0, 0);
                    effect.World = mesh.ParentBone.Transform * meshWorldMatrices[i] * objectWorldMatrix;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           // GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Bisque);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            this.IsMouseVisible = true;


            Matrix[] meshWorldMatrices = new Matrix[3];
            meshWorldMatrices[0] = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            meshWorldMatrices[1] = Matrix.CreateRotationY(mainRotorAngle);
            meshWorldMatrices[2] = Matrix.CreateTranslation(new Vector3(0, -0.25f, -3.4f)) *
                                                        Matrix.CreateRotationX(tailRotorAngle) *
                                                        Matrix.CreateTranslation(new Vector3(0, 0.25f, 3.4f));

            world = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);

            DrawModel(model, world, meshWorldMatrices, view, projection);



        }

    }
}
