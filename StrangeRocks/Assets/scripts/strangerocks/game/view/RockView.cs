//The "View" for a Rock. This MonoBehaviour is attached to the rock prefab inside Unity.

using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class RockView : View
	{
		//INJECTING INTO VIEWS IS GENERALlY A BAD, BAD THING!!!!!!!!!
		//I'm deliberately including this as an example of the right time to do it.
		//ScreenUtil is uniquely interested in matching screen coordinates/relative camera
		//positions with the world/local positions of GameObjects. As such, it is pure View
		//logic. Injecting a bit of pure View logic into the View allows access to the right utility
		//in the right place.
		//DO NOT USE INJECTION IN VIEWS TO INJECT THINGS THAT BELONG IN THE MEDIATOR,
		//such as Signals, GameModels, etc.
		[Inject]
		public IScreenUtil screenUtil{get;set;}

		//Values settable from Unity
		public Renderer mainRenderer;
		public float rotationSpeed = 1000f;

		//Set from CreateRockCommand to determine this Rock's level. Higher levels are smaller, worth more points.
		public int level = 0;

		void FixedUpdate()
		{
			//Some spin for visual fun
			rigidbody.AddRelativeTorque (Vector3.up * rotationSpeed);

			if (!mainRenderer.isVisible)
			{
				//Rock flew offscreen. Translate it to the far side.
				screenUtil.TranslateToFarSide (gameObject);
			}
		}
	}
}

