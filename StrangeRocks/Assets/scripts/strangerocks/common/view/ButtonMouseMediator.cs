//Mediates the ButtonView in Editor/Desktop/Web...wherever mouse clicks are appropriate

using System;
using strange.extensions.mediation.impl;

namespace strange.examples.strangerocks.ui
{
	public class ButtonMouseMediator : Mediator
	{
		[Inject]
		public ButtonView view { get; set; }

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

		protected void OnMouseDown()
		{
			view.pressBegan ();
		}

		protected void OnMouseUp()
		{
			view.pressEnded ();
		}

		protected void OnMouseEnter()
		{
			view.background.renderer.material.color = view.overColor;
		}

		protected void OnMouseExit()
		{
			view.background.renderer.material.color = view.normalColor;
		}

		#endif
	}
}

