//While this Context doesn't actually need a startup Command
//I almost always leave one here...just in case.
//In any sufficiently complex project, there's pretty much
//always **something** that needs to happen on startup.

using System;
using strange.extensions.command.impl;

namespace strange.examples.strangerocks.ui
{
	public class UIStartCommand : Command
	{
		public override void Execute ()
		{

		}
	}
}

