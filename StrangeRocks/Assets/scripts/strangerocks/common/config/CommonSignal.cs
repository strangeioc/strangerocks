//This file contains all the signals that are dispatched between Contexts

using System;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks
{
	public class StartSignal : Signal{}

	//Input
	public class GameInputSignal : Signal<int>{};

	//Game
	public class GameStartSignal : Signal{}
	public class GameEndSignal : Signal{}
	public class LevelStartSignal : Signal{}
	public class LevelEndSignal : Signal{}
	public class UpdateLivesSignal : Signal<int>{}
	public class UpdateScoreSignal : Signal<int>{}
	public class UpdateLevelSignal : Signal<int>{}
}

