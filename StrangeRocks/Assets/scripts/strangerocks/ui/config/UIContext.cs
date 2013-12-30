//The Context for our UI.

using System;
using UnityEngine;
using strange.extensions.context.impl;

namespace strange.examples.strangerocks.ui
{
	public class UIContext : SignalContext
	{
		//All Contexts should extend at least one of the base Constructors
		public UIContext (MonoBehaviour contextView) : base (contextView)
		{
		}

		//Map bindings is where most of the **visible** work of a Context happens.
		//There are of course lots of **invisible** things happening, which you
		//can investigate up the inheritance chain. For purposes of this tutorial,
		//however, note that at some point this method gets called and all your
		//bindings get called.
		protected override void mapBindings ()
		{
			//We need to call mapBindings up the inheritance chain (see SignalContext)
			base.mapBindings ();

			//This group of bindings occur ONLY if this Context is running in standalone mode.
			//Otherwise, they're assumed to be called elsewhere. If they're not bound
			//somewhere, things will break.
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

			//StartSignal is instantiated and fired in the SignalContext.
			//When it fires, UIStartCommand is Executed.
			commandBinder.Bind<StartSignal> ()
				.To<UIStartCommand> ();

			//Mediation
			//Mediation allows us to separate the View code from the rest of the app.
			//The details of **why** mediation is a good thing can be read in the faq:
			//http://thirdmotion.github.io/strangeioc/faq.html#why-mediator
			mediationBinder.Bind<EndGamePanelView> ().To<EndGameMediator> ();
			mediationBinder.Bind<HUDView> ().To<HUDMediator> ();
			mediationBinder.Bind<IdlePanelView> ().To<IdlePanelMediator> ();
			mediationBinder.Bind<StartLevelPanelView> ().To<StartLevelPanelMediator> ();

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)

			//This code executes if we're on a mobile device

			//If we're on mobile...
			//We'll show the touchscreen controls
			mediationBinder.Bind<OnscreenControlsView> ().To<OnscreenControlsMediator> ();
			//And we'll map the touch version of the ButtonMediator.
			mediationBinder.Bind<ButtonView> ().To<ButtonTouchMediator> ();
#else

			//This code executes for other platforms

			//If we're on desktop/editor/web...
			//We'll destroy the touchscreen controls
			Transform transform = (contextView as GameObject).transform.FindChild("onscreen_controls");
			if (transform != null)
			{
				GameObject.Destroy(transform.gameObject);
			}
			//And we'll map the mouse-friendly version of the ButtonMediator
			mediationBinder.Bind<ButtonView> ().To<ButtonMouseMediator> ();
#endif
		}

		//After bindings are done, you sometimes want to do more stuff to configure your app.
		//Do that sort of stuff here.
		protected override void postBindings ()
		{
			//Identify and bind the UI camera
			Camera cam = (contextView as GameObject).GetComponentInChildren<Camera> ();
			if (cam == null)
			{
				throw new Exception ("Couldn't find the UI camera");
			}
			injectionBinder.Bind<Camera> ().ToValue (cam).ToName (StrangeRocksElement.GAME_CAMERA);

			//Don't forget to call the base version...important stuff happens there!!!
			base.postBindings ();

			//Disable the AudioListener
			if (Context.firstContext != this)
			{
				AudioListener listener = (contextView as GameObject).GetComponentInChildren<AudioListener> ();
				listener.enabled = false;
			}
		}
	}
}

