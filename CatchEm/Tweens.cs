﻿using System;
using System.Collections.Generic;
namespace CatchEm
{

    public enum Tween
    {
        Linear,
        LinearPause,
        Sinusoidal
    }
    public static class Tweens
    {
        public static float SwitchTween(Tween tween, float start, float finish, float currentTime, float duration)
        {
            switch (tween)
            {
                case Tween.Linear:
                    return LinearTween(start, finish, currentTime, duration);
                case Tween.Sinusoidal:
                    return SinusoidalTween(start, finish, currentTime, duration);
                case Tween.LinearPause:
                    return LinearPauseTween(start, finish, currentTime, duration);
                default:
                    return 0;
            }
        }

        public static float LinearTween(float start, float finish, float currentTime, float duration)
        {
            if (currentTime > duration || Math.Abs(duration) <= 0)
                return finish;
            float change = finish - start;
            float time = currentTime / duration;
            return change * time + start;
        }

        public static float LinearPauseTween(float start, float finish, float currentTime, float duration)
        {
            if (currentTime > duration || Math.Abs(duration) <= 0)
                return finish;
            float change = finish - start;
            float time = (currentTime * 1.5f) / duration;
            if (time > 1)
                time = 1;
            return change * time + start;
        }

        public static float SinusoidalTween(float start, float finish, float currentTime, float duration)
        {
            if (currentTime > duration || duration == 0)
                return finish;
            float change = finish - start;
            float time = currentTime / duration;
            return (float)(change * Math.Sin(time * (Math.PI / 2)) + start);
        }
    }
}
