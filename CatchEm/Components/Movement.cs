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
        public event dgJumped Jumped;
        public delegate void dgJumped();

        public Movement()
        {
        }

        public static Vector2 MoveTowards(Vector2 current_pos, Vector2 dest_pos, float speed)
        {
            float x_distance = current_pos.X - dest_pos.X;
            float y_distance = current_pos.Y - dest_pos.Y;
            float angle = (float)Math.Atan2(y_distance, x_distance);

            float x_speed = (float)(Math.Cos(angle) * speed);
            float y_speed = (float)(Math.Sin(angle) * speed);

            return current_pos -= new Vector2(x_speed, y_speed);
        }

        public static float TotalDistance(Vector2 position1, Vector2 position2)
        {
            float x_distance = position1.X - position2.X;
            float y_distance = position1.Y - position2.Y;
            float total_distance = (float)Math.Sqrt(x_distance * x_distance + y_distance * y_distance);
            return total_distance;
        }

        public static float Hypotenuse(Vector2 vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public void update()
        {
            if (!(entity is IVelocity))
                return;

            var e = (IVelocity)entity;

            if ((Input.isKeyDown(Keys.Space)) && state != states.jumping)
            {
                state = states.jumping;
                e.velocity += new Vector2(0, -(Gravity.accel * 25.2f));
                var gravity_component = entity.getComponent<Gravity>();
                if (gravity_component != null)
                    gravity_component.enabled = true;
                if (Jumped != null)
                    Jumped();
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
