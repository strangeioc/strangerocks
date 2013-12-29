//Write in all the Cross-Context bindings

using System;
using UnityEngine;
using strange.extensions.context.impl;
using strange.examples.strangerocks.game;

namespace strange.examples.strangerocks.main
{
	public class MainContext : SignalContext
	{
		public MainContext (MonoBehaviour contextView) : base (contextView)
		{
		}

		protected override void mapBindings ()
		{
			base.mapBindings ();

			if (Context.firstContext == this)
			{
				injectionBinder.Bind<IGameModel> ().To<GameModel> ().ToSingleton().CrossContext();

				injectionBinder.Bind<GameStartSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<GameInputSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<GameEndSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<LevelStartSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<LevelEndSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<UpdateLevelSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<UpdateLivesSignal> ().ToSingleton ().CrossContext();
				injectionBinder.Bind<UpdateScoreSignal> ().ToSingleton ().CrossContext();
			}

			commandBinder.Bind<StartSignal> ()
				.To<MainStartupCommand> ();
		}
	}
}

