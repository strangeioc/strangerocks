using System;
using UnityEngine;
using strange.extensions.mediation.impl;
using System.Collections;

namespace strange.examples.strangerocks.game
{

	public class EnemySpawner : View
	{
		[Inject]
		public CreateEnemySignal createEnemySignal{ get; set; }

		[Inject]
		public IScreenUtil screenUtil{ get; set; }

		public float minSpawnSeconds = 2f;
		public float maxSpawnSeconds = 5f;

		protected override void Start ()
		{
			base.Start ();
			StartCoroutine (spawn ());
		}

		IEnumerator spawn()
		{
			while (true)
			{
				yield return new WaitForSeconds (UnityEngine.Random.Range(minSpawnSeconds, maxSpawnSeconds));
				Vector3 startPos = screenUtil.RandomPositionOnLeft ();
				createEnemySignal.Dispatch (UnityEngine.Random.Range(1, 3), startPos);
			}
		}
	}
}

