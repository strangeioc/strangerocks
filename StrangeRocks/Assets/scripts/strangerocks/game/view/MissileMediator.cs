//Mediators provide a buffer between Views and the rest of the app.
//THIS IS A REALLY GOOD THING. READ ABOUT IT HERE:
//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator

//This mediates between the app and the MissileView.

using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class MissileMediator : Mediator
	{
		[Inject]
		public MissileView view { get; set; }

		[Inject]
		public MissileHitSignal missileHitSignal{ get; set; }

		[Inject]
		public DestroyMissileSignal destroyMissileSignal{ get; set; }

		public override void OnRegister ()
		{
			view.contactSignal.AddListener (onContact);
			view.exitScreenSignal.AddListener (onExitScreen);
		}

		public override void OnRemove ()
		{
			view.contactSignal.RemoveListener (onContact);
			view.exitScreenSignal.RemoveListener (onExitScreen);
		}

		private void onContact(GameObject go)
		{
			missileHitSignal.Dispatch (view, go);
		}

		private void onExitScreen()
		{
			destroyMissileSignal.Dispatch (view, GameElement.MISSILE_POOL);
		}
	}
}

