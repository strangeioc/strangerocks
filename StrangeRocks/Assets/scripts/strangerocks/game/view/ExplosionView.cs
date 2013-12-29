using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks.game
{
	public class ExplosionView : View
	{
		public GameObject particleView;

		internal Signal animationCompleteSignal = new Signal ();

		void OnEnable()
		{

		}

		protected void Update()
		{
			if (particleView.GetComponent<ParticleSystem> ().IsAlive () == false)
				animationCompleteSignal.Dispatch ();
		}

		internal void resetAnimation()
		{
			particleView.GetComponent<ParticleSystem> ().time = 0f;
		}
	}
}

