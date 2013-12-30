//Destroy the player's ShipView. This can happen for two reasons:
//1. The player got struck by an object (missile, rock, enemy).
//2. The level/game ended, and we're simply cleaning up.

using System;
using strange.extensions.command.impl;
using UnityEngine;
using System.Collections;

namespace strange.examples.strangerocks.game
{
	public class DestroyPlayerCommand : Command
	{
		//Reference to the player's ship
		[Inject]
		public ShipView shipView{ get; set; }

		//Boolean to indicate whether this destruction is for cleanup
		[Inject]
		public bool isEndOfLevel{ get; set; }

		//Tracking of lives lost
		[Inject]
		public IGameModel gameModel { get; set; }

		[Inject]
		public GameEndSignal gameEndSignal { get; set; }

		[Inject]
		public CreatePlayerSignal createPlayerSignal { get; set; }

		[Inject]
		public UpdateLivesSignal updateLivesSignal { get; set; }

		//Need a delay between ship destruction and the next creation. Run a coroutine.
		[Inject]
		public IRoutineRunner routineRunner { get; set; }

		public override void Execute ()
		{
			//Gating boolean so we don't double-destroy the player.
			//This can happen because the ShipView is pretty dumb. It simply reports
			//that a collision occurred, so collision with multiple rocks/missiles
			//can over-report destruction.
			//Commands are the brains, so we let this Command (together with the model) decide whether
			//this collision represents a genuine ship destruction.
			if (!gameModel.levelInProgress)
			{
				return;
			}

			//Not isEndOfLevel means the ship was in fact destroyed, not just cleaned up
			if (!isEndOfLevel)
			{
				gameModel.levelInProgress = false;

				//Decrement lives
				gameModel.lives--;
				updateLivesSignal.Dispatch (gameModel.lives);

				//Blow up the ship!
				GameObject explosionPrototype = Resources.Load<GameObject> ("player_explosion");
				explosionPrototype.transform.localScale = Vector3.one;

				GameObject explosionGO = GameObject.Instantiate (explosionPrototype) as GameObject;
				Vector3 pos = shipView.transform.localPosition;
				explosionGO.transform.localPosition = pos;
				explosionGO.rigidbody.velocity = shipView.rigidbody.velocity;
				explosionGO.transform.parent = shipView.transform.parent;


				//Are we at the end of the game?
				if (gameModel.lives <= 0)
				{
					gameEndSignal.Dispatch ();
				}
				else
				{
					//If not, pause a couple seconds, then ask for a new ship.
					Retain ();
					routineRunner.StartCoroutine (waitThenCreateShip ());
				}
			}
			//Unbind the current instance. If we create another ship, the new instance will get
			//re-bound (see CreatePlayerCommand).
			GameObject.Destroy (shipView.gameObject);
			if (injectionBinder.GetBinding<ShipView> (GameElement.PLAYER_SHIP) != null)
				injectionBinder.Unbind<ShipView> (GameElement.PLAYER_SHIP);
		}

		//Wait a couple seconds, then request another ship
		private IEnumerator waitThenCreateShip()
		{
			yield return new WaitForSeconds (2f);
			createPlayerSignal.Dispatch ();
			Release ();
		}
	}
}

