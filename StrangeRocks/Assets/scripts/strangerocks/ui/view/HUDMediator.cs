using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.ui
{
	public class HUDMediator : Mediator
	{
		[Inject]
		public HUDView view{ get; set; }

		[Inject]
		public UpdateScoreSignal updateScoreSignal{ get; set; }

		[Inject]
		public UpdateLevelSignal updateLevelSignal{ get; set; }

		[Inject]
		public UpdateLivesSignal updateLivesSignal{ get; set; }

		public override void OnRegister ()
		{
			updateScoreSignal.AddListener (onScoreUpdate);
			updateLevelSignal.AddListener (onLevelUpdate);
			updateLivesSignal.AddListener (onLivesUpdate);

			view.init ();
		}

		public override void OnRemove ()
		{
			updateScoreSignal.RemoveListener (onScoreUpdate);
			updateLevelSignal.RemoveListener (onLevelUpdate);
			updateLivesSignal.RemoveListener (onLivesUpdate);
		}

		private void onScoreUpdate(int value)
		{
			view.SetScore (value);
		}

		private void onLevelUpdate(int value)
		{
			view.SetLevel (value);
		}

		private void onLivesUpdate(int value)
		{
			view.SetLives (value);
		}
	}
}

