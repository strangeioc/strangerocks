using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace strange.examples.strangerocks.main
{
	public class MainBootstrap : ContextView
	{

		// Initialize your game context
		void Start ()
		{
			context = new MainContext (this);
		}
	}
}
