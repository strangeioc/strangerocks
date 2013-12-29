//Loads UI and Game levels
//Note the use of LoadLevelAdditive. This means we're ADDING
//to the scene, rather than destroying it. The advantage of this
//approach is that out Cross-Context bindings remain in place,
//which makes better use of Strange's Cross-Context capabilities.

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

