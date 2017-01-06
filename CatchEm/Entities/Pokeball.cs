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
        bool HasCollided = false;

        public Pokeball(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity;
            addComponent(new Sprite(texture));
            var main_collider = new CircleCollider();
            main_collider.collidesWithLayers = 2;
            addComponent(main_collider);
            addComponent(new Gravity());
            addComponent(new Friction(main_collider));
        }

        public override void update()
        {
            base.update();

            bool collided = getComponent<Friction>().process();

            if (collided)
                HasCollided = true;

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
