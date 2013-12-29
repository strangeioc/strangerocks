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

		}
	}
}

