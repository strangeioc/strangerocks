using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks.game
{
	public class ShipView : View
	{
		[Inject]
		public IScreenUtil screenUtil{get;set;}

		internal Signal collisionSignal = new Signal ();

		public Renderer mainRenderer;
		public ParticleSystem thrustParticles;

		public float rotationSpeed = 10f;
		public float thrustSpeed = 6f;

		private int input;



		override protected void Start()
		{
			base.Start ();
			rigidbody.centerOfMass = Vector3.zero;
		}

		internal void SetAction(int evt)
		{
			input = evt;
		}

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

		void OnTriggerEnter()
		{
			collisionSignal.Dispatch ();
		}
	}
}

