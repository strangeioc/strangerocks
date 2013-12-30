//At the end of the Game, do whatever cleanup is required.

using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class EndGameCommand : Command
	{

		[Inject]
		public ISpawner spawner{ get; set; }

		public override void Execute ()
		{
			spawner.Stop ();
		}
	}
}

