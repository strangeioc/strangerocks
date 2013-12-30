//An Enemy can be destroyed in one of three ways:
//1. The player's missile can strike it, earning the player points.
//2. The Enemy can fly offscreen.
//3. The Enemy may simply be cleaned up if left behind at the end of a level.

//We're using pooling, so Enemies are never really "destroyed". We just move
//them offscreen and reset them until we need one again. This is more memory
//and performance friendly than the constant creation/destruction of Objects.

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.pool.api;

namespace strange.examples.strangerocks.game
{
	public class DestroyEnemyCommand : Command
	{
		//The pool to which we return the enemies
		[Inject(GameElement.ENEMY_POOL)]
		public IPool<GameObject> pool{ get; set; }

		//Keeper of score, level, etc.
		[Inject]
		public IGameModel gameModel{ get; set; }

		//The specific enemy being destroyed
		[Inject]
		public EnemyView enemyView{get;set;}

		//True if this destruction earns the player points (False if it flew offscreen
		//or was cleaned up at end of level)
		[Inject]
		public bool isPointEarning{ get; set; }
		
		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }
		
		//An offscreen location to place the recycled Enemies.
		//Arguably this value should be in a config somewhere.
		private static Vector3 PARKED_POS = new Vector3(1000f, 0f, 1000f);

		public override void Execute ()
		{
			if (isPointEarning)
			{
				//Give us some points
				int level = enemyView.level;

				//NOTE: arguably all the point-earning from destroying Rocks and Enemies
				//should be offloaded to a set of ScoreCommands. Certainly in a more complex game,
				//You'd do yourself a favor by centralizing the tabulation of scores.
				gameModel.score += 100 * level;
				updateScoreSignal.Dispatch (gameModel.score);
			}

			//We're pooling instances, not actually destroying them,
			//So reset the instances to an appropriate state for reuse...
			enemyView.rigidbody.velocity = Vector3.zero;
			enemyView.gameObject.SetActive (false);

			//...store them offscreen...
			enemyView.transform.localPosition = PARKED_POS;

			//...and RETURN THEM TO THE POOL!
			pool.ReturnInstance (enemyView.gameObject);
		}
	}
}

