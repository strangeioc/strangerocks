using System;
using UnityEngine;
using strange.extensions.injector.api;

namespace strange.examples.strangerocks
{
	public enum ScreenAnchor
	{
		LEFT,
		RIGHT,
		TOP,
		BOTTOM,
		CENTER_VERTICAL,
		CENTER_HORIZONTAL,
	}


	[Implements(typeof(IScreenUtil), InjectionBindingScope.SINGLE_CONTEXT)]
	public class ScreenUtil : IScreenUtil
	{

		[Inject("GameCamera")]
		public Camera gameCamera{ get; set; }

		public Rect GetScreenRect (float x, float y, float width, float height)
		{
			float screenWidth = Screen.width;
			float screenHeight = Screen.height;
			return new Rect (x * screenWidth,
				y * screenHeight,
				width * screenWidth,
				height * screenHeight);
		}

		public bool IsInCamera(GameObject go)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes (gameCamera);
			return GeometryUtility.TestPlanesAABB (planes, go.renderer.bounds);
		}

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

		public Vector3 RandomPositionOnLeft()
		{
			Vector3 viewPos = new Vector3 (0f, UnityEngine.Random.Range(0f, 1f), gameCamera.transform.localPosition.y);
			Vector3 retv = gameCamera.ViewportToWorldPoint (viewPos);
			return retv;
		}

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

