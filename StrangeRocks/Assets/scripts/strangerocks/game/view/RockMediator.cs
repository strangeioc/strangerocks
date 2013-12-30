//Mediators provide a buffer between Views and the rest of the app.
//THIS IS A REALLY GOOD THING. READ ABOUT IT HERE:
//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator

//This mediates between the app and the RockView.

using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.game
{
	public class RockMediator : Mediator
	{
		[Inject]
		public RockView view { get; set; }

		public override void OnRegister ()
		{
			//No-op...rocks are pretty dumb.
		}
	}
}

