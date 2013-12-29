using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.ui
{
	public class OnscreenControlsMediator : Mediator
	{
		[Inject]
		public OnscreenControlsView view{ get; set; }

		[Inject]
		public GameInputSignal gameInputSignal{ get; set; }

		[Inject]
		public LevelEndSignal levelEndSignal{ get; set; }

		[Inject]
		public LevelStartSignal levelStartSignal{ get; set; }

		[Inject]
		public GameEndSignal gameEndSignal{ get; set; }

		private bool firingInProgress = false;

		public override void OnRegister ()
		{
			levelEndSignal.AddListener (hide);
			levelStartSignal.AddListener (show);
			gameEndSignal.AddListener (hide);

			view.init ();
			hide ();
		}

		public override void OnRemove ()
		{
			levelEndSignal.RemoveListener (hide);
			levelStartSignal.RemoveListener (show);
			gameEndSignal.RemoveListener (hide);
		}

		private void show()
		{
			gameObject.SetActive (true);
		}

		private void hide()
		{
			gameObject.SetActive (false);
		}

		public void Update()
		{
			int input = view.input;

			//Only permit one firing per tap
			bool fireButtonPressed = (input & GameInputEvent.FIRE) > 0;
			if (fireButtonPressed)
			{
				if (firingInProgress)
					input ^= GameInputEvent.FIRE;
				firingInProgress = true;
			}
			else if (!fireButtonPressed && firingInProgress)
			{
				firingInProgress = false;
			}

			gameInputSignal.Dispatch (input);
		}
	}
}

