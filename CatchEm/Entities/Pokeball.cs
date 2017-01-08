using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokeball : Entity, IVelocity
    {
        List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.ball_1),
            Core.content.Load<Texture2D>(Content.Textures.ball_2),
            Core.content.Load<Texture2D>(Content.Textures.ball_3),
            Core.content.Load<Texture2D>(Content.Textures.ball_4),
            Core.content.Load<Texture2D>(Content.Textures.ball_4),
            Core.content.Load<Texture2D>(Content.Textures.ball_4),
            //Core.content.Load<Texture2D>(Content.Textures.ball_5)
        };
        public Vector2 velocity { get; set; }
        public bool HasCollided = false;
        Player _player;

        public Pokeball(Player player, Vector2 position, Vector2 velocity)
        {
            this._player = player;
            this.position = position;
            this.velocity = velocity;
            addComponent(new Sprite(textures[Nez.Random.range(0, textures.Count)]));
            var main_collider = new CircleCollider();
            main_collider.collidesWithLayers = (1 << 2) | (1 << 3) | (1 << 4);
            addComponent(main_collider);
            addComponent(new Gravity());
            addComponent(new Friction(main_collider));
        }

        public override void update()
        {
            base.update();

            CollisionResult collisionResult;
            collisionResult = getComponent<Friction>().process();

            if (!HasCollided && collisionResult.collider != null)
            {
                HasCollided = true;
                if (collisionResult.collider.entity is Pokemon && _player != null)
                {
                    var pokemon = (Pokemon)collisionResult.collider.entity;
                    pokemon.Catch(_player.NumberOfPokemon);
                    _player.NumberOfPokemon++;
                }
            }

            position += new Vector2(velocity.X * Time.deltaTime, velocity.Y * Time.deltaTime);

            if (HasCollided)
            {
                getComponent<Sprite>().color *= 0.9f;
                if (getComponent<Sprite>().color.A < 0.1f)
                    destroy();
            }

        }
    }
}
