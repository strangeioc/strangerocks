using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class CreatePlayerCommand : Command
	{
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		[Inject]
		public IGameModel gameModel { get; set; }

		public override void Execute ()
		{
			if (injectionBinder.GetBinding<ShipView> (GameElement.PLAYER_SHIP) != null)
				injectionBinder.Unbind<ShipView> (GameElement.PLAYER_SHIP);

			//add the player's ship
			GameObject shipStyle = Resources.Load<GameObject> (GameElement.PLAYER_SHIP.ToString());
			shipStyle.transform.localScale = Vector3.one;

			GameObject shipGO = GameObject.Instantiate (shipStyle) as GameObject;
			shipGO.transform.localPosition = Vector3.zero;
			shipGO.layer = LayerMask.NameToLayer("player");

			shipGO.transform.parent = gameField.transform;

			injectionBinder.Bind<ShipView> ().ToValue (shipGO.GetComponent<ShipView> ()).ToName (GameElement.PLAYER_SHIP);

			gameModel.levelInProgress = true;
		}
	}
}

