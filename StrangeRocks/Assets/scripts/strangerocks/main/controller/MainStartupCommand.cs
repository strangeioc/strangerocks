using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;

namespace strange.examples.strangerocks.main
{
	public class MainStartupCommand : Command
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView { get; set; }


		override public void Execute ()
		{
			Application.LoadLevelAdditive ("ui");
			Application.LoadLevelAdditive ("game");
		}
	}
}

