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

		}
	}
}

