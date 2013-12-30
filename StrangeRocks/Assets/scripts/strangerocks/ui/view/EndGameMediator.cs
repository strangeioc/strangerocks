//The panels are largely very simple.
//The Mediators listen to and dispatch Signals.
//Some signals set values on the Views.
//The Views themselves expose themselves to the Mediators on one hand
//And to the Unity Editor on the other.

using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.ui
{
	public class EndGameMediator : Mediator
	{
		[Inject]
		public EndGamePanelView view{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		[Inject]
		public GameEndSignal gameEndSignal{ get; set; }

		[Inject]
		public GameStartSignal gameStartSignal{ get; set; }

		public override void OnRegister ()
		{
			view.proceedSignal.AddListener (onProceed);

			gameEndSignal.AddListener (show);
			updateScoreSignal.AddListener (onScoreUpdate);

			view.Init ();
			hide ();
		}

		public override void OnRemove ()
		{
			view.proceedSignal.RemoveListener (onProceed);

			gameEndSignal.RemoveListener (show);
			updateScoreSignal.RemoveListener (onScoreUpdate);
		}

		private void show()
		{
			gameObject.SetActive (true);
		}

		private void hide()
		{
			gameObject.SetActive (false);
		}

		private void onProceed()
		{
			hide ();
			gameStartSignal.Dispatch ();
		}

		private void onScoreUpdate(int value)
		{
			view.SetScore (value);
		}
	}
}

