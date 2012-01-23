using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParticleEngine
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        private int currentParticles = 0;
        private int maxParticles = 53;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void Update()
        {

            for (int i = 0; i < maxParticles; i++)
            {
                if (currentParticles < maxParticles)
                {
                    particles.Add(GenerateNewParticle());
                    currentParticles++;
                }
            }

            for (int i = 0; i< particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].Lifetime <= 0)
                {
                    particles.RemoveAt(i);
                    i--;
                    currentParticles--;
                }
            }
        }


        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() / 2 - 1),
                                    1f * (float)(random.NextDouble() / 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                        (float)random.NextDouble(),
                        (float)random.NextDouble(),
                        (float)random.NextDouble());
            float size = (float)random.NextDouble()/3;
            int lifetime = 20 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, lifetime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
