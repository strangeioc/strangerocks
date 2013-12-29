using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class CreateEnemyCommand : Command
	{
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		[Inject(GameElement.ENEMY_POOL)]
		public IPool<GameObject> pool{ get; set; }

		[Inject]
		public int level{ get; set; }

		[Inject]
		public Vector3 pos{ get; set; }

		public override void Execute ()
		{
			GameObject enemyGO = pool.GetInstance ();
			enemyGO.transform.localPosition = pos;
			enemyGO.transform.parent = gameField.transform;

			enemyGO.SetActive (true);
			enemyGO.GetComponent<EnemyView> ().init(level);
		}
	}
}

