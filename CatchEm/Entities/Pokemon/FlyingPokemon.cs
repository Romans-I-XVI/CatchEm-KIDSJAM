using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace CatchEm
{
    public class FlyingPokemon : PathPokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.koffing),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.geodude),
        };

        public FlyingPokemon(Vector2 position, List<Vector2> path, float speed = 5, Tween tween = Tween.Sinusoidal) : base(textures, position, path, speed, tween)
        {
        }

    }
}
