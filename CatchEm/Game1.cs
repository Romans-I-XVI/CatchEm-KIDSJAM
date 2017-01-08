using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace CatchEm
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {

        public Game1() : base(isFullScreen: true, windowTitle: "CatchEm")
        {
        }
        protected override void Initialize()
        {
            base.Initialize();
            var scene_play = new ScenePlay();
            Core.scene = scene_play;
        }
    }
}
