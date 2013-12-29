using System;
using strange.extensions.context.impl;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace strange.examples.strangerocks
{
	public class SignalContext : MVCSContext
	{
		public SignalContext (MonoBehaviour contextView) : base(contextView)
		{
		}

		protected override void addCoreComponents ()
		{
			base.addCoreComponents ();
			injectionBinder.Unbind<ICommandBinder> ();
			injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
		}

		public override void Launch ()
		{
			base.Launch ();
			StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>(); 
			startSignal.Dispatch();
		}

		protected override void mapBindings ()
		{
			base.mapBindings ();
			implicitBinder.ScanForAnnotatedClasses (new string[]{"strange.examples.strangerocks"});
		}
	}
}

