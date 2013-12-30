//Newly created Enemies get pulled from their pool and placed at the given position.
//(See DestroyEnemyCommand for the bit where they're returned to their pool)

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class CreateEnemyCommand : Command
	{
		//The named GameObject that parents the rest of the game area
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		//The pool from which we draw Enemies (see also the GameContext's use of ResourceInstanceProvider).
		[Inject(GameElement.ENEMY_POOL)]
		public IPool<GameObject> pool{ get; set; }

		//Higher level Enemies are smaller, faster, fire more often, and are worth more points.
		[Inject]
		public int level{ get; set; }

		//The position to place the blighter.
		[Inject]
		public Vector3 pos{ get; set; }

		public override void Execute ()
		{
			GameObject enemyGO = pool.GetInstance ();
			enemyGO.transform.localPosition = pos;
			enemyGO.transform.parent = gameField.transform;

			enemyGO.SetActive (true);
			enemyGO.GetComponent<EnemyView> ().Init(level);
		}
	}
}

