using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Pokeball : Entity, IVelocity
    {
        public static Texture2D texture = Core.content.Load<Texture2D>(Content.Textures.ball);
        public Vector2 velocity { get; set; }
        public bool HasCollided = false;
        Player _player;

        public Pokeball(Player player, Vector2 position, Vector2 velocity)
        {
            this._player = player;
            this.position = position;
            this.velocity = velocity;
            addComponent(new Sprite(texture));
            var main_collider = new CircleCollider();
            main_collider.collidesWithLayers = (1 << 2) | (1 << 3);
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
