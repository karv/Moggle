﻿using Moggle.Shape;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Moggle.Controles
{
	/// <summary>
	/// Un control que interactúa con los clicks del ratón.
	/// </summary>
	public class Botón : SBC
	{
		/// <summary>
		/// Inicaliza un <see cref="Botón"/> rectangular.
		/// </summary>
		/// <param name="screen">Screen.</param>
		public Botón (Moggle.Screens.IScreen screen)
			: base (screen)
		{
			Color = Color.White;
		}

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> de una forma arbitraria.
		/// </summary>
		/// <param name="screen">Screen.</param>
		/// <param name="shape">Forma del botón</param>
		public Botón (Moggle.Screens.IScreen screen, IShape shape)
			: this (screen)
		{
			Bounds = shape;
		}

		/// <summary>
		/// Inicaliza un <see cref="Botón"/> rectangular.
		/// </summary>
		/// <param name="screen">Screen.</param>
		/// <param name="bounds">Límites del rectángulo.</param>
		public Botón (Moggle.Screens.IScreen screen,
		              Microsoft.Xna.Framework.Rectangle bounds)
			: this (screen)
		{
			Bounds = new Moggle.Shape.Rectangle (bounds.Location, bounds.Size);
		}

		/// <summary>
		/// Devuelve o establece si el botón está habilitado para interacción con el jugador.
		/// </summary>
		/// <value><c>true</c> if habilidato; otherwise, <c>false</c>.</value>
		public bool Habilidato { get; set; }

		/// <summary>
		/// Devuelve o restablece la forma del botón
		/// </summary>
		/// <value>The bounds.</value>
		public IShape Bounds { get; set; }

		/// <summary>
		/// Textura del fondo del botón
		/// </summary>
		public Texture2D TexturaInstancia { get; protected set; }

		/// <summary>
		/// Devuelve o establece el color de la textura de fondo <see cref="TexturaInstancia"/>
		/// </summary>
		/// <value>The color.</value>
		public Color Color { get; set; }

		/// <summary>
		/// Devuelve o establece el nombre de la textura.
		/// Si se establece, <see cref="LoadContent"/> cargará la <see cref="TexturaInstancia"/>
		/// </summary>
		public string Textura { get; set; }

		/// <summary>
		/// Dibuja el botón
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Dibujar (GameTime gameTime)
		{
			Screen.Batch.Draw (
				TexturaInstancia,
				Bounds.GetContainingRectangle (),
				Color);
		}

		/// <summary>
		/// Cargar contenido
		/// </summary>
		public override void LoadContent ()
		{
			try
			{
				TexturaInstancia = Screen.Content.Load<Texture2D> (Textura);
			}
			catch (Exception ex)
			{
				throw new Exception ("Error trying to load texture " + Textura, ex);
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.Botón"/> object.
		/// </summary>
		protected override void Dispose ()
		{
			TexturaInstancia = null;
			Textura = null;
			base.Dispose ();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Moggle.Controles.Botón"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Moggle.Controles.Botón"/>.</returns>
		public override string ToString ()
		{
			return string.Format ("{0}{1}", Habilidato ? "[H]" : "", Textura);
		}

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		public override IShape GetBounds ()
		{
			return Bounds;
		}
	}
}