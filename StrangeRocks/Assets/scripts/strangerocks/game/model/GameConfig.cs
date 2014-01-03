using System;
using UnityEngine;

namespace strange.examples.strangerocks.game
{
	public class GameConfig : IGameConfig
	{
		//PostConstruct methods fire automatically after Construction
		//and after all injections are satisfied. It's a safe place
		//to do things you'd usually sonsider doing in the Constructor.
		[PostConstruct]
		public void PostConstruct()
		{
			TextAsset file = Resources.Load ("gameConfig") as TextAsset;

			var n = SimpleJSON.JSON.Parse (file.text);

			initLives = n ["initLives"].AsInt;
			newLifeEvery = n ["newLifeEvery"].AsInt;
			maxLives = n ["maxLives"].AsInt;

			initRocks = n ["initRocks"].AsInt;
			additionalRocksPerLevel = n ["additionalRocksPerLevel"].AsInt;
			baseRockScore = n ["baseRockScore"].AsInt;
			rockExplosiveForceMin = n ["rockExplosiveForceMin"].AsFloat;
			rockExplosiveForceMax = n ["rockExplosiveForceMax"].AsFloat;

			enemySpawnSecondsMin = n ["enemySpawnSecondsMin"].AsFloat;
			enemySpawnSecondsMax = n ["enemySpawnSecondsMax"].AsFloat;
			baseEnemyScore = n ["baseEnemyScore"].AsInt;
		}

		#region implement IGameConfig
		public int initLives{ get; set; }

		//Unimplemented
		public int newLifeEvery{ get; set; }

		//Unimplemented
		public int maxLives{ get; set; }

		public int initRocks{ get; set; }

		public int additionalRocksPerLevel{ get; set; }

		public int baseRockScore{ get; set; }

		public float rockExplosiveForceMin{ get; set; }

		public float rockExplosiveForceMax{ get; set; }

		public float rockExplosiveRadius{ get; set; }

		public float enemySpawnSecondsMin{ get; set; }

		public float enemySpawnSecondsMax{ get; set; }

		public int baseEnemyScore{ get; set; }
		#endregion
	}
}

