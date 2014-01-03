//Newly created Rocks get pulled from their pool and placed at the given position.
//(See DestroyRockCommand for the bit where they're returned to their pool)

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class CreateRockCommand : Command
	{
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		//We're drawing instances from a pool, instead of wasting our resources.
		[Inject(GameElement.ROCK_POOL)]
		public IPool<GameObject> pool{ get; set; }

		//Higher level Rocks are smaller and earn the player more points
		[Inject]
		public int level{ get; set; }

		//The position to place the Rock
		[Inject]
		public Vector3 localPos{ get; set; }

		[Inject]
		public IGameConfig gameConfig{ get; set; }

		public override void Execute ()
		{
			//Draw an instance from the Pool
			GameObject rockGO = pool.GetInstance();
			rockGO.SetActive (true);

			//place it
			rockGO.transform.localPosition = localPos;
			rockGO.transform.localScale = Vector3.one / level;
			rockGO.layer = LayerMask.NameToLayer ("enemy");
			rockGO.GetComponent<RockView> ().level = level;

			//move it
			Vector3 expPt = rockGO.transform.localPosition;
			expPt.x += UnityEngine.Random.Range (2f, 4f);
			expPt.x *= (UnityEngine.Random.Range (0f, 1f) < .5f) ? -1f : 1f;
			expPt.z += UnityEngine.Random.Range (2f, 4f);
			expPt.z *= (UnityEngine.Random.Range (0f, 1f) < .5f) ? -1f : 1f;
			rockGO.rigidbody.AddExplosionForce (
				UnityEngine.Random.Range (gameConfig.rockExplosiveForceMin, gameConfig.rockExplosiveForceMax), 
				expPt,
				gameConfig.rockExplosiveRadius);

			rockGO.transform.parent = gameField.transform;

		}
	}
}

