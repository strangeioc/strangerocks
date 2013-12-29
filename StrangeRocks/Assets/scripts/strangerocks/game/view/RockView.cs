using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class RockView : View
	{
		[Inject]
		public IScreenUtil screenUtil{get;set;}

		public Renderer mainRenderer;
		public float rotationSpeed = 1000f;

		public int level = 0;

		protected override void Start ()
		{
			base.Start ();

		}

		void FixedUpdate()
		{
			rigidbody.AddRelativeTorque (Vector3.up * rotationSpeed);


			if (!mainRenderer.isVisible)
			{
				screenUtil.TranslateToFarSide (gameObject);
			}
		}
	}
}

