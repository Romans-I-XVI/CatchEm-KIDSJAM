using System;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CatchEm
{
    public class PathPokemon : Pokemon
    {
        public static List<Texture2D> textures = new List<Texture2D>()
        {
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player),
            Core.content.Load<Texture2D>(Content.Textures.player)
        };
        int _dest_point = 0;
        float _speed;
        List<Vector2> _path;
        GameTimeSpan movement_timer = new GameTimeSpan();
        Tween _tween;

        public PathPokemon(Vector2 position, List<Vector2> path, float speed = 5, Tween tween = Tween.Sinusoidal) : base(textures[Nez.Random.range(0, 4)], position)
        {
            _tween = tween;
            _speed = speed;
            _path = path;
            this.position = position;
            addComponent(new BoxCollider());
            velocity = new Vector2(speed, 0);
        }

        protected override void UncaughtMovementProcessor()
        {
            Vector2 start_position;
            if (_dest_point > 0)
                start_position = _path[_dest_point - 1];
            else
                start_position = _path[_path.Count - 1];
            var dest_position = _path[_dest_point];
            var current_position = position;
            var total_distance = Movement.TotalDistance(start_position, dest_position);
            var duration = total_distance / (_speed * 0.1f);

            position = new Vector2(
                Tweens.SwitchTween(_tween, start_position.X, dest_position.X, movement_timer.TotalMilliseconds, duration),
                Tweens.SwitchTween(_tween,start_position.Y, dest_position.Y, movement_timer.TotalMilliseconds, duration)
            );

            float remaining_distance = Movement.TotalDistance(position, dest_position);
            if (remaining_distance <= _speed)
            {
                movement_timer.Mark();
                if (_path.Count > _dest_point + 1)
                    _dest_point++;
                else
                    _dest_point = 0;
            }
        }
    }
}
