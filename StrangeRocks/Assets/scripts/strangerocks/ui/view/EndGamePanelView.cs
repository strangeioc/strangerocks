using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks.ui
{
	public class EndGamePanelView : View
	{
		[Inject]
		public IScreenUtil screenUtil{ get; set; }

		public ButtonView startButton;
		public TextMesh score_field;
		public ScreenAnchor horizontalAnchor = ScreenAnchor.CENTER_HORIZONTAL;
		public ScreenAnchor verticalAnchor = ScreenAnchor.CENTER_VERTICAL;

		internal Signal proceedSignal = new Signal ();

		internal void Init()
		{
			startButton.releaseSignal.AddListener (onStartClick);
			transform.localPosition = screenUtil.GetAnchorPosition(horizontalAnchor, verticalAnchor);
		}

		internal void SetScore(int value)
		{
			score_field.text = value.ToString ();
		}

		private void onStartClick()
		{
			proceedSignal.Dispatch ();
		}
	}
}

