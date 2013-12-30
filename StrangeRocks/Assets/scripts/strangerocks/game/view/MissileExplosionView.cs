//Extends ExplosionView to mark it as the Missile version.
//MissileExplosions get pooled, regular explosions don't. By marking the explosion in this way,
//We can add different Mediators.

//This View is attached to the missile_explosion prefab in Unity.

using System;

namespace strange.examples.strangerocks.game
{
	public class MissileExplosionView : ExplosionView
	{
	}
}

