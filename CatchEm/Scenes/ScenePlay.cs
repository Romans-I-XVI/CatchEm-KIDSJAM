using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Nez;
using Newtonsoft.Json;
using Microsoft.CSharp;

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
            addEntity(new Wall(-50, map_height - 100, (map_width + 100) / 50, 500 / 50));
            addEntity(new Wall(-50, 100, map_width + 100, 500 / 50));
            addEntity(new Wall(100 - 500, 50, 500 / 50, (map_height - 100) / 50));
            addEntity(new Wall(map_width - 100, 50, 500 / 50, (map_height - 100) / 50));

            // Create Platforms
            //addEntity(new Wall(500, map_height - 200, 100, 100));
            //addEntity(new Wall(500 + 200 * 1, map_height - 200 * 1, 100, 100));
            //addEntity(new Wall(500 + 200 * 2, map_height - 200 * 2, 100, 100));
            //addEntity(new Wall(500 + 200 * 3, map_height - 200 * 3, 100, 100));
            //addEntity(new Wall(500 + 200 * 4, map_height - 200 * 4, 100, 100));


            // Add Player
            var player = new Player(camera);
            addEntity(player);
            addEntity(new AimArrow(player));

            // Add Pokemon
            //addEntity(new Pokemon(Player.texture, new Vector2(200, 4200)));
            //addEntity(new Pokemon(Player.texture, new Vector2(200, 4400)));
            //addEntity(new Pokemon(Player.texture, new Vector2(200, 4600)));
            //addEntity(new Pokemon(Player.texture, new Vector2(200, 4800)));
            //addEntity(new Pokemon(Player.texture, new Vector2(600, 4200)));
            //addEntity(new Pokemon(Player.texture, new Vector2(800, 4200)));
            //addEntity(new Pokemon(Player.texture, new Vector2(900, 4200)));
            //addEntity(new Pokemon(Player.texture, new Vector2(1200, 4200)));
            //daddEntity(new Crosshair(camera));

            // Add Walls


            dynamic wall_data = JsonConvert.DeserializeObject(Utilities.ReadEmbeddedResource("WallLayout.json"));
            foreach (var wall in wall_data)
            {
                addEntity(new Wall((int)wall.x, (int)wall.y, (int)wall.width, (int)wall.height));
            }

        }
    }
}
