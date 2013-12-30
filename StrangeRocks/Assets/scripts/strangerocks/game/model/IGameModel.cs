//The API for a model representing how the player is doing

using System;

namespace strange.examples.strangerocks.game
{
	public interface IGameModel
	{
		// Restore game model to default state (as on startup)
		void Reset();

		int score{ get; set; }

		int lives{ get; set; }

		int maxLives{ get; set; }

		int level{ get; set; }

		bool levelInProgress{ get; set; }
	}
}

