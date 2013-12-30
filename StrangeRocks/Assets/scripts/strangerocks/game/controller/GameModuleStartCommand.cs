//When we start the module, take care of any startup requriements.

//This version is when GameContext runs as part of an integrated, multi-Context app.
//It just instantiates the IME.

using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;

namespace strange.examples.strangerocks.game
{
	public class GameModuleStartCommand : Command
	{

		[Inject]	//This injection simply instantiates the game input
		public IInput input{ get; set; }


		public override void Execute ()
		{
			//No-op.
		}
	}
}

