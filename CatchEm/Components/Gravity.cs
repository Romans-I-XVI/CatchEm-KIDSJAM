using System;
using Nez;

namespace CatchEm
{
    public class Gravity : Component, IUpdatable
    {
        public const float accel = 40f;

        public Gravity()
        {
        }

        public void update()
        {
            if (entity is IVelocity)
            {
                var cv = ((IVelocity)entity).velocity;
                cv.Y += (accel * 60) * Time.deltaTime;
                ((IVelocity)entity).velocity = cv;
            }
        }
    }
}
