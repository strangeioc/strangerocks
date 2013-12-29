using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class FireMissileCommand : Command
	{
		[Inject]
		public GameObject shipGO{ get; set; }

		[Inject]
		public GameElement id{ get; set; }

		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		//We're drawing instances from a pool, instead of wasting our resources.
		[Inject(GameElement.MISSILE_POOL)]
		public IPool<GameObject> playerMissilePool{ get; set; }

		[Inject(GameElement.ENEMY_MISSILE_POOL)]
		public IPool<GameObject> enemyMissilePool{ get; set; }
		 
		public override void Execute ()
		{
			GameObject missileGO = (id == GameElement.ENEMY_MISSILE_POOL) ? enemyMissilePool.GetInstance() : playerMissilePool.GetInstance();

			missileGO.transform.localPosition = shipGO.transform.localPosition;
			missileGO.transform.parent = gameField.transform;

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
			missileGO.rigidbody.AddRelativeForce(Vector3.right * 1000f);
		}
	}
}

