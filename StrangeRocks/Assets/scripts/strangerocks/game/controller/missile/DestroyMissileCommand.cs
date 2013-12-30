//A Missile can be destroyed in one of two ways:
//1. It can strike something (rock, enemy, player, another missile.
//2. It can fly offscreen.

//We're using pooling, so Missiles are never really "destroyed". We just move
//them offscreen and reset them until we need one again. This is more memory
//and performance friendly than the constant creation/destruction of Objects.

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;
using System.Collections;

namespace strange.examples.strangerocks.game
{
	public class DestroyMissileCommand : Command
	{
		//The missile being destroyed
		[Inject]
		public MissileView missileView{ get; set; }

		//An identifier of MISSILE_POOL (player) or ENEMY_MISSILE_POOL.
		[Inject]
		public GameElement id{ get; set; }

		//The player's missile pool
		[Inject(GameElement.MISSILE_POOL)]
		public IPool<GameObject> playerMissilePool{ get; set; }

		//The enemy's missile pool
		[Inject(GameElement.ENEMY_MISSILE_POOL)]
		public IPool<GameObject> enemyMissilePool{ get; set; }

		//A place offscreen to place the recycled GameObjects
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

