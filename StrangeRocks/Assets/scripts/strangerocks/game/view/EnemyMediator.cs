using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.game
{
	public class EnemyMediator : Mediator
	{
		[Inject]
		public EnemyView view { get; set; }

		[Inject]
		public DestroyEnemySignal destroyEnemySignal{ get; set; }

		[Inject]
		public FireMissileSignal fireMissileSignal{ get; set; }

		public override void OnRegister ()
		{
			view.fireWeaponSignal.AddListener (onFireWeapon);
			view.exitScreenSignal.AddListener (onExitScreen);
		}

		public override void OnRemove ()
		{
			view.fireWeaponSignal.RemoveListener (onFireWeapon);
			view.exitScreenSignal.RemoveListener (onExitScreen);
		}

		private void onExitScreen()
		{
			destroyEnemySignal.Dispatch (view, false);
		}

		private void onFireWeapon()
		{
			fireMissileSignal.Dispatch (gameObject, GameElement.ENEMY_MISSILE_POOL);
		}
	}
}

