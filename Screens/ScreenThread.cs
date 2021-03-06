﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Moggle.Screens
{
	/// <summary>
	/// Representa una serie de invocaciones de <see cref="IScreen"/>
	/// </summary>
	public class ScreenThread : IUpdate
	{
		#region Data

		readonly List<IScreen> _invocationStack;
		readonly List<ScreenStackOptions> _options;

		#endregion

		#region Status

		/// <summary>
		/// Devuelve el número de pantallas la pila
		/// </summary>
		/// <value>The count.</value>
		public int Count { get { return _invocationStack.Count; } }

		/// <summary>
		/// Devuelve la panalla correspondiente a un índice.
		/// Hace corresponer a la pantalla actual con el índice cero
		/// </summary>
		/// <param name="index">Índice de pantalla basado en cero.</param>
		public IScreen this [int index]
		{
			// No hay necesidad de exception check. Va a devolver el error de fuera de índice, y ése corresponde
			get { return _invocationStack [Count - index - 1]; }
		}

		/// <summary>
		/// Devuelve la pantalla actual
		/// </summary>
		/// <value>The current.</value>
		public IScreen Current
		{
			get
			{
				if (Count == 0)
					throw new InvalidOperationException ("This thread has no screens.");
				return this [0];
			}
		}

		/// <summary>
		/// Devuelve la pantalla más próxima de un tipo dado
		/// </summary>
		public IScreen ClosestOfType<T> ()
			where T : IScreen
		{
			for (int i = 0; i < Count; i++)
			{
				var iterScr = this [i];
				if (iterScr is T)
					return iterScr;
			}
			throw new Exception ("There is not screen of the given type.");
		}

		/// <summary>
		/// Devuelve un nuevo <see cref="System.Collections.Generic.Stack{IScreen}"/> con las invocaciones de pantallas
		/// </summary>
		public Stack<IScreen> AsStack ()
		{
			return new Stack<IScreen> (_invocationStack);
		}

		#endregion

		#region Control

		/// <summary>
		/// Devuelve el color de fondo pedido por la pila
		/// </summary>
		public Color? BgColor
		{
			get
			{
				for (int i = Count - 1; i >= 0; i--)
				{
					var retClr = _invocationStack [i].BgColor;
					if (retClr != null)
						return retClr;
				}
				return null;
			}
		}

		[Obsolete]
		ScreenStackOptions getOptionsFromNewIndex (int index)
		{
			return _options [Count - index - 1];
		}

		void RemoveAt (int i)
		{
			_invocationStack.RemoveAt (i);
			_options.RemoveAt (i);
		}

		/// <summary>
		/// Actualizacińo lógica
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Update (GameTime gameTime)
		{
			// Localizar el primer índice que se va a dibujar
			var deepestDrawIndex = Count - 1;
			while (deepestDrawIndex > 0 && _options [deepestDrawIndex].ActualizaBase)
				deepestDrawIndex--;

			// Dibujar en orden creciente
			for (int i = deepestDrawIndex; i < Count; i++)
				_invocationStack [i].Update (gameTime, this);
		}

		/// <summary>
		/// Dibuja
		/// </summary>
		public void Draw ()
		{
			// Localizar el primer índice que se va a dibujar
			var deepestDrawIndex = Count - 1;
			while (deepestDrawIndex > 0 && _options [deepestDrawIndex].DibujaBase)
				deepestDrawIndex--;

			// Dibujar en orden creciente
			for (int i = deepestDrawIndex; i < Count; i++)
				_invocationStack [i].Draw ();
		}

		#endregion

		#region Manipulation

		/// <summary>
		/// Añade una nueva pantalla a la pila, haciendo ésta la pantalla actual
		/// </summary>
		/// <param name="scr">Nueva pantalla</param>
		/// <param name="opt">Opciones</param>
		public void Stack (IScreen scr, ScreenStackOptions opt)
		{
			if (scr == null)
				throw new ArgumentNullException (nameof (scr));

			var lastCurr = Count == 0 ? null : Current;
			_invocationStack.Add (scr);
			_options.Add (opt);
			if (lastCurr != null)
				LostPreference?.Invoke (this, lastCurr);
			GotPreference?.Invoke (this, Current);
			if (lastCurr != null)
				GotChild?.Invoke (this, lastCurr);
		}

		/// <summary>
		/// Añade una nueva pantalla a la pila, haciendo ésta la pantalla actual
		/// </summary>
		/// <param name="scr">Nueva pantalla</param>
		public void Stack (IScreen scr)
		{
			Stack (scr, ScreenStackOptions.Default);
		}

		/// <summary>
		/// Termina el último elemento del stack
		/// </summary>
		public void TerminateLast ()
		{
			var lastCurr = Current;

			RemoveAt (Count - 1);

			Terminated?.Invoke (this, lastCurr);
			LostChild?.Invoke (this, Current);
			LostPreference?.Invoke (this, lastCurr);
			GotPreference?.Invoke (this, Current);

			lastCurr.Dispose ();
		}

		#endregion

		#region Memory

		/// <summary>
		/// Releases all resource used by the <see cref="ScreenThread"/> object.
		/// </summary>
		public void Dispose ()
		{
			for (int i = 0; i < Count; i++)
				this [i].Dispose ();
			_invocationStack.Clear ();
			_options.Clear ();
		}

		#endregion

		#region Events

		/// <summary>
		/// Ocurre cuando un screen ya no es la actual.
		/// </summary>
		public event EventHandler<IScreen> LostPreference;
		/// <summary>
		/// Ocurre cuando un screen ahora es la actual
		/// </summary>
		public event EventHandler<IScreen> GotPreference;
		/// <summary>
		/// Ocurre cuando su child es terminado
		/// </summary>
		public event EventHandler<IScreen> LostChild;
		/// <summary>
		/// Ocurre cuando tiene un nuevo child
		/// </summary>
		public event EventHandler<IScreen> GotChild;
		/// <summary>
		/// Ocurre cuando es terminado
		/// </summary>
		public event EventHandler<IScreen> Terminated;

		#endregion

		#region ctor

		/// <summary>
		/// </summary>
		public ScreenThread ()
		{
			_invocationStack = new List<IScreen> ();
			_options = new List<ScreenStackOptions> ();
		}

		#endregion

		#region Subclasses

		/// <summary>
		/// Options for each screen in the stack
		/// </summary>
		public struct ScreenStackOptions
		{
			/// <summary>
			/// Determina si la pantalla anterior debe dibujarse
			/// </summary>
			public bool DibujaBase;

			/// <summary>
			/// Determina si la pantalla anterior debe actualizarse
			/// </summary>
			public bool ActualizaBase;

			#region Constantes

			/// <summary>
			/// Devuelve el <see cref="ScreenStackOptions"/> predeterminado
			/// </summary>
			public readonly static ScreenStackOptions Default;

			/// <summary>
			/// Tipo diálogo con base frozen
			/// </summary>
			public readonly static ScreenStackOptions Dialog;

			/// <summary>
			/// Tipo diálogoc on base activa
			/// </summary>
			public readonly static ScreenStackOptions DialogLive;

			#endregion

			static ScreenStackOptions ()
			{
				Default = new ScreenStackOptions
				{
					DibujaBase = false,
					ActualizaBase = false
				};
				Dialog = new ScreenStackOptions
				{
					DibujaBase = true,
					ActualizaBase = false
				};
				DialogLive = new ScreenStackOptions
				{
					DibujaBase = true,
					ActualizaBase = true
				};
			}
		}

		#endregion
	}
}