using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;

namespace strange.examples.strangerocks.game
{
	public class CreateGameFieldCommand : Command
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{ get; set; }

		public override void Execute ()
		{
			Vector3 center = Vector3.zero;

			//setup the game field
			if (injectionBinder.GetBinding<GameObject> (GameElement.GAME_FIELD) == null)
			{
				GameObject gameField = new GameObject (GameElement.GAME_FIELD.ToString());
				gameField.transform.localPosition = center;
				gameField.transform.parent = contextView.transform;

				//Bind it so we can use it elsewhere
				injectionBinder.Bind<GameObject> ().ToValue (gameField).ToName (GameElement.GAME_FIELD);
			}
		}
	}
}

