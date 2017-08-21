using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Moggle.Screens
{
	/// <summary>
	/// Argments for <see cref="SpriteBatch.Begin"/>.
	/// </summary>
	public struct BatchBeginOpts
	{
		/// <summary>
		/// Rendering order.
		/// </summary>
		public SpriteSortMode BatchSorting;
		/// <summary>
		/// Blend.
		/// </summary>
		public BlendState Blend;
		/// <summary>
		/// Sampler state.
		/// </summary>
		public SamplerState Sampler;
		/// <summary>
		/// Depth stencil state.
		/// </summary>
		public DepthStencilState DepthStencil;
		/// <summary>
		/// Rasterizer state,
		/// </summary>
		public RasterizerState Rasterizer;
		/// <summary>
		/// Effect.
		/// </summary>
		public Effect Effect;
		/// <summary>
		/// The transformation matrix.
		/// </summary>
		public Func<Matrix> Transformatrix;

		/// <summary>
		/// Begin the specfied batch with this options.
		/// </summary>
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