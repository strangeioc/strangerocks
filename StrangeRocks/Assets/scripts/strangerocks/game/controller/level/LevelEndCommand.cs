//Executes when the user clears a level. Increments the game level.

using System;
using strange.extensions.command.impl;

namespace strange.examples.strangerocks.game
{
	public class LevelEndCommand : Command
	{
		[Inject]
		public IGameModel gameModel{ get; set; }

		[Inject]
		public UpdateLevelSignal updateLevelSignal{get;set;}

		public override void Execute ()
		{
			if (gameModel.levelInProgress)
			{
				gameModel.levelInProgress = false;
				gameModel.level++;
				updateLevelSignal.Dispatch (gameModel.level);
			}
		}
	}
}

