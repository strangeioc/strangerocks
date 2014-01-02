//Wrap UnityEngine.Application to easier mocking

using System;
using UnityEngine;

public class ApplicationWrapper : IApplication
{
	public ApplicationWrapper ()
	{
	}

	public void CancelQuit ()
	{
		Application.CancelQuit();
	}

	public bool CanStreamedLevelBeLoaded (int levelIndex)
	{
		return Application.CanStreamedLevelBeLoaded( levelIndex );
	}

	public bool CanStreamedLevelBeLoaded (string levelName)
	{
		return Application.CanStreamedLevelBeLoaded( levelName );
	}

	public void CaptureScreenshot (string filename, int superSize)
	{
		Application.CaptureScreenshot( filename, superSize );
	}

	public void CaptureScreenshot (string filename)
	{
		Application.CaptureScreenshot( filename );
	}

	public void ExternalCall (string functionName, params object[] args)
	{
		Application.ExternalCall( functionName, args );
	}

	public void ExternalEval (string script)
	{
		Application.ExternalEval( script );
	}

	public float GetStreamProgressForLevel (string levelName)
	{
		return Application.GetStreamProgressForLevel( levelName );
	}

	public float GetStreamProgressForLevel (int levelIndex)
	{
		return Application.GetStreamProgressForLevel( levelIndex );
	}

	public bool HasProLicense ()
	{
		return Application.HasProLicense();
	}

	public bool HasUserAuthorization (UserAuthorization mode)
	{
		return Application.HasUserAuthorization( mode );
	}

	public void LoadLevel (string name)
	{
		Application.LoadLevel( name );
	}

	public void LoadLevel (int index)
	{
		Application.LoadLevel( index );
	}

	public void LoadLevelAdditive (string name)
	{
		Application.LoadLevelAdditive( name );
	}

	public void LoadLevelAdditive (int index)
	{
		Application.LoadLevelAdditive( index );
	}

	public AsyncOperation LoadLevelAdditiveAsync (string levelName)
	{
		return Application.LoadLevelAdditiveAsync( levelName );
	}

	public AsyncOperation LoadLevelAdditiveAsync (int index)
	{
		return Application.LoadLevelAdditiveAsync( index );
	}

	public AsyncOperation LoadLevelAsync (int index)
	{
		return Application.LoadLevelAsync( index );
	}

	public AsyncOperation LoadLevelAsync (string levelName)
	{
		return Application.LoadLevelAsync( levelName );
	}

	public void OpenURL (string url)
	{
		Application.OpenURL( url );
	}

	public void Quit ()
	{
		Application.Quit();
	}

	public void RegisterLogCallback (Application.LogCallback handler)
	{
		Application.RegisterLogCallback( handler );
	}

	public void RegisterLogCallbackThreaded (Application.LogCallback handler)
	{
		Application.RegisterLogCallbackThreaded( handler );
	}

	public AsyncOperation RequestUserAuthorization (UserAuthorization mode)
	{
		return Application.RequestUserAuthorization( mode );
	}

	public string absoluteURL {
		get
		{
			return Application.absoluteURL;
		}
	}

	public ThreadPriority backgroundLoadingPriority {
		get
		{
			return Application.backgroundLoadingPriority;
		}
		set
		{
			Application.backgroundLoadingPriority = value;
		}
	}

	public string dataPath {
		get
		{
			return Application.dataPath;
		}
	}

	public bool genuine {
		get
		{
			return Application.genuine;
		}
	}

	public bool genuineCheckAvailable {
		get
		{
			return Application.genuineCheckAvailable;
		}
	}

	public NetworkReachability internetReachability {
		get
		{
			return Application.internetReachability;
		}
	}

	public bool isEditor {
		get
		{
			return Application.isEditor;
		}
	}

	public bool isLoadingLevel {
		get
		{
			return Application.isLoadingLevel;
		}
	}

	public bool isPlaying {
		get
		{
			return Application.isPlaying;
		}
	}

	public bool isWebPlayer {
		get
		{
			return Application.isWebPlayer;
		}
	}

	public int levelCount {
		get
		{
			return Application.levelCount;
		}
	}

	public int loadedLevel {
		get
		{
			return Application.loadedLevel;
		}
	}

	public string loadedLevelName {
		get
		{
			return Application.loadedLevelName;
		}
	}

	public string persistentDataPath {
		get
		{
			return Application.persistentDataPath;
		}
	}

	public RuntimePlatform platform {
		get
		{
			return Application.platform;
		}
	}

	public bool runInBackground {
		get
		{
			return Application.runInBackground;
		}
		set
		{
			Application.runInBackground = value;
		}
	}

	public string srcValue {
		get
		{
			return Application.srcValue;
		}
	}

	public int streamedBytes {
		get
		{
			return Application.streamedBytes;
		}
	}

	public string streamingAssetsPath {
		get
		{
			return Application.streamingAssetsPath;
		}
	}

	public SystemLanguage systemLanguage {
		get
		{
			return Application.systemLanguage;
		}
	}

	public int targetFrameRate {
		get
		{
			return Application.targetFrameRate;
		}
		set
		{
			Application.targetFrameRate = value;
		}
	}

	public string temporaryCachePath {
		get
		{
			return Application.temporaryCachePath;
		}
	}

	public string unityVersion {
		get
		{
			return Application.unityVersion;
		}
	}

	public bool webSecurityEnabled {
		get
		{
			return Application.webSecurityEnabled;
		}
	}

	public string webSecurityHostUrl {
		get
		{
			return Application.webSecurityHostUrl;
		}
	}

}

