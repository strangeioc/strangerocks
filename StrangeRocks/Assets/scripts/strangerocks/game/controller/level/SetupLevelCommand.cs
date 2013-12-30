//At the start of each level, place the player and the rocks

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;

namespace strange.examples.strangerocks.game
{
	public class SetupLevelCommand : Command
	{
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		[Inject]
		public IGameModel gameModel{ get; set; }

		[Inject]
		public CreatePlayerSignal createPlayerSignal{ get; set; }

		[Inject]
		public CreateRockSignal createRockSignal{ get; set; }

		public override void Execute ()
		{
			createPlayerSignal.Dispatch ();

			int rocks = gameModel.level + 2;
			for (int a = 0; a < rocks; a++)
			{
				float theta = UnityEngine.Random.Range (0, Mathf.PI * 2);
				float x = Mathf.Cos (theta) * 10f;
				float y = Mathf.Sin (theta) * 10f;
				Vector3 randomPos = new Vector3 (x, 0f, y);

				createRockSignal.Dispatch (1, randomPos);
			}
		}
	}
}

