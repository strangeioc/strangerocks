using System;
using strange.extensions.command.impl;
using UnityEngine;
using System.Collections;

namespace strange.examples.strangerocks.game
{
	public class DestroyPlayerCommand : Command
	{
		[Inject]
		public ShipView shipView{ get; set; }

		[Inject]
		public bool isEndOfLevel{ get; set; }

		[Inject]
		public IGameModel gameModel { get; set; }

		[Inject]
		public GameEndSignal gameEndSignal { get; set; }

		[Inject]
		public CreatePlayerSignal createPlayerSignal { get; set; }

		[Inject]
		public UpdateLivesSignal updateLivesSignal { get; set; }

		[Inject]
		public IRoutineRunner routineRunner { get; set; }

		public override void Execute ()
		{
			if (!gameModel.levelInProgress)
			{
				return;
			}

			if (!isEndOfLevel)
			{
				gameModel.levelInProgress = false;
				gameModel.lives--;
				updateLivesSignal.Dispatch (gameModel.lives);

				GameObject explosionPrototype = Resources.Load<GameObject> ("player_explosion");
				explosionPrototype.transform.localScale = Vector3.one;

				GameObject explosionGO = GameObject.Instantiate (explosionPrototype) as GameObject;
				Vector3 pos = shipView.transform.localPosition;
				explosionGO.transform.localPosition = pos;
				explosionGO.rigidbody.velocity = shipView.rigidbody.velocity;
				explosionGO.transform.parent = shipView.transform.parent;



				if (gameModel.lives <= 0)
				{
					gameEndSignal.Dispatch ();
				}
				else
				{
					Retain ();
					routineRunner.StartCoroutine (waitThenCreateShip ());
				}
			}
			GameObject.Destroy (shipView.gameObject);
			if (injectionBinder.GetBinding<ShipView> (GameElement.PLAYER_SHIP) != null)
				injectionBinder.Unbind<ShipView> (GameElement.PLAYER_SHIP);
		}

		private IEnumerator waitThenCreateShip()
		{
			yield return new WaitForSeconds (2f);
			createPlayerSignal.Dispatch ();
			Release ();
		}
	}
}

