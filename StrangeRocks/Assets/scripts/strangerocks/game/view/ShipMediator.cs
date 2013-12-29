using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class ShipMediator : Mediator
	{
		//View
		[Inject]
		public ShipView view { get; set; }

		//Signals
		[Inject]
		public GameInputSignal gameInputSignal{ get; set; }

		[Inject]
		public FireMissileSignal fireMissileSignal{ get; set; }

		[Inject]
		public DestroyPlayerSignal destroyPlayerSignal{ get; set; }

		public override void OnRegister ()
		{
			view.collisionSignal.AddListener (onCollision);
			gameInputSignal.AddListener (onGameInput);
		}

		public override void OnRemove ()
		{
			view.collisionSignal.RemoveListener (onCollision);
			gameInputSignal.RemoveListener (onGameInput);
		}

		private void onGameInput(int input)
		{
			view.SetAction (input);

			if ((input & GameInputEvent.FIRE) > 0)
			{
				fireMissileSignal.Dispatch (gameObject, GameElement.MISSILE_POOL);
			}
		}

		private void onCollision()
		{
			destroyPlayerSignal.Dispatch (view, false);
		}
	}
}

