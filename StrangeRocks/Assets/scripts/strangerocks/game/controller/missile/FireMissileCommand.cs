//Perhaps should be called CreateMissileCommand for parity with, e.g., CreateEnemyCommand

//Newly created Missiles get pulled from their pool and placed at the given position.
//(See DestroyMissileCommand for the bit where they're returned to their pool)

//Note that we have TWO pools here...one for the player's missiles. One for the
//Enemy missiles.

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class FireMissileCommand : Command
	{
		//The GameObject that spawned the missile (could be player or enemy)
		[Inject]
		public GameObject shipGO{ get; set; }

		//An identifier of MISSILE_POOL (player) or ENEMY_MISSILE_POOL.
		[Inject]
		public GameElement id{ get; set; }

		//The parent GameObject for the game
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		//We're drawing instances from pools, instead of wasting our resources.
		//This one is for the player's missiles
		[Inject(GameElement.MISSILE_POOL)]
		public IPool<GameObject> playerMissilePool{ get; set; }

		//This one is for the enemy's missiles
		[Inject(GameElement.ENEMY_MISSILE_POOL)]
		public IPool<GameObject> enemyMissilePool{ get; set; }
		 
		public override void Execute ()
		{
			//Fetch the missile from the appropriate pool
			GameObject missileGO = (id == GameElement.ENEMY_MISSILE_POOL) ? enemyMissilePool.GetInstance() : playerMissilePool.GetInstance();

			//place it
			missileGO.transform.localPosition = shipGO.transform.localPosition;
			missileGO.transform.parent = gameField.transform;

			//Sort out the rotation
			if (id == GameElement.ENEMY_MISSILE_POOL)
			{
				Vector3 euler = new Vector3 (0f, UnityEngine.Random.Range (0f, 360f), 0f);
				Quaternion q = Quaternion.Euler (euler);
				missileGO.transform.localRotation = q;
			} else
			{
				missileGO.transform.localRotation = shipGO.transform.localRotation;
			}

			missileGO.SetActive (true);

			//Send it out
			missileGO.rigidbody.AddRelativeForce(Vector3.right * 1000f);
		}
	}
}

