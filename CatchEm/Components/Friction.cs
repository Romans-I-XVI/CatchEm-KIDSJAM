using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
    public class Friction : Component
    {
        Collider _collider;
        float _vertical_friction;
        float _horizontal_friction;
        public delegate void dgCollision(CollisionResult collisionResult);
        public event dgCollision Collision;

        public Friction(Collider collider, float vertical_friction = 0.6f, float horizontal_friction = 0.99f)
        {
            _vertical_friction = vertical_friction;
            _horizontal_friction = horizontal_friction;
            _collider = collider;
        }

        public CollisionResult process()
        {
            CollisionResult collisionResult = new CollisionResult();
            if (!(entity is IVelocity))
                return collisionResult;

            var velocity = ((IVelocity)entity).velocity;

            if (_collider.collidesWithAny(out collisionResult) && collisionResult.collider.entity.getComponent<Sprite>() != null && collisionResult.collider.entity.getComponent<Sprite>().enabled)
            {
                //velocity = new Vector2(velocity.X * 0.99f, velocity.Y * 0.99f);
                var y_flip = 1;
                var x_flip = 1;
                entity.position -= collisionResult.minimumTranslationVector;
                if ((collisionResult.minimumTranslationVector.X > 0 && velocity.X > 0) || (collisionResult.minimumTranslationVector.X < 0 && velocity.X < 0))
                    x_flip = -1;
                if ((collisionResult.minimumTranslationVector.Y > 0 && velocity.Y > 0) || (collisionResult.minimumTranslationVector.Y < 0 && velocity.Y < 0))
                    y_flip = -1;
                velocity *= new Vector2(x_flip, y_flip);
                velocity *= new Vector2(_horizontal_friction, _vertical_friction);

                if (Collision != null)
                    Collision(collisionResult);
                //if (Math.Abs(velocity.Y) < 50)
                //    velocity = new Vector2(velocity.X, 0);
                //if (Math.Abs(velocity.X) < 1)
                //    velocity = new Vector2(0, velocity.Y);
            }

            ((IVelocity)entity).velocity = velocity;

            return collisionResult;
        }
    }
}
