using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;

namespace strange.examples.strangerocks
{
	public class EstablishGameCameraCommand : Command
	{
		[Inject (ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{ get; set; }

		public override void Execute ()
		{
			Debug.Log ("EstablishGameCameraCommand");

			Camera cam = contextView.GetComponentInChildren<Camera> ();

			if (cam == null)
			{
				throw new Exception ("EstablishGameCameraCommand couldn't find the game camera");
			}

			injectionBinder.Bind<Camera> ().ToValue (cam).ToName ("GameCamera");
		}
	}
}

