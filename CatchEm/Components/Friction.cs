using System;
using Nez;

namespace CatchEm
{
    public class Friction : Component, IUpdatable
    {
        Collider _collider;
        public Friction(Collider collider)
        {
            _collider = collider;
        }

        public void update()
        {
            CollisionResult collisionResult;
            Console.WriteLine(_collider.collidesWithAny(out collisionResult));
            if (_collider.collidesWithAny(out collisionResult))
                if (entity is IVelocity)
                    ((IVelocity)entity).velocity *= new Microsoft.Xna.Framework.Vector2(0.94f, 0.94f);
        }
    }
}
