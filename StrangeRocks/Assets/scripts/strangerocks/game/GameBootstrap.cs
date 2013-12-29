using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace strange.examples.strangerocks.game
{
	public class GameBootstrap : ContextView
	{

		// Initialize your game context
		void Start ()
		{
			context = new GameContext (this);
		}
	}
}
