using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class MissileHitCommand : Command
	{
		[Inject]
		public MissileView missileView{ get; set; }

		[Inject]
		public GameObject contact{ get; set; }

		[Inject]
		public IGameModel model{ get; set; }

		[Inject]
		public DestroyRockSignal destroyRockSignal{ get; set; }

		[Inject]
		public DestroyEnemySignal destroyEnemySignal{ get; set; }

		[Inject]
		public DestroyMissileSignal destroyMissileSignal{ get; set; }

		[Inject(GameElement.MISSILE_EXPLOSION_POOL)]
		public IPool<GameObject> pool{ get; set; }


		public override void Execute ()
		{
			GameObject explosionPrototype = Resources.Load<GameObject> ("missile_explosion");
			explosionPrototype.transform.localScale = Vector3.one;

			GameObject explosionGO = pool.GetInstance ();
			explosionGO.SetActive (true);
			Vector3 pos = missileView.transform.localPosition;
			explosionGO.transform.localPosition = pos;
			explosionGO.transform.parent = missileView.transform.parent;

			GameElement id = missileView.gameObject.name.IndexOf ("enemy") > -1 ? GameElement.ENEMY_MISSILE_POOL : GameElement.MISSILE_POOL;
			destroyMissileSignal.Dispatch (missileView, id);

			RockView rockView = contact.GetComponent<RockView> ();
			if (rockView != null)
			{
				explosionGO.rigidbody.velocity = rockView.rigidbody.velocity;
				destroyRockSignal.Dispatch (rockView, true);
			}

			EnemyView enemyView = contact.GetComponent<EnemyView> ();
			if (enemyView != null)
			{
				explosionGO.rigidbody.velocity = enemyView.rigidbody.velocity;
				destroyEnemySignal.Dispatch (enemyView, true);
			}

			MissileView otherMissileView = contact.GetComponent<MissileView> ();
			if (otherMissileView != null)
			{
				GameElement otherID = (id == GameElement.ENEMY_MISSILE_POOL) ? GameElement.MISSILE_POOL : GameElement.ENEMY_MISSILE_POOL;
				destroyMissileSignal.Dispatch (otherMissileView, otherID);
			}
		}
	}
}

