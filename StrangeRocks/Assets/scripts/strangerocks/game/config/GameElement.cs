//This is an enum of named injections used around the game.

using System;

namespace strange.examples.strangerocks.game
{
	public enum GameElement
	{
		ENEMY_POOL,				//Injection names of the pools
		ENEMY_MISSILE_POOL,
		MISSILE_EXPLOSION_POOL,
		MISSILE_POOL,
		ROCK_POOL,
		GAME_FIELD,				//Injection name of the GameObject that parents the rocks/missiles/player/etc.
		PLAYER_SHIP,			//Injection name of the player's vessel
	}
}

