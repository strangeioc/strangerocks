using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace strange.examples.strangerocks.ui
{
	public class UIBootstrap : ContextView
	{

		// Initialize your game context
		void Start ()
		{
			context = new UIContext (this);
		}
	}
}
