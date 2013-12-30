//The "View" for the player's ship. This MonoBehaviour is attached to the player_ship prefab inside Unity.

using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks.game
{
	public class ShipView : View
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

		internal Signal collisionSignal = new Signal ();

		//Settable from Unity
		public Renderer mainRenderer;
		public ParticleSystem thrustParticles;
		public float rotationSpeed = 10f;
		public float thrustSpeed = 6f;

		//When the user selects input (by whatever method we've mapped), the result
		//arrives in the form of an int. We keep a copy of that int here for analysis.
		//The value is bitwise...see KeyboardInput and OnscreenControlsView for details.
		private int input;

		//Initialize called by the Mediator. Init is a little like
		//Start()...but by calling it from the Mediator's OnRegister(),
		//we know the Mediator is in place before doing anything important.
		internal void Init()
		{
			rigidbody.centerOfMass = Vector3.zero;
		}

		//Set the IME value
		internal void SetAction(int evt)
		{
			input = evt;
		}

		//Move the ship based on user input
		void FixedUpdate()
		{
			bool left = (input & GameInputEvent.ROTATE_LEFT) > 0;
			bool right = (input & GameInputEvent.ROTATE_RIGHT) > 0;
			bool thrust = (input & GameInputEvent.THRUST) > 0;

			if (left)
			{
				rigidbody.AddRelativeTorque (Vector3.down * rotationSpeed);
			}
				
			if (right)
			{
				rigidbody.AddRelativeTorque (Vector3.up * rotationSpeed);
			}
				
			if (thrust)
			{
				rigidbody.AddRelativeForce (Vector3.right * thrustSpeed);
				thrustParticles.startLifetime = 1f;
			} else
			{
				thrustParticles.startLifetime = .1f;
			}

			if (!mainRenderer.isVisible)
			{
				screenUtil.TranslateToFarSide (gameObject);
			}
		}

		//If we hit anything, we fire a collisionSignal. The logic of what happens passes through the ShipMediator to the DestroyPlayerCommand;
		//but we could map it to something else if we were inclined to change the logic. For example, if we were to add rubber bumpers,
		//the mediator could add a playerBounceSignal() mapped (if necessary) to an appropriate PlayerBounceCommand. Importantly, the View
		//wouldn't require an edit for that rule change to happen.
		void OnTriggerEnter()
		{
			collisionSignal.Dispatch ();
		}
	}
}

