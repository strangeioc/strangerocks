using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class DestroyEnemyCommand : Command
	{

		[Inject(GameElement.ENEMY_POOL)]
		public IPool<GameObject> pool{ get; set; }

		[Inject]
		public IGameModel gameModel{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		[Inject]
		public EnemyView enemyView{get;set;}

		[Inject]
		public bool isPointEarning{ get; set; }

		private static Vector3 PARKED_POS = new Vector3(1000f, 0f, 1000f);

		public override void Execute ()
		{
			if (isPointEarning)
			{
				int level = enemyView.level;

				gameModel.score += 100 * level;
				updateScoreSignal.Dispatch (gameModel.score);
			}

			//We're pooling instances, not actually destroying them,
			//So reset the instances to an appropriate state for reuse...
			enemyView.rigidbody.velocity = Vector3.zero;
			enemyView.gameObject.SetActive (false);

			//...and store them offscreen
			enemyView.transform.localPosition = PARKED_POS;
			pool.ReturnInstance (enemyView.gameObject);
		}
	}
}

