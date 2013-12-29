using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class EndGameCommand : Command
	{

		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		public override void Execute ()
		{
			UnityEngine.Object.Destroy (gameField.GetComponent<EnemySpawner> ());
		}
	}
}

