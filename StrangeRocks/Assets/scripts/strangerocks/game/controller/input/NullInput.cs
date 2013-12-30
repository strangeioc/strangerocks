//For the moment, user input comes via KeyboardInput or the OnscreenControlsView.
//OnscreenControlsView is a View, rather than an injectable, instantiable
//Class, so when we use it, IInput gets mapped to this class to fulfill the injection
//requirement.

//A different solution might be to work up some logic in your Context that avoids the
//IInput injection. I prefer this route, as it guarantees all my dependencies will
//be satisfied.

using System;

namespace strange.examples.strangerocks.game
{
	public class NullInput : IInput
	{
	}
}

