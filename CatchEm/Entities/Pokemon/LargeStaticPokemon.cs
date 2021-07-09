using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace CatchEm
{
    public class LargeStaticPokemon : Pokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            //Core.content.Load<Texture2D>(Content.Textures.Pokemon.lapras),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.lapras_2),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.snorlax),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.executor),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.waterdragon),
        };

        public LargeStaticPokemon(Vector2 position) : base(textures, position)
        {
            RespawnRate = 240;
        }
    }
}
