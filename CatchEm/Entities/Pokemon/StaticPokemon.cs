using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace CatchEm
{
    public class SmallStaticPokemon : Pokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.cubone),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.bellsprout),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.gloom),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.voltorb),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.electrode),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.chansey),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.shelder),
        };

        readonly GameTimeSpan _timer = new GameTimeSpan();

        public SmallStaticPokemon(Vector2 position) : base(textures, position)
        {
            getComponent<Collider>().physicsLayer = (1 << 4);
        }
    }
}
