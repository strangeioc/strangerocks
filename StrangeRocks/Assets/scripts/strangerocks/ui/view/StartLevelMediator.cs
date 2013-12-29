using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.ui
{
	public class StartLevelPanelMediator : Mediator
	{
		[Inject]
		public StartLevelPanelView view{ get; set; }

		[Inject]
		public LevelStartSignal levelStartSignal{ get; set; }

		[Inject]
		public LevelEndSignal levelEndSignal{ get; set; }

		[Inject]
		public UpdateLevelSignal updateLevelSignal{ get; set; }

		[Inject]
		public GameStartSignal gameStartSignal{ get; set; }

		public override void OnRegister ()
		{
			view.proceedSignal.AddListener (onProceed);

			levelEndSignal.AddListener (show);
			gameStartSignal.AddListener (show);
			updateLevelSignal.AddListener (onLevelUpdate);

			view.init ();
			hide ();
		}

		public override void OnRemove ()
		{
			view.proceedSignal.RemoveListener (onProceed);

			levelEndSignal.RemoveListener (show);
			gameStartSignal.RemoveListener (show);
			updateLevelSignal.RemoveListener (onLevelUpdate);
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
			levelStartSignal.Dispatch ();
		}

		private void onLevelUpdate(int value)
		{
			view.SetLevel (value);
		}
	}
}

