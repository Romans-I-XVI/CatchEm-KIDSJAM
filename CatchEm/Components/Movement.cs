using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace CatchEm
{
    public class Movement : Component, IUpdatable
    {
        public enum states
        {
            idle,
            jumping
        }

        public states state = states.idle;

        public Movement()
        {
        }

        public void update()
        {
            if (!(entity is IVelocity))
                return;

            var e = (IVelocity)entity;

            if ((Input.isKeyDown(Keys.Space) || Input.isKeyDown(Keys.W)) && state != states.jumping)
            {
                state = states.jumping;
                e.velocity += new Vector2(0, -(Gravity.accel * 25.2f));
                var gravity_component = entity.getComponent<Gravity>();
                if (gravity_component != null)
                    gravity_component.enabled = true;
            }

            if (Input.isKeyDown(Keys.D) && e.velocity.X < 800)
            {
                float x_accel = 20;
                if (e.velocity.X < 0)
                    x_accel = 80;
                e.velocity += new Vector2(x_accel, 0);
            }

            if (Input.isKeyDown(Keys.A) && e.velocity.X > -800)
            {
                float x_accel = -20;
                if (e.velocity.X > 0)
                    x_accel = -80;
                e.velocity += new Vector2(x_accel, 0);
            }

            if (!(Input.isKeyDown(Keys.A) || Input.isKeyDown(Keys.D)) && (state != states.jumping))
                e.velocity *= new Vector2(0.8f, 1);
        }
    }
}
