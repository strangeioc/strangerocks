//Mediates the ButtonView on devices

using System;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.ui
{
	public class ButtonTouchMediator : Mediator
	{
		[Inject]
		public ButtonView view{ get; set; }

		[Inject(StrangeRocksElement.GAME_CAMERA)]
		public Camera gameCamera{ get; set; }

		#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)

		void Update()
		{
			if (Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					Ray ray = gameCamera.ScreenPointToRay (touch.position);
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit))
					{
						if (hit.collider == collider)
						{
							if (touch.phase == TouchPhase.Began)
							{
								view.pressBegan ();
							}
							else if (touch.phase == TouchPhase.Ended)
							{
								view.pressEnded ();
							}
						}
					}
				}
			}
		}



		#endif
	}
}

