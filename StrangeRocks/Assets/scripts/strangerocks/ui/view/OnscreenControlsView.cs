using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.ui
{
	public class OnscreenControlsView : View
	{
		[Inject]
		public IScreenUtil screenUtil{ get; set; }

		public ButtonView rotateLeftButton;
		public ButtonView rotateRightButton;
		public ButtonView thrustButton;
		public ButtonView fireButton;

		internal int input = 0;

		internal void init()
		{
			rotateLeftButton.pressSignal.AddListener (leftPress);
			rotateLeftButton.releaseSignal.AddListener (leftRelease);

			rotateRightButton.pressSignal.AddListener (rightPress);
			rotateRightButton.releaseSignal.AddListener (rightRelease);

			thrustButton.pressSignal.AddListener (thrustPress);
			thrustButton.releaseSignal.AddListener (thrustRelease);

			fireButton.pressSignal.AddListener (firePress);
			fireButton.releaseSignal.AddListener (fireRelease);

			Vector3 leftPos = 
				screenUtil.GetAnchorPosition (ScreenAnchor.LEFT, ScreenAnchor.BOTTOM) - transform.localPosition;
			leftPos.x += rotateLeftButton.background.renderer.bounds.size.x;
			leftPos.z += rotateLeftButton.background.renderer.bounds.size.z;
			rotateLeftButton.transform.localPosition = leftPos;

			Vector3 rightPos = screenUtil.GetAnchorPosition (ScreenAnchor.RIGHT, ScreenAnchor.BOTTOM) - transform.localPosition;
			rightPos.x -= rotateRightButton.background.renderer.bounds.size.x;
			rightPos.z += rotateRightButton.background.renderer.bounds.size.z;
			rotateRightButton.transform.localPosition = rightPos;

			Vector3 thrustPos = rotateLeftButton.transform.localPosition;
			thrustPos.z += rotateLeftButton.background.renderer.bounds.size.z;
			thrustButton.transform.localPosition = thrustPos;

			Vector3 firePos = rotateRightButton.transform.localPosition;
			firePos.z += rotateRightButton.background.renderer.bounds.size.z;
			fireButton.transform.localPosition = firePos;
		}

		private void leftPress()
		{
			input |= GameInputEvent.ROTATE_LEFT;
		}

		private void leftRelease()
		{
			input ^= GameInputEvent.ROTATE_LEFT;
		}

		private void rightPress()
		{
			input |= GameInputEvent.ROTATE_RIGHT;
		}

		private void rightRelease()
		{
			input ^= GameInputEvent.ROTATE_RIGHT;
		}

		private void thrustPress()
		{
			input |= GameInputEvent.THRUST;
		}

		private void thrustRelease()
		{
			input ^= GameInputEvent.THRUST;
		}

		private void firePress()
		{
			input |= GameInputEvent.FIRE;
		}

		private void fireRelease()
		{
			input ^= GameInputEvent.FIRE;
		}
	}
}

