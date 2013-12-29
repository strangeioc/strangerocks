using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;
using System.Collections;

namespace strange.examples.strangerocks.game
{
	public class DestroyMissileCommand : Command
	{

		[Inject]
		public MissileView missileView{ get; set; }

		[Inject]
		public GameElement id{ get; set; }

		[Inject(GameElement.MISSILE_POOL)]
		public IPool<GameObject> playerMissilePool{ get; set; }

		[Inject(GameElement.ENEMY_MISSILE_POOL)]
		public IPool<GameObject> enemyMissilePool{ get; set; }

		private static Vector3 PARKED_POS = new Vector3(1000f, 0f, 1000f);

		public override void Execute ()
		{
			//We're pooling instances, not actually destroying them,
			//So reset the instances to an appropriate state for reuse...
			missileView.rigidbody.velocity = Vector3.zero;
			//...and store them offscreen
			missileView.transform.localPosition = PARKED_POS;

			if (id == GameElement.ENEMY_MISSILE_POOL)
				enemyMissilePool.ReturnInstance (missileView.gameObject);
			else if (id == GameElement.MISSILE_POOL)
				playerMissilePool.ReturnInstance (missileView.gameObject);
			else
				throw new Exception ("DestroyMissileCommand unrecognized pool id " + id);

			missileView.gameObject.SetActive (false);
		}
	}
}

