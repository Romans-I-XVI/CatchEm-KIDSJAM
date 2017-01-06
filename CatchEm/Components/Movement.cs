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

        public static Vector2 MoveTowards(Vector2 current_pos, Vector2 dest_pos, float speed)
        {
            float x_distance = current_pos.X - dest_pos.X;
            float y_distance = current_pos.Y - dest_pos.Y;
            float total_distance = (float)Math.Sqrt(x_distance * x_distance + y_distance * y_distance);

            float angle = (float)Math.Atan2(y_distance, x_distance);

            float x_speed = (float)(Math.Cos(angle) * speed);
            float y_speed = (float)(Math.Sin(angle) * speed);

            return current_pos -= new Vector2(x_speed, y_speed);
        }

        public void update()
        {
            if (!(entity is IVelocity))
                return;

            var e = (IVelocity)entity;

            if ((Input.isKeyPressed(Keys.Space)) && state != states.jumping)
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
