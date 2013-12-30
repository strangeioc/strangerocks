//Note how this View mimics the behavior of the KeyboardInput class over in the game context.
//This allows us to have onscreen controls when appropriate and keyboard controls when we'd rather
//have that.

//Note also that I'm using bitwise evaluation to handle the key input. If you're not familiar with
//bitwise operations, I suggest you look it up. It's very useful for just this sort of scenario.
//http://en.wikipedia.org/wiki/Bitwise_operation

//In this case, I specifically use the following:
// |= Add the value to the result
// ^= Remove the value from the result
// &  Test if the value appears in the result

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

