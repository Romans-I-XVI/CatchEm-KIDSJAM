using System;
using Microsoft.Xna.Framework;
using Nez;

namespace CatchEm
{
    public class Friction : Component
    {
        Collider _collider;
        public Friction(Collider collider)
        {
            _collider = collider;
        }

        public CollisionResult process()
        {
            CollisionResult collisionResult = new CollisionResult();
            if (!(entity is IVelocity))
                return collisionResult;

            var velocity = ((IVelocity)entity).velocity;

            if (_collider.collidesWithAny(out collisionResult))
            {
                //velocity = new Vector2(velocity.X * 0.99f, velocity.Y * 0.99f);
                var y_flip = 1;
                var x_flip = 1;
                if ((collisionResult.minimumTranslationVector.X > 0 && velocity.X > 0) || (collisionResult.minimumTranslationVector.X < 0 && velocity.X < 0))
                    x_flip = -1;
                if ((collisionResult.minimumTranslationVector.Y > 0 && velocity.Y > 0) || (collisionResult.minimumTranslationVector.Y < 0 && velocity.Y < 0))
                    y_flip = -1;
                velocity *= new Vector2(x_flip, y_flip);
                velocity *= new Vector2(0.99f, 0.60f);
                if (Math.Abs(velocity.Y) < 50)
                    velocity = new Vector2(velocity.X, 0);
                if (Math.Abs(velocity.X) < 1)
                    velocity = new Vector2(0, velocity.Y);
            }

            ((IVelocity)entity).velocity = velocity;

            return collisionResult;
        }
    }
}
