//Mediators provide a buffer between Views and the rest of the app.
//THIS IS A REALLY GOOD THING. READ ABOUT IT HERE:
//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator

//This mediates between the app and the EnemyMissileView.

using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class EnemyMissileMediator : Mediator
	{
		[Inject]
		public EnemyMissileView view { get; set; }

		[Inject]
		public DestroyMissileSignal destroyMissileSignal{ get; set; }

		public override void OnRegister ()
		{
			view.exitScreenSignal.AddListener (onExitScreen);
		}

		public override void OnRemove ()
		{
			view.exitScreenSignal.RemoveListener (onExitScreen);
		}

		private void onExitScreen()
		{
			destroyMissileSignal.Dispatch (view, GameElement.ENEMY_MISSILE_POOL);
		}
	}
}

