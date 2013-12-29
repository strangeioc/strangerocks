using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class GameStartCommand : Command
	{
		[Inject]
		public UpdateLevelSignal updateLevelSignal{ get; set; }

		[Inject]
		public GameStartedSignal gameStartedSignal{ get; set; }

		[Inject]
		public UpdateLivesSignal updateLivesSignal{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		[Inject]
		public IGameModel gameModel{ get; set; }

		public override void Execute ()
		{
			gameModel.maxLives = 3;
			gameModel.Reset ();

			updateLevelSignal.Dispatch (gameModel.level);
			updateLivesSignal.Dispatch (gameModel.lives);
			updateScoreSignal.Dispatch (gameModel.score);
			gameStartedSignal.Dispatch ();
		}
	}
}

