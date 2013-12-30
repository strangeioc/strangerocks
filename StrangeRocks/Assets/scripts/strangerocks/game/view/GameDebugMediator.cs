//Mediators provide a buffer between Views and the rest of the app.
//THIS IS A REALLY GOOD THING. READ ABOUT IT HERE:
//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator

//This mediates between the app and the GameDebugView,
//receiving game signals that update the screen state,
//and sending signals that indicate the user's desire to
//start a game or level.

using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.game
{
	public class GameDebugMediator : Mediator
	{
		[Inject]
		public GameDebugView view{ get; set; }

		//Signals
		[Inject]
		public GameStartSignal gameStartSignal{ get; set; }

		[Inject]
		public GameEndSignal gameEndSignal{ get; set; }

		[Inject]
		public LevelStartSignal levelStartSignal{ get; set; }

		[Inject]
		public LevelStartedSignal levelStartedSignal{ get; set; }

		[Inject]
		public GameStartedSignal gameStartedSignal{ get; set; }

		[Inject]
		public UpdateLevelSignal updateLevelSignal{ get; set; }

		[Inject]
		public UpdateLivesSignal updateLivesSignal{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		public override void OnRegister ()
		{
			view.startGameSignal.AddListener (onStartGameClick);
			view.startLevelSignal.AddListener (onStartLevelClick);

			updateLevelSignal.AddListener (onLevelUpdate);
			updateLivesSignal.AddListener (onLivesUpdate);
			updateScoreSignal.AddListener (onScoreUpdate);
			gameStartedSignal.AddListener (onGameStarted);
			gameEndSignal.AddListener (onGameEnded);
			levelStartedSignal.AddListener (onLevelStarted);
		}

		public override void OnRemove ()
		{
			view.startGameSignal.RemoveListener (onStartGameClick);
			view.startLevelSignal.RemoveListener (onStartLevelClick);

			updateLevelSignal.RemoveListener (onLevelUpdate);
			updateLivesSignal.RemoveListener (onLivesUpdate);
			updateScoreSignal.RemoveListener (onScoreUpdate);
			gameStartedSignal.RemoveListener (onGameStarted);
			gameEndSignal.RemoveListener (onGameEnded);
			levelStartedSignal.RemoveListener (onLevelStarted);
		}

		private void onStartGameClick()
		{
			gameStartSignal.Dispatch ();
		}

		private void onGameStarted()
		{
			view.SetState (GameDebugView.ScreenState.START_LEVEL);
		}

		private void onGameEnded()
		{
			view.SetState (GameDebugView.ScreenState.END_GAME);
		}

		private void onStartLevelClick()
		{
			levelStartSignal.Dispatch ();
		}

		private void onLevelStarted()
		{
			view.SetState (GameDebugView.ScreenState.LEVEL_IN_PROGRESS);
		}

		private void onLevelUpdate(int value)
		{
			view.SetState (GameDebugView.ScreenState.START_LEVEL);
			view.SetLevel (value);
		}

		private void onScoreUpdate(int value)
		{
			view.SetScore (value);
		}

		private void onLivesUpdate(int value)
		{
			view.SetLives (value);
		}
	}
}

