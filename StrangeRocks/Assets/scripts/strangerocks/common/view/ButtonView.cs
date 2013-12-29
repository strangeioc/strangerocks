using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace strange.examples.strangerocks
{
	public class ButtonView : View
	{
		public Signal pressSignal = new Signal();
		public Signal releaseSignal = new Signal();

		public GameObject background;
		public TextMesh labelMesh;
		public string label;

		public Color normalColor = Color.red;
		public Color overColor = Color.magenta;
		public Color pressColor = Color.black;


		protected override void Start ()
		{
			base.Start ();
			BoxCollider bc = gameObject.AddComponent<BoxCollider> ();
			bc.center = Vector3.zero;
			Vector3 size = Vector3.one;
			size.x /= background.transform.localScale.x;
			size.y /= background.transform.localScale.y;
			size.z /= background.transform.localScale.z;

			bc.size = background.transform.localScale;

			if (labelMesh != null)
				labelMesh.text = label;
		}

		internal void pressBegan()
		{
			pressSignal.Dispatch ();
			background.renderer.material.color = pressColor;
		}

		internal void pressEnded()
		{
			releaseSignal.Dispatch ();
			background.renderer.material.color = normalColor;
		}
	}
}