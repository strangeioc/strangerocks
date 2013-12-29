using System;
using strange.extensions.signal.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	//Game
	public class GameStartedSignal : Signal{}

	//Input

	//Player
	public class CreatePlayerSignal : Signal{}
	public class DestroyPlayerSignal : Signal<ShipView, bool>{}

	//Rocks, Missiles, Enemies
	public class CreateEnemySignal : Signal<int, Vector3>{}
	public class CreateRockSignal : Signal<int, Vector3>{}
	public class DestroyEnemySignal : Signal<EnemyView, bool>{}
	public class DestroyMissileSignal : Signal<MissileView, GameElement>{}
	public class DestroyRockSignal : Signal<RockView, bool>{}
	public class FireMissileSignal : Signal<GameObject, GameElement>{}
	public class MissileHitSignal : Signal<MissileView, GameObject>{}

	//Level

	public class SetupLevelSignal : Signal{}
	public class LevelStartedSignal : Signal{}
}

