using System;

namespace strange.examples.strangerocks.game
{
	public class GameModel : IGameModel
	{
		public GameModel ()
		{
		}

		#region IGameModel implementation

		public void Reset ()
		{
			score = 0;
			level = 1;
			lives = maxLives;
		}

		public int score { get; set; }

		public int lives { get; set; }

		public int maxLives { get; set; }

		public int level { get; set; }

		public bool levelInProgress{ get; set; }

		#endregion
	}
}

