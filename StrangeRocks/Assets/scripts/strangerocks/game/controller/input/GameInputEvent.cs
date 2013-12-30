//Bitwise values for user input.
//In KeyboardInput and OnscreenControlsView, these values are combined
//bitwise to indicate the user's input.

using System;

namespace strange.examples.strangerocks
{
	public class GameInputEvent
	{
		public static int NONE = 0;
		public static int ROTATE_RIGHT = 1;
		public static int ROTATE_LEFT = 2;
		public static int THRUST = 4;
		public static int FIRE = 8;
	}
}

