//This Command starts a service that spawns enemy ships at irregular intervals.

using System;
using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.command.impl;

namespace strange.examples.strangerocks.game
{
	public class CreateEnemySpawnerCommand : Command
	{

		[Inject]
		public ISpawner spawner{ get; set; }

		public override void Execute ()
		{
			spawner.Start ();
		}
	}
}

