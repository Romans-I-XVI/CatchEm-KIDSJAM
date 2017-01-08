using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class DiggingPokemon : PathPokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.diglet)
        };

        public DiggingPokemon(Vector2 position, List<Vector2> path, float speed = 2, Tween tween = Tween.LinearPause) : base(textures, position, path, speed, tween)
        {
            RespawnRate = 5;
            getComponent<Sprite>().setRenderLayer(100000001);
        }

    }
}
