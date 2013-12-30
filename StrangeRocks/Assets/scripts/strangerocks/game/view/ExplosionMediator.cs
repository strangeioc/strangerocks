//Mediators provide a buffer between Views and the rest of the app.
//THIS IS A REALLY GOOD THING. READ ABOUT IT HERE:
//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator

//This mediates between the app and the ExplosionView.

//Note that we're not pooling ShipView explosions (nor ShipViews), since they
//are relatively rare in the game.

using System;
using strange.extensions.mediation.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class ExplosionMediator : Mediator
	{
		[Inject]
		public ExplosionView view { get; set; }

		public override void OnRegister ()
		{
			view.animationCompleteSignal.AddListener (onComplete);
		}

		private void onComplete()
		{
			//This default mediator just deletes the view
			GameObject.Destroy (gameObject);
		}
	}
}

