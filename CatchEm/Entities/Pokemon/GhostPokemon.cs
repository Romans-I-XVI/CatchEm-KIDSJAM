using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class GhostPokemon : Pokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.gastly),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.haunter),
            Core.content.Load<Texture2D>(Content.Textures.Pokemon.gengar),
        };

        bool changing_location;
        states state = states.fade_in;
        GameTimeSpan timer = new GameTimeSpan();
        enum states
        {
            fade_in,
            fade_out,
        }


        public GhostPokemon(Vector2 position) : base(textures, position)
        {
            getComponent<Collider>().physicsLayer = (1 << 4);
        }

        public override void update()
        {
            base.update();

            if (changing_location)
            {
                CollisionResult collisionResult = new CollisionResult();
            change_position:
                position = new Vector2(Nez.Random.range(200 + 25, 4800 - 25), Nez.Random.range(2500, 4800 - 25));
                if (getComponent<Collider>().collidesWithAny(out collisionResult))
                {
                    goto change_position;
                }
                changing_location = false;
                state = states.fade_in;
                timer.Mark();
            }

            var sprite = getComponent<Sprite>();

            if (state == states.fade_in)
            {
                if (sprite.color.A < 255)
                {
                    sprite.color.A += 5;
                    sprite.color.B += 5;
                    sprite.color.G += 5;
                    sprite.color.R += 5;
                }
                if (timer.TotalMilliseconds > 3000)
                    state = states.fade_out;
            }
            else
            {
                if (sprite.color.A > 0)
                {
                    sprite.color.A -= 5;
                    sprite.color.B -= 5;
                    sprite.color.G -= 5;
                    sprite.color.R -= 5;
                }
                else
                    changing_location = true;
            }
        }
    }
}
