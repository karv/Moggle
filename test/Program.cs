﻿#region Using Statements
using System;

#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;





#elif __IOS__
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
#endregion

namespace Test
{
#if __IOS__
	[Register("AppDelegate")]
	class Program : UIApplicationDelegate
	




#else
	public static class Program
#endif
	{
		static Game1 game;

		internal static void RunGame ()
		{
			game = new Game1 ();
			game.Run ();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
#if !MONOMAC && !__IOS__
		[STAThread]
#endif
		public static void Main (string [] args)
		{
			Console.WriteLine ("Running...");
#if MONOMAC
			NSApplication.Init ();

			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate();
				NSApplication.Main(args);
			}
#elif __IOS__
			UIApplication.Main(args, null, "AppDelegate");
#else
			RunGame ();
#endif
			Environment.Exit (0);
		}

#if __IOS__
		public override void FinishedLaunching(UIApplication app)
		{
			RunGame();
		}
#endif
	}

#if MONOMAC
	class AppDelegate : NSApplicationDelegate
	{
		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs a) =>  {
				if (a.Name.StartsWith("MonoMac")) {
					return typeof(MonoMac.AppKit.AppKitFramework).Assembly;
				}
				return null;
			};
			Program.RunGame();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}  
#endif
}