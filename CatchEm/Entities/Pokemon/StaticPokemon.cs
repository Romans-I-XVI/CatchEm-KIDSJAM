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
            //Core.content.Load<Texture2D>(Content.Textures.Pokemon.pikachu_2),
            //Core.content.Load<Texture2D>(Content.Textures.Pokemon.vulpix),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.cubone),
        };

        readonly GameTimeSpan _timer = new GameTimeSpan();

        public SmallStaticPokemon(Vector2 position) : base(textures, position)
        {
        }
    }
}
