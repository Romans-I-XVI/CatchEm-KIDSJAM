using System;
using Nez;

namespace CatchEm
{
    public class ScenePlay : Scene
    {
        public ScenePlay()
        {
            //setDesignResolution(1280, 720, SceneResolutionPolicy.BestFit, 46, 26);
            var map_height = 5000;
            var map_width = 5000;
            addRenderer(new DefaultRenderer());

            // Create Borders
            addEntity(new Wall(map_width / 2, map_height + 180, map_width + 100, 500));
            addEntity(new Wall(map_width / 2, -180, map_width + 100, 500));
            addEntity(new Wall(-180, map_height / 2, 500, map_height - 100));
            addEntity(new Wall(map_width + 180, map_height / 2, 500, map_height - 100));

            // Create Platforms
            addEntity(new Wall(500, map_height - 200, 100, 100));
            addEntity(new Wall(500 + 200 * 1, map_height - 200 * 1, 100, 100));
            addEntity(new Wall(500 + 200 * 2, map_height - 200 * 2, 100, 100));
            addEntity(new Wall(500 + 200 * 3, map_height - 200 * 3, 100, 100));
            addEntity(new Wall(500 + 200 * 4, map_height - 200 * 4, 100, 100));


            // Add Player
            var player = new Player(camera);
            addEntity(player);
            addEntity(new AimArrow(player));

            // Add Pokemon
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            addEntity(new Pokemon(Player.texture));
            //daddEntity(new Crosshair(camera));
        }
    }
}
