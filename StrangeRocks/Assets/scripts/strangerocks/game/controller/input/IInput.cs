//An API-less interface.
//This simply functions as a "Marker" class for injecting the Input.
//Right now there's only KeyboardInput, so why bother?
//Well, tomorrow the boss may ask for JoystickInput, GameControllerInput
//AccelerometerInput...who knows? By presupposing an interface, we
//set ourselves up today for the minimum refactoring tomorrow.

using System;

namespace strange.examples.strangerocks.game
{
	public interface IInput
	{
	}
}

