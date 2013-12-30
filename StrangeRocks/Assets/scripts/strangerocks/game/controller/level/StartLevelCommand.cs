//Kick off the level

using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class StartLevelCommand : Command
	{
		[Inject]
		public IGameModel gameModel{ get; set; }

		[Inject]
		public LevelStartedSignal levelStartedSignal{ get; set; }

		[Inject]
		public SetupLevelSignal setupLevelSignal{ get; set; }

		[Inject]
		public UpdateLivesSignal updateLivesSignal{ get; set; }

		public override void Execute ()
		{
			setupLevelSignal.Dispatch ();
			gameModel.levelInProgress = true;
			levelStartedSignal.Dispatch ();
			updateLivesSignal.Dispatch (gameModel.lives);
		}
	}
}

