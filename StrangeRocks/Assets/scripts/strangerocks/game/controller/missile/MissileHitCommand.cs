//When a MissileView reports a collission, that event is forwarded here
//to decide what to do about it (see MissileView and MissileMediator).

//The missile may hit a Rock, an Enemy or another missile.

//Player destruction is handled a little differently using any trigger that
//strikes the ShipView, which may include an enemy missile. (See ShipView for details)

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class MissileHitCommand : Command
	{
		//The missile involved in a collision
		[Inject]
		public MissileView missileView{ get; set; }

		//The other GameObject involved in the collision
		[Inject]
		public GameObject contact{ get; set; }

		//A pool for creation of explosion effects
		[Inject(GameElement.MISSILE_EXPLOSION_POOL)]
		public IPool<GameObject> pool{ get; set; }

		[Inject]
		public DestroyRockSignal destroyRockSignal{ get; set; }

		[Inject]
		public DestroyEnemySignal destroyEnemySignal{ get; set; }

		[Inject]
		public DestroyMissileSignal destroyMissileSignal{ get; set; }


		public override void Execute ()
		{
			//Set off an explosion, drawn from the pool
			GameObject explosionGO = pool.GetInstance ();
			explosionGO.SetActive (true);
			Vector3 pos = missileView.transform.localPosition;
			explosionGO.transform.localPosition = pos;
			explosionGO.transform.parent = missileView.transform.parent;

			//Destroy the missile
			GameElement id = missileView.gameObject.name.IndexOf ("enemy") > -1 ? GameElement.ENEMY_MISSILE_POOL : GameElement.MISSILE_POOL;
			destroyMissileSignal.Dispatch (missileView, id);

			//WHAT DID WE HIT?
			//If a rock...
			RockView rockView = contact.GetComponent<RockView> ();
			if (rockView != null)
			{
				explosionGO.rigidbody.velocity = rockView.rigidbody.velocity;
				destroyRockSignal.Dispatch (rockView, true);
			}

			//...enemy...
			EnemyView enemyView = contact.GetComponent<EnemyView> ();
			if (enemyView != null)
			{
				explosionGO.rigidbody.velocity = enemyView.rigidbody.velocity;
				destroyEnemySignal.Dispatch (enemyView, true);
			}

			//...another missile.
			MissileView otherMissileView = contact.GetComponent<MissileView> ();
			if (otherMissileView != null)
			{
				GameElement otherID = (id == GameElement.ENEMY_MISSILE_POOL) ? GameElement.MISSILE_POOL : GameElement.ENEMY_MISSILE_POOL;
				destroyMissileSignal.Dispatch (otherMissileView, otherID);
			}
		}
	}
}

