//Since we have an injectable RoutineRunner, we can move
//some behaviours that should be Controllers out of the classification of View.

//Here's an example. This mechanism simply waits a few seconds, then spawns a new enemy.
//Since it's pure controller, there's no good reason for it to be taking up space/
//cycles in the visual portion of the game. So we simply write it as a Controller
//and inject in the MonoBehaviour bit.

using System;
using strange.extensions.mediation.impl;
using System.Collections;
using UnityEngine;

namespace strange.examples.strangerocks.game
{

	public class EnemySpawner : ISpawner
	{
		[Inject]
		public CreateEnemySignal createEnemySignal{ get; set; }

		[Inject]
		public IScreenUtil screenUtil{ get; set; }

		[Inject]
		public IRoutineRunner routineRunner{ get; set; }

		[Inject]
		public IGameConfig gameConfig{ get; set; }

		//Arguably these should be in a gameConfig somewhere
		private float minSpawnSeconds = 2f;
		private float maxSpawnSeconds = 5f;

		private bool running = false;

		//PostConstruct methods fire automatically after Construction
		//and after all injections are satisfied. It's a safe place
		//to do things you'd usually sonsider doing in the Constructor.
		[PostConstruct]
		public void PostConstruct()
		{
			minSpawnSeconds = gameConfig.enemySpawnSecondsMin;
			maxSpawnSeconds = gameConfig.enemySpawnSecondsMax;
		}

		public void Start ()
		{
			running = true;
			routineRunner.StartCoroutine (spawn ());
		}

		public void Stop()
		{
			running = false;
		}

		IEnumerator spawn()
		{
			while (running)
			{
				yield return new WaitForSeconds (UnityEngine.Random.Range(minSpawnSeconds, maxSpawnSeconds));
				Vector3 startPos = screenUtil.RandomPositionOnLeft ();
				createEnemySignal.Dispatch (UnityEngine.Random.Range(1, 3), startPos);
			}
		}
	}
}

