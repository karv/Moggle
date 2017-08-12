using System;
using NUnit.Framework;
using Moggle.Screens;
using OpenTK.Input;
namespace Civo
{
	[TestFixture]
	public class Threads
	{
		[Test]
		public void IteratingThreadsGoodActiveThread()
		{
			var game = new Game();
			game.Initialized += delegate
			{
				var _scr1 = new Screen(game);
				var _scr2 = new Screen(game);
				var thr = game.Threads.CreateAndSwitch();
				thr.Stack(_scr1);
				Assert.That(game.Threads.ActiveThread.TopmostScreen, Is.EqualTo(_scr1));
				thr.Stack(_scr2);
				Assert.That(game.Threads.ActiveThread.TopmostScreen, Is.EqualTo(_scr2));
				thr.TerminateLast();
				Assert.That(game.Threads.ActiveThread.TopmostScreen, Is.EqualTo(_scr1));
				Assert.Pass();
			};
			game.Run();
		}
	}
}
