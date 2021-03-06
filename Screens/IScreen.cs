﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moggle.Comm;
using Moggle.Controles;
using MonoGame.Extended.Input.InputListeners;


namespace Moggle.Screens
{
	/// <summary>
	/// Representa una pantalla con controles visibles al jugador.
	/// </summary>
	public interface IScreen :
	IEmisor<KeyboardEventArgs>,                         // Puede emitir datos de teclas a sus componentes
	IReceptor<Tuple<KeyboardEventArgs, ScreenThread>>,  // Ouede recibir datos desde un thread
	IComponentContainerComponent<IControl>,             // Contiene controles
	IControl,                                           // Posee un contenedor
	IDisposable
	{
		#region Dibujo

		/// <summary>
		/// Dibuja la pantalla
		/// </summary>
		void Draw ();

		#endregion

		#region Comportamiento

		/// <summary>
		/// Ciclo de la lógica
		/// </summary>
		void Update (GameTime gameTime, ScreenThread currentThread);

		/// <summary>
		/// Color de fondo
		/// </summary>
		Color? BgColor { get; }

		#endregion

		#region Memoria

		/// <summary>
		/// El manejador de contenido
		/// </summary>
		/// <value>The content.</value>
		ContentManager Content { get; }

		/// <summary>
		/// Loads the content of the all it's components
		/// </summary>
		void LoadAllContent ();

		#endregion

		#region hardware

		/// <summary>
		/// Batch de dibujo
		/// </summary>
		SpriteBatch Batch { get; }

		/// <summary>
		/// Devuelve el modo actual de display gráfico.
		/// </summary>
		DisplayMode GetDisplayMode { get; }

		/// <summary>
		/// Devuelve el controlador gráfico.
		/// </summary>
		GraphicsDevice Device { get; }

		#endregion

		#region Game

		/// <summary>
		/// Devuelve el campo Juego.
		/// </summary>
		Game Juego { get; }

		#endregion
	}
}