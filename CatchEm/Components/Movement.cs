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

        private const float MaxSpeed = 700;
        private const float AccelSpeed = 30;
        private const float DecelerationMultiplier = 0.7f;
        public states state = states.idle;
        public event dgJumped Jumped;
        public delegate void dgJumped();
        VirtualButton _jump;
        VirtualButton _right;
        VirtualButton _left;

        public Movement()
        {
            _left = new VirtualButton();
            _left.addKeyboardKey(Keys.A);
            _left.addGamePadDPad(0, Direction.Left);

            _right = new VirtualButton();
            _right.addKeyboardKey(Keys.D);
            _right.addGamePadDPad(0, Direction.Right);

            _jump = new VirtualButton();
            _jump.addKeyboardKey(Keys.Space);
            _jump.addGamePadButton(0, Buttons.A);
            _jump.addGamePadDPad(0, Direction.Up);
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

            if (_jump.isDown && state != states.jumping)
            {
                state = states.jumping;
                e.velocity += new Vector2(0, -(Gravity.accel * 25.2f));
                var gravity_component = entity.getComponent<Gravity>();
                if (gravity_component != null)
                    gravity_component.enabled = true;
                if (Jumped != null)
                    Jumped();
            }

            if (_right.isDown && e.velocity.X < Movement.MaxSpeed)
            {
                float x_accel = Movement.AccelSpeed;
                if (e.velocity.X < 0)
                    x_accel = Movement.AccelSpeed * 4;
                e.velocity += new Vector2(x_accel, 0);
            }

            if (_left.isDown && e.velocity.X > -Movement.MaxSpeed)
            {
                float x_accel = -Movement.AccelSpeed;
                if (e.velocity.X > 0)
                    x_accel = -Movement.AccelSpeed * 4;
                e.velocity += new Vector2(x_accel, 0);
            }

            if (!(_right.isDown || _left.isDown))
                e.velocity *= new Vector2(Movement.DecelerationMultiplier, 1);
        }
    }
}
