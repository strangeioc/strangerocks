//Every context starts by attaching a ContextView to a GameObject.
//The main job of this ContextView is to instantiate the Context.
//Remember, if the GameObject is destroyed, the Context and all your
//bindings go with it.

//This ContextView holds the core game. It is capable of running standalone
//(run from game.unity), or as part of the integrated whole (run from main.unity).

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
