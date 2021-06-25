using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace CatchEm
{
	public class HatchingPokemon : Pokemon
	{
		public static List<Texture2D> egg_textures = new List<Texture2D>()
		{
			Core.content.Load<Texture2D>(Content.Textures.Pokemon.bananaman_egg),
		};

		public static List<Texture2D> pokemon_textures = new List<Texture2D>()
		{
			Core.content.Load<Texture2D>(Content.Textures.Pokemon.bananaman),
		};

		public bool Hatched { get; private set; } = false;

		public HatchingPokemon(Vector2 position, List<Vector2> path, float speed = 5, Tween tween = Tween.LinearPause) : base(egg_textures, position)
		{
		}

		public void Hatch() {
			if (this.Hatched)
				return;

			var sprite = this.getComponent<Sprite>();
			if (sprite != null) {
				this.removeComponent(sprite);
			}

			int i = Nez.Random.range(0, pokemon_textures.Count);
			addComponent(new Sprite(pokemon_textures[i]));
			getComponent<Sprite>().setRenderLayer(100);

			this.Hatched = true;
		}
	}
}
