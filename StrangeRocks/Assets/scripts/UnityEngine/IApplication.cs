//UnityEngine.Application interface

namespace UnityEngine
{
	public interface IApplication
	{
		string absoluteURL { get; }
		ThreadPriority backgroundLoadingPriority { get; set; }
		string dataPath { get; }
		bool genuine { get; }
		bool genuineCheckAvailable { get; }
		NetworkReachability internetReachability { get; }
		bool isEditor { get; }
		bool isLoadingLevel { get; }
		bool isPlaying { get; }
		bool isWebPlayer { get; }
		int levelCount { get; }
		int loadedLevel { get; }
		string loadedLevelName { get; }
		string persistentDataPath { get; }
		RuntimePlatform platform { get; }
		bool runInBackground { get; set; }
		string srcValue { get; }
		int streamedBytes { get; }
		string streamingAssetsPath { get; }
		SystemLanguage systemLanguage { get; }
		int targetFrameRate { get; set; }
		string temporaryCachePath { get; }
		string unityVersion { get; }
		bool webSecurityEnabled { get; }
		string webSecurityHostUrl { get; }

		void CancelQuit ();
		bool CanStreamedLevelBeLoaded (int levelIndex);
		bool CanStreamedLevelBeLoaded (string levelName);
		void CaptureScreenshot (string filename, int superSize);
		void CaptureScreenshot (string filename);
		void ExternalCall (string functionName, params object[] args);
		void ExternalEval (string script);
		float GetStreamProgressForLevel (string levelName);
		float GetStreamProgressForLevel (int levelIndex);
		bool HasProLicense ();
		bool HasUserAuthorization (UserAuthorization mode);
		void LoadLevel (string name);
		void LoadLevel (int index);
		void LoadLevelAdditive (string name);
		void LoadLevelAdditive (int index);
		AsyncOperation LoadLevelAdditiveAsync (string levelName);
		AsyncOperation LoadLevelAdditiveAsync (int index);
		AsyncOperation LoadLevelAsync (int index);
		AsyncOperation LoadLevelAsync (string levelName);
		void OpenURL (string url);
		void Quit ();
		void RegisterLogCallback (Application.LogCallback handler);
		void RegisterLogCallbackThreaded (Application.LogCallback handler);
		AsyncOperation RequestUserAuthorization (UserAuthorization mode);
	}
}


