using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace strange.examples.strangerocks.ui
{
	public class HUDView : View
	{
		[Inject]
		public IScreenUtil screenUtil{ get; set; }

		public ScreenAnchor scoreHorizontalAnchor = ScreenAnchor.LEFT;
		public ScreenAnchor scoreVerticalAnchor = ScreenAnchor.TOP;
		public GameObject scoreBlock;
		public TextMesh scoreText;

		public ScreenAnchor levelHorizontalAnchor = ScreenAnchor.LEFT;
		public ScreenAnchor levelVerticalAnchor = ScreenAnchor.BOTTOM;
		public GameObject levelBlock;
		public TextMesh levelText;

		public ScreenAnchor livesHorizontalAnchor = ScreenAnchor.RIGHT;
		public ScreenAnchor livesVerticalAnchor = ScreenAnchor.TOP;
		public GameObject livesBlock;

		private GameObject[] livesGOs;

		internal void Init()
		{
			transform.localPosition = screenUtil.GetAnchorPosition (ScreenAnchor.CENTER_HORIZONTAL, ScreenAnchor.CENTER_VERTICAL);
			scoreBlock.transform.localPosition = screenUtil.GetAnchorPosition (scoreHorizontalAnchor, scoreVerticalAnchor);
			levelBlock.transform.localPosition = screenUtil.GetAnchorPosition (levelHorizontalAnchor, levelVerticalAnchor);
			livesBlock.transform.localPosition = screenUtil.GetAnchorPosition (livesHorizontalAnchor, livesVerticalAnchor);

			SetScore (0);
			SetLives (0);
			SetLevel (0);
		}

		internal void SetScore(int value)
		{
			scoreText.text = value.ToString();
		}

		internal void SetLevel(int value)
		{
			levelText.text = value.ToString();
		}

		internal void SetLives(int value)
		{
			if (livesGOs == null)
			{
				GameObject proto = livesBlock.transform.FindChild ("ship").gameObject;
				livesGOs = new GameObject[9];
				livesGOs [0] = proto;
				for (int a = 1; a < 9; a++)
				{
					GameObject go = GameObject.Instantiate (proto) as GameObject;
					go.transform.parent = proto.transform.parent;
					Vector3 pos = proto.transform.localPosition;
					pos.x = pos.x - (go.renderer.bounds.size.x * a);
					go.transform.localPosition = pos;
					livesGOs [a] = go;
				}
			}
			for (int a = 0; a < 9; a++)
			{
				livesGOs [a].SetActive (a < value-1);
			}
		}
	}
}

