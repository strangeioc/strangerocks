//Mediators provide a buffer between Views and the rest of the app.
//THIS IS A REALLY GOOD THING. READ ABOUT IT HERE:
//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator

//This mediates between the app and the MissileExplosionView.
//When a MissileExplosion compeletes, we return its instance to the pool.

using System;
using strange.extensions.mediation.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class MissileExplosionMediator : Mediator
	{
		[Inject]
		public MissileExplosionView view { get; set; }

		[Inject(GameElement.MISSILE_EXPLOSION_POOL)]
		public IPool<GameObject> pool { get; set; }

		public override void OnRegister ()
		{
			view.animationCompleteSignal.AddListener (onComplete);
		}

		private void onComplete()
		{
			//We're pooling instances, not actually destroying them,
			//So reset the instances to an appropriate state for reuse...
			view.resetAnimation ();
			rigidbody.velocity = Vector3.zero;
			//...and store them offscreen
			transform.localPosition = new Vector3 (1000f,0f,1000f);
			gameObject.SetActive (false);
			pool.ReturnInstance (gameObject);
		}
	}
}

