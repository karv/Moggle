﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using Moggle.Screens;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.InputListeners;
using MonoGame.Extended.Shapes;

namespace Moggle.Controles
{
	/// <summary>
	/// Permite entrar un renglón de texto
	/// </summary>
	public class EntradaTexto : DSBC, IReceptor<KeyboardEventArgs>
	{
		#region ctor

		/// <summary>
		/// </summary>
		/// <param name="screen">Screen.</param>
		public EntradaTexto (IScreen screen)
			: base (screen)
		{
			StringListen = new KeyStringListener ();
		}

		#endregion

		#region Estado

		/// <summary>
		/// Devuelve o establece el texto visible (editable)
		/// </summary>
		public string Texto
		{
			get { return StringListen.CurrentString; }
			set { StringListen.CurrentString = value; }
		}

		/// <summary>
		/// Si el control debe responder al estado del teclado.
		/// </summary>
		public bool CatchKeys = true;

		/// <summary>
		/// Gets or sets the background texture.
		/// </summary>
		/// <value>The background texture.</value>
		public string BgTexture { get; set; }

		/// <summary>
		/// Gets or sets the font texture.
		/// </summary>
		/// <value>The font texture.</value>
		public string FontTexture { get; set; }

		#endregion

		#region Comportamiento

		/// <summary>
		/// Devuelve o establece la posición del control.
		/// </summary>
		/// <value>The position.</value>
		public Vector2 Pos
		{
			get
			{
				return Bounds.Location;
			}
			set
			{
				Bounds = new RectangleF (value, Bounds.Size);
			}
		}

		/// <summary>
		/// Color del contorno
		/// </summary>
		public Color ColorContorno = Color.White;
		/// <summary>
		/// Color del texto
		/// </summary>
		public Color ColorTexto = Color.White;

		/// <summary>
		/// Límites de el control
		/// </summary>
		public RectangleF Bounds { get; set; }

		/// <summary>
		/// Devuelve el límite gráfico del control.
		/// </summary>
		/// <returns>The bounds.</returns>
		protected override IShapeF GetBounds ()
		{
			return Bounds;
		}

		/// <summary>
		/// Update lógico
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public override void Update (GameTime gameTime)
		{
		}

		#endregion

		#region Dibujo

		Texture2D contornoTexture;
		BitmapFont fontTexture;

		/// <summary>
		/// Dibuja el control
		/// </summary>
		protected override void Draw ()
		{
			var bat = Screen.Batch;
			Primitivos.DrawRectangle (
				bat,
				GetBounds ().GetBoundingRectangle (),
				ColorContorno,
				contornoTexture);
			bat.DrawString (fontTexture, Texto, Pos, ColorTexto);
		}

		#endregion

		#region Memoria

		/// <summary>
		/// Cargar contenido
		/// </summary>
		protected override void AddContent ()
		{
			var manager = Screen.Content;
			manager.AddContent (BgTexture);
			manager.AddContent (FontTexture);
		}

		/// <summary>
		/// Vincula el contenido  a campos de clase
		/// </summary>
		protected override void InitializeContent ()
		{
			var manager = Screen.Content;
			contornoTexture = manager.GetContent<Texture2D> (BgTexture);
			fontTexture = manager.GetContent<BitmapFont> (FontTexture);
		}

		#endregion

		#region Teclado

		KeyStringListener StringListen { get; }

		/// <summary>
		/// Esta función establece el comportamiento de este control cuando el jugador presiona una tecla dada.
		/// </summary>
		/// <param name="key">Tecla presionada por el usuario.</param>
		bool IReceptor<KeyboardEventArgs>.RecibirSeñal (KeyboardEventArgs key)
		{
			return StringListen.RecibirSeñal (key);
		}

		#endregion

		#region Dispose

		/// <summary>
		/// Releases all resource used by the <see cref="Moggle.Controles.EntradaTexto"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Moggle.Controles.EntradaTexto"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Moggle.Controles.EntradaTexto"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Moggle.Controles.EntradaTexto"/>
		/// so the garbage collector can reclaim the memory that the <see cref="Moggle.Controles.EntradaTexto"/> was occupying.</remarks>
		protected override void Dispose ()
		{
			base.Dispose ();
			StringListen.Dispose ();
		}

		#endregion
	}
}