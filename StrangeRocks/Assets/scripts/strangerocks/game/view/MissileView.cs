using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.signal.impl;

namespace strange.examples.strangerocks.game
{
	public class MissileView : View
	{
		internal Signal<GameObject> contactSignal = new Signal<GameObject> ();
		internal Signal exitScreenSignal = new Signal ();

		public Renderer mainRenderer;

		virtual protected void OnTriggerEnter(Collider other)
		{
			contactSignal.Dispatch (other.gameObject);
		}

		void FixedUpdate()
		{
			if (gameObject.activeSelf && !mainRenderer.isVisible)
			{
				exitScreenSignal.Dispatch ();
			}
		}
	}
}

