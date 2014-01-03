//A Rock is destroyed if it is struck by a player missile.
//It may also get removed during cleanup when re-starting the game.

//One could make a pretty good argument that this Command should in fact be broken into three:
//1. Tabulate points.
//2. Destroy the Rock.
//3. Determine if the level is complete.

//I chose not to do that in this case (a bit lazy, to be honest). If I did, it would be a fairly simple matter,
//and probably better, since I'd be doing the single-responsibility thing far more cleanly.
//In the GameContext, I'd re-write the binding something like this:

//Currently:
//commandBinder.Bind<DestroyRockSignal>().To<DestroyRockCommand>().Pooled();

//Rewritten:
//commandBinder.Bind<DestroyRockSignal>()
//		.To<ScoreByRockCommand>()
//		.To<DestroyRockCommand>()
//		.To<CheckLevelEndCommand>()
//		/InSequence()
//		.Pooled();

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

		//A reference to the specific rock
		[Inject]
		public RockView rockView{ get; set; }

		//Does this destruction earn the player points?
		[Inject]
		public bool isPointEarning{ get; set; }

		//For score-keeping
		[Inject]
		public IGameModel gameModel{ get; set; }

		//We run a brief coroutine after destruction to test whether all Rocks have been destroyed
		[Inject]
		public IRoutineRunner routineRunner{ get; set; }

		//We're drawing instances from a pool, instead of wasting our resources.
		[Inject(GameElement.ROCK_POOL)]
		public IPool<GameObject> pool{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		[Inject]
		public CreateRockSignal createRockSignal{ get; set; }

		[Inject]
		public LevelEndSignal levelEndSignal{ get; set; }

		[Inject]
		public IGameConfig gameConfig{ get; set; }

		private static Vector3 PARKED_POS = new Vector3(1000f, 0f, 1000f);

		public override void Execute ()
		{
			if (isPointEarning)
			{
				//NOTE: arguably all the point-earning from destroying Rocks and Enemies
				//should be offloaded to a set of ScoreCommands. Certainly in a more complex game,
				//You'd do yourself a favor by centralizing the tabulation of scores.
				int level = rockView.level;

				gameModel.score += gameConfig.baseRockScore * level;
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
				//If this was the player destroying a rock, pause...
				Retain ();
				routineRunner.StartCoroutine (checkRocks ());
			}
		}

		//...then test if we've destroyed them all.
		public IEnumerator checkRocks()
		{
			//A one-frame delay is necessary to ensure the gameField's view has cleaned up the destroyed rock.
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

