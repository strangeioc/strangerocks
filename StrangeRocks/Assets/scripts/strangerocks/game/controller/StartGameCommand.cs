//User has asked to Start a game. Let's Rock!

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
			//Set the max lives and zero out the gameModel
			gameModel.maxLives = 3;
			gameModel.Reset ();

			//Update all the model values
			updateLevelSignal.Dispatch (gameModel.level);
			updateLivesSignal.Dispatch (gameModel.lives);
			updateScoreSignal.Dispatch (gameModel.score);

			//Tell everyone who cares that we've started
			gameStartedSignal.Dispatch ();
		}
	}
}

