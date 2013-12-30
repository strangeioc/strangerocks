//Some capabilities are Commands/Models/Service but really require the office of a MonoBehaviour.

//A technique I use from time-to-time is this hybridized appraoch...basically nesting a
//MonoBehaviour (View) inside the other relevant file to highlight their direct relationship
//while "hiding" it from the rest of the app.

//This Command adds a View that spawns enemy ships at irregular intervals.

using System;
using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.command.impl;

namespace strange.examples.strangerocks.game
{
	public class CreateEnemySpawnerCommand : Command
	{

		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		public override void Execute ()
		{
			gameField.AddComponent<EnemySpawner> ();
		}
	}
}

