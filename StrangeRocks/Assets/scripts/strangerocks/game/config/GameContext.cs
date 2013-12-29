using System;
using strange.extensions.context.impl;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using strange.extensions.pool.impl;

namespace strange.examples.strangerocks.game
{
	public class GameContext : SignalContext
	{
		public GameContext (MonoBehaviour contextView) : base (contextView)
		{
		}

		// Create bindings as necessary to fulfill dependencies.
		// Anything nested inside if (Context.firstContext == this)
		// will be provided here *if* you just run this Context as a standalone app.
		// If this app runs as part of a multi-Context app, the bindings are assumed to
		// be provided elsewhere (from the MainContext, most likely).
		protected override void mapBindings ()
		{
			base.mapBindings ();

			#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
			injectionBinder.Bind<IInput> ().To<NullInput> ().ToSingleton ();
			#else
			injectionBinder.Bind<IInput> ().To<KeyboardInput> ().ToSingleton ();
			#endif

			//Injection
			if (Context.firstContext == this)
			{
				injectionBinder.Bind<IGameModel> ().To<GameModel> ().ToSingleton ();
			}

			//Pools
			injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.ENEMY_MISSILE_POOL);
			injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.ENEMY_POOL);
			injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.MISSILE_EXPLOSION_POOL);
			injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.MISSILE_POOL);
			injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.ROCK_POOL);

			//Signals (not bound to Commands)
			injectionBinder.Bind<GameStartedSignal> ().ToSingleton ();
			injectionBinder.Bind<LevelStartedSignal> ().ToSingleton ();

			if (Context.firstContext == this)
			{
				injectionBinder.Bind<GameInputSignal> ().ToSingleton ();
				injectionBinder.Bind<UpdateLevelSignal> ().ToSingleton ();
				injectionBinder.Bind<UpdateLivesSignal> ().ToSingleton ();
				injectionBinder.Bind<UpdateScoreSignal> ().ToSingleton ();
			}

			//Commands
			if (Context.firstContext == this)
			{
				commandBinder.Bind<StartSignal> ()
					.To<GameIndependentStartCommand> ()
					.Once ();
			}
			else
			{
				commandBinder.Bind<StartSignal> ()
					.To<GameModuleStartCommand> ()
					.Once ();
			}

			commandBinder.Bind<CreateEnemySignal> ().To<CreateEnemyCommand> ().Pooled();
			commandBinder.Bind<CreatePlayerSignal> ().To<CreatePlayerCommand> ();
			commandBinder.Bind<CreateRockSignal> ().To<CreateRockCommand> ().Pooled();
			commandBinder.Bind<DestroyEnemySignal>().To<DestroyEnemyCommand>().Pooled();
			commandBinder.Bind<DestroyMissileSignal>().To<DestroyMissileCommand>().Pooled();
			commandBinder.Bind<DestroyPlayerSignal>().To<DestroyPlayerCommand>().Pooled();
			commandBinder.Bind<DestroyRockSignal>().To<DestroyRockCommand>().Pooled();
			commandBinder.Bind<FireMissileSignal> ().To<FireMissileCommand> ().Pooled();
			commandBinder.Bind<GameStartSignal> ().To<GameStartCommand> ();
			commandBinder.Bind<GameEndSignal> ().To<EndGameCommand> ();
			commandBinder.Bind<LevelStartSignal> ()
				.To<CreateGameFieldCommand>()
				.To<CleanupLevelCommand>()
				.To<StartLevelCommand> ()
				.InSequence();
			commandBinder.Bind<LevelEndSignal> ()
				.To<CleanupLevelCommand>()
				.To<LevelEndCommand> ()
				.InSequence();
			commandBinder.Bind<MissileHitSignal> ().To<MissileHitCommand> ().Pooled();
			commandBinder.Bind<SetupLevelSignal> ().To<SetupLevelCommand> ();


			//Mediation
			mediationBinder.Bind<EnemyView> ().To<EnemyMediator> ();
			mediationBinder.Bind<EnemyMissileView> ().To<EnemyMissileMediator> ();
			mediationBinder.Bind<ExplosionView> ().To<ExplosionMediator> ();
			mediationBinder.Bind<GameDebugView> ().To<GameDebugMediator> ();
			mediationBinder.Bind<MissileView> ().To<MissileMediator> ();
			mediationBinder.Bind<MissileExplosionView> ().To<MissileExplosionMediator> ();
			mediationBinder.Bind<RockView> ().To<RockMediator> ();
			mediationBinder.Bind<ShipView> ().To<ShipMediator> ();

		}

		protected override void postBindings ()
		{
			//Establish our camera. We do this early since it gets injected in places that help us do layout.
			Camera cam = (contextView as GameObject).GetComponentInChildren<Camera> ();
			if (cam == null)
			{
				throw new Exception ("EstablishGameCameraCommand couldn't find the game camera");
			}
			injectionBinder.Bind<Camera> ().ToValue (cam).ToName ("GameCamera");

			// Configure the pools.
			// (Hint: all our pools for this game are identical, but for the content of the InstanceProvider)
			IPool<GameObject> enemyMissilePool = injectionBinder.GetInstance<IPool<GameObject>> (GameElement.ENEMY_MISSILE_POOL);
			enemyMissilePool.instanceProvider = new ResourceInstanceProvider ("enemy_missile", LayerMask.NameToLayer ("enemy"));
			enemyMissilePool.inflationType = PoolInflationType.INCREMENT;

			IPool<GameObject> enemyPool = injectionBinder.GetInstance<IPool<GameObject>> (GameElement.ENEMY_POOL);
			enemyPool.instanceProvider = new ResourceInstanceProvider ("enemy", LayerMask.NameToLayer ("enemy"));
			enemyPool.inflationType = PoolInflationType.INCREMENT;

			IPool<GameObject> missilePool = injectionBinder.GetInstance<IPool<GameObject>> (GameElement.MISSILE_POOL);
			missilePool.instanceProvider = new ResourceInstanceProvider ("missile", LayerMask.NameToLayer ("missile"));
			missilePool.inflationType = PoolInflationType.INCREMENT;

			IPool<GameObject> missileExplosionPool = injectionBinder.GetInstance<IPool<GameObject>> (GameElement.MISSILE_EXPLOSION_POOL);
			missileExplosionPool.instanceProvider = new ResourceInstanceProvider ("missile_explosion", LayerMask.NameToLayer ("Default"));
			missileExplosionPool.inflationType = PoolInflationType.INCREMENT;

			IPool<GameObject> rockPool = injectionBinder.GetInstance<IPool<GameObject>> (GameElement.ROCK_POOL);
			rockPool.instanceProvider = new ResourceInstanceProvider ("rock", LayerMask.NameToLayer ("enemy"));
			rockPool.inflationType = PoolInflationType.INCREMENT;

			base.postBindings ();
		}
	}
}

