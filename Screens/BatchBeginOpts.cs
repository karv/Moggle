using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle.Screens
{
	public struct BatchBeginOpts
	{
		public SpriteSortMode BatchSorting;
		public BlendState Blend;
		public SamplerState Sampler;
		public DepthStencilState DepthStencil;
		public RasterizerState Rasterizer;
		public Effect Effect;
		public Func<Matrix> Transformatrix;

		public void Begin(SpriteBatch batch)
		{
			batch.Begin(BatchSorting,
									Blend,
									Sampler,
									DepthStencil,
									Rasterizer,
									Effect,
									Transformatrix());
		}
	}
}
