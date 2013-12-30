using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.ui
{
	public class IdlePanelMediator : Mediator
	{
		[Inject]
		public IdlePanelView view{ get; set; }

		[Inject]
		public GameStartSignal gameStartSignal{ get; set; }

		[Inject]
		public LevelStartSignal levelStartSignal{ get; set; }

		public override void OnRegister ()
		{
			view.Init ();
			view.proceedSignal.AddListener (onProceed);
		}

		public override void OnRemove ()
		{
			view.proceedSignal.RemoveListener (onProceed);
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
	}
}

