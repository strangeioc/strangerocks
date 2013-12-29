using System;
using UnityEngine;
using strange.extensions.context.impl;

namespace strange.examples.strangerocks.ui
{
	public class UIContext : SignalContext
	{
		public UIContext (MonoBehaviour contextView) : base (contextView)
		{
		}

		protected override void mapBindings ()
		{
			base.mapBindings ();

			if (Context.firstContext == this)
			{
				injectionBinder.Bind<GameEndSignal> ().ToSingleton ();
				injectionBinder.Bind<GameInputSignal> ().ToSingleton ();
				injectionBinder.Bind<GameStartSignal> ().ToSingleton ();
				injectionBinder.Bind<LevelStartSignal> ().ToSingleton ();
				injectionBinder.Bind<LevelEndSignal> ().ToSingleton ();
				injectionBinder.Bind<UpdateLevelSignal> ().ToSingleton ();
				injectionBinder.Bind<UpdateLivesSignal> ().ToSingleton ();
				injectionBinder.Bind<UpdateScoreSignal> ().ToSingleton ();
			}

			commandBinder.Bind<StartSignal> ()
				.To<UIStartCommand> ();

			//Mediation
			mediationBinder.Bind<EndGamePanelView> ().To<EndGameMediator> ();
			mediationBinder.Bind<HUDView> ().To<HUDMediator> ();
			mediationBinder.Bind<IdlePanelView> ().To<IdlePanelMediator> ();
			mediationBinder.Bind<StartLevelPanelView> ().To<StartLevelPanelMediator> ();

			#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
			mediationBinder.Bind<OnscreenControlsView> ().To<OnscreenControlsMediator> ();
			mediationBinder.Bind<ButtonView> ().To<ButtonTouchMediator> ();
			#else
			mediationBinder.Bind<ButtonView> ().To<ButtonMouseMediator> ();
			Transform transform = (contextView as GameObject).transform.FindChild("onscreen_controls");
			if (transform != null)
			{
				GameObject.Destroy(transform.gameObject);
			}
			#endif
		}

		protected override void postBindings ()
		{
			Camera cam = (contextView as GameObject).GetComponentInChildren<Camera> ();
			if (cam == null)
			{
				throw new Exception ("Couldn't find the UI camera");
			}
			injectionBinder.Bind<Camera> ().ToValue (cam).ToName ("GameCamera");
			base.postBindings ();

			if (Context.firstContext != this)
			{
				AudioListener listener = (contextView as GameObject).GetComponentInChildren<AudioListener> ();
				listener.enabled = false;
			}
		}
	}
}

