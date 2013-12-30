//All the Signals exclusive to the GameContext

using System;
using strange.extensions.signal.impl;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	//Game
	public class GameStartedSignal : Signal{}

	//Player
	public class CreatePlayerSignal : Signal{}

	//ShipView - reference to the Player's Ship
	//bool - False indicates destruction. True indicates cleanup at end of level.
	public class DestroyPlayerSignal : Signal<ShipView, bool>{}

	//Rocks, Missiles, Enemies

	//int - The level of the enemy
	//Vector3 - Position of the enemy
	public class CreateEnemySignal : Signal<int, Vector3>{}

	//int - The level (size) of the rock
	//Vector3 - Position of the rock
	public class CreateRockSignal : Signal<int, Vector3>{}

	//EnemyView - reference to the specific ship
	//bool - True indicates player gets points. False is simple cleanup.
	public class DestroyEnemySignal : Signal<EnemyView, bool>{}

	//MissileView - reference to the specific missile
	//GameElement - ID to indicate if it was a Player or Enemy missile
	public class DestroyMissileSignal : Signal<MissileView, GameElement>{}

	//RockView - reference to the specific rock
	//bool - True indicates player gets points. False is simple cleanup.
	public class DestroyRockSignal : Signal<RockView, bool>{}

	//GameObject - The GameObject that fired the missile
	//GameElemet - ID to indicate if it is a Player or Enemy missile
	public class FireMissileSignal : Signal<GameObject, GameElement>{}

	//MissileView - reference to the specific missile
	//GameObject - The contact with which the missile collided
	public class MissileHitSignal : Signal<MissileView, GameObject>{}

	//Level
	public class SetupLevelSignal : Signal{}
	public class LevelStartedSignal : Signal{}
}

