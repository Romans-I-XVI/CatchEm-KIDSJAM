using System;
using Nez;

namespace CatchEm
{
    public class ScenePlay : Scene
    {
        public ScenePlay()
        {
            //setDesignResolution(1280, 720, SceneResolutionPolicy.BestFit, 46, 26);
            var scene_height = 5000;
            addRenderer(new DefaultRenderer());
            addEntity(new Wall(1280 / 2, scene_height + 180, 10000, 500));
            addEntity(new Wall(500, scene_height - 200, 100, 100));
            addEntity(new Player(camera));
            //daddEntity(new Crosshair(camera));
        }
    }
}
