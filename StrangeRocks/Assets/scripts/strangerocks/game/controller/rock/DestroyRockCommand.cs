using System;
using strange.extensions.command.impl;
using UnityEngine;
using System.Collections;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class DestroyRockCommand : Command
	{
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		[Inject]
		public RockView rockView{ get; set; }

		[Inject]
		public bool isPointEarning{ get; set; }

		[Inject]
		public IGameModel gameModel{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		[Inject]
		public CreateRockSignal createRockSignal{ get; set; }

		[Inject]
		public LevelEndSignal levelEndSignal{ get; set; }

		[Inject]
		public IRoutineRunner routineRunner{ get; set; }

		//We're drawing instances from a pool, instead of wasting our resources.
		[Inject(GameElement.ROCK_POOL)]
		public IPool<GameObject> pool{ get; set; }

		private static Vector3 PARKED_POS = new Vector3(1000f, 0f, 1000f);

		public override void Execute ()
		{
			if (isPointEarning)
			{
				int level = rockView.level;

				gameModel.score += 10 * level;
				updateScoreSignal.Dispatch (gameModel.score);

				if (level < 3)
				{
					for (int a = 0; a < level + 1; a++)
					{
						createRockSignal.Dispatch (level + 1, rockView.transform.localPosition);
					}
				}
			}

			//We're pooling instances, not actually destroying them,
			//So reset the instances to an appropriate state for reuse...
			rockView.rigidbody.velocity = Vector3.zero;
			rockView.rigidbody.angularVelocity = Vector3.zero;
			rockView.gameObject.SetActive (false);

			//...and store them offscreen
			rockView.transform.localPosition = PARKED_POS;
			pool.ReturnInstance (rockView.gameObject);

			if (isPointEarning)
			{
				Retain ();
				routineRunner.StartCoroutine (checkRocks ());
			}
		}

		public IEnumerator checkRocks()
		{
			yield return null;

			RockView[] rocks = gameField.GetComponentsInChildren<RockView> ();
			bool levelCleared = true;
			foreach (RockView rock in rocks)
			{
				if (rock.gameObject.activeSelf)
				{
					levelCleared = false;
				}
			}
			if (levelCleared)
			{
				levelEndSignal.Dispatch ();
			}

			Release ();
		}
	}
}

