using System;
using UnityEngine;

namespace strange.examples.strangerocks
{
	public interface IScreenUtil
	{
		Rect GetScreenRect(float x, float y, float width, float height);

		bool IsInCamera(GameObject go);

		void TranslateToFarSide(GameObject go);

		Vector3 RandomPositionOnLeft();

		Vector3 GetAnchorPosition(ScreenAnchor horizontal, ScreenAnchor vertical);
	}
}

