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

