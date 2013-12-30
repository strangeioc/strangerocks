//Utility class providing Camera/GameObject mapping capabilities

using System;
using UnityEngine;
using strange.extensions.injector.api;

namespace strange.examples.strangerocks
{
	//Anchors to the corners/edges/center of screen
	public enum ScreenAnchor
	{
		LEFT,
		RIGHT,
		TOP,
		BOTTOM,
		CENTER_VERTICAL,
		CENTER_HORIZONTAL,
	}

	//OUR ONE EXAMPLE OF IMPLICIT BINDINGS
	//You'll note that there is no binding of IScreenUtil to ScreenUtil in any of the Contexts.
	//It's handled automatically here.
	[Implements(typeof(IScreenUtil), InjectionBindingScope.SINGLE_CONTEXT)]
	public class ScreenUtil : IScreenUtil
	{
		//The camera in use by the Context
		[Inject(StrangeRocksElement.GAME_CAMERA)]
		public Camera gameCamera{ get; set; }

		//Get a rect that represents the provided values as a percentage of the screen
		//GameDebugView uses this to create resolution-independent positions for GUI elements
		public Rect GetScreenRect (float x, float y, float width, float height)
		{
			float screenWidth = Screen.width;
			float screenHeight = Screen.height;
			return new Rect (x * screenWidth,
				y * screenHeight,
				width * screenWidth,
				height * screenHeight);
		}

		//Not actually used. A method for determining is a gameObject is visible to the camera.
		//I ended up just using renderer.isVisible
		public bool IsInCamera(GameObject go)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes (gameCamera);
			return GeometryUtility.TestPlanesAABB (planes, go.renderer.bounds);
		}

		//When a rock or the player exists the screen,
		//This "wraps" the GameObject to the far side of the screen
		public void TranslateToFarSide(GameObject go)
		{
			Vector3 pos = go.transform.localPosition;
			Vector3 viewPos = gameCamera.WorldToViewportPoint (pos);

			if (viewPos.x > 1f)
			{
				viewPos.x = .001f;
			}
			else if (viewPos.x < 0f)
			{
				viewPos.x = .999f;
			}

			if (viewPos.y > 1f)
			{
				viewPos.y = .001f;
			}
			else if (viewPos.y < 0f)
			{
				viewPos.y = .999f;
			}

			Vector3 newPos = gameCamera.ViewportToWorldPoint (viewPos);
			go.transform.localPosition = newPos;
		}

		//Calculates entry positions for the enemy spaceships
		public Vector3 RandomPositionOnLeft()
		{
			Vector3 viewPos = new Vector3 (0f, UnityEngine.Random.Range(0f, 1f), gameCamera.transform.localPosition.y);
			Vector3 retv = gameCamera.ViewportToWorldPoint (viewPos);
			return retv;
		}

		//Return a Vector3 placing a UI element at some anchored place onscreen
		public Vector3 GetAnchorPosition(ScreenAnchor horizontal, ScreenAnchor vertical)
		{
			float x;
			float y;

			switch (horizontal)
			{
			case ScreenAnchor.LEFT:
				x = 0;
				break;
			case ScreenAnchor.CENTER_HORIZONTAL:
				x = .5f;
				break;
			case ScreenAnchor.RIGHT:
				x = 1f;
				break;
			default:
				throw new Exception ("ScreenUtil.GetAnchorPosition illegal horizontal value");
			}

			switch (vertical)
			{
			case ScreenAnchor.BOTTOM:
				y = 0;
				break;
			case ScreenAnchor.CENTER_VERTICAL:
				y = .5f;
				break;
			case ScreenAnchor.TOP:
				y = 1f;
				break;
			default:
				throw new Exception ("ScreenUtil.GetAnchorPosition illegal horizontal value");
			}
			Vector3 retv = new Vector3 (x, y, gameCamera.transform.localPosition.y);
			retv = gameCamera.ViewportToWorldPoint (retv);
			return retv;
		}
	}
}

