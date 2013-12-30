using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks.ui
{
	public class IdlePanelView : View
	{
		[Inject]
		public IScreenUtil screenUtil{ get; set; }

		internal Signal proceedSignal = new Signal ();

		public ButtonView startButton;

		public ScreenAnchor horizontalAnchor = ScreenAnchor.CENTER_HORIZONTAL;
		public ScreenAnchor verticalAnchor = ScreenAnchor.CENTER_VERTICAL;

		internal void Init()
		{
			startButton.releaseSignal.AddListener (onStartClick);

			transform.localPosition = screenUtil.GetAnchorPosition (horizontalAnchor, verticalAnchor);
		}

		private void onStartClick()
		{
			proceedSignal.Dispatch ();
		}
	}
}

