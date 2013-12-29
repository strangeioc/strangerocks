using System;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.signal.impl;
using System.Collections;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class KeyboardInput : IInput
	{
		[Inject(ContextKeys.CONTEXT_DISPATCHER)]
		public IEventDispatcher dispatcher { get; set; }

		[Inject]
		public IRoutineRunner routinerunner { get; set; }

		[Inject]
		public GameInputSignal gameInputSignal{ get; set; } 

		[PostConstruct]
		public void PostConstruct()
		{
			routinerunner.StartCoroutine (update());
		}

		private bool firing = false;

		protected IEnumerator update()
		{
			while (true)
			{
				int input = GameInputEvent.NONE;
				if (Input.GetKeyDown (KeyCode.Space) && !firing)
				{
					firing = true;
					input |= GameInputEvent.FIRE;
				} else
				{
					firing = false;
				}
				if (Input.GetKey (KeyCode.LeftArrow))
				{
					input |= GameInputEvent.ROTATE_LEFT;
				} 
				if (Input.GetKey (KeyCode.UpArrow))
				{
					input |= GameInputEvent.THRUST;
				}
				if (Input.GetKey (KeyCode.RightArrow))
				{
					input |= GameInputEvent.ROTATE_RIGHT;
				}
				gameInputSignal.Dispatch (input);
				yield return null;
			}
		}
	}
}

