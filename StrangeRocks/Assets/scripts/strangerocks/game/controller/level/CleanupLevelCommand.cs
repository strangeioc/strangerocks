using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class CleanupLevelCommand : Command
	{
		[Inject(GameElement.GAME_FIELD)]
		public GameObject gameField{ get; set; }

		[Inject]
		public DestroyPlayerSignal destroyPlayerSignal{ get; set; }

		[Inject]
		public DestroyRockSignal destroyRockSignal{ get; set; }

		[Inject]
		public DestroyEnemySignal destroyEnemySignal{ get; set; }

		[Inject]
		public DestroyMissileSignal destroyMissileCommand{ get; set; }

		public override void Execute()
		{
			if (injectionBinder.GetBinding<ShipView> (GameElement.PLAYER_SHIP) != null)
			{
				ShipView shipView = injectionBinder.GetInstance<ShipView> (GameElement.PLAYER_SHIP);
				destroyPlayerSignal.Dispatch (shipView, true);
			}

			RockView[] rocks = gameField.GetComponentsInChildren<RockView> ();
			foreach (RockView rock in rocks)
			{
				destroyRockSignal.Dispatch (rock, false);
			}

			EnemyView[] enemies = gameField.GetComponentsInChildren<EnemyView> ();
			foreach (EnemyView enemy in enemies)
			{
				destroyEnemySignal.Dispatch (enemy, false);
			}

			MissileView[] missiles = gameField.GetComponentsInChildren<MissileView> ();
			foreach (MissileView missile in missiles)
			{
				GameElement id = (missile.gameObject.name.IndexOf ("enemy") > -1) ? GameElement.ENEMY_MISSILE_POOL : GameElement.MISSILE_POOL;
				destroyMissileCommand.Dispatch (missile, id);
			}
		}
	}
}

