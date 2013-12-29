using System;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.Collections;

namespace strange.examples.strangerocks.game
{
	public class EnemyView : View
	{
		internal Signal exitScreenSignal = new Signal ();
		internal Signal fireWeaponSignal = new Signal ();

		public Renderer mainRenderer;

		public float rotationSpeed = .1f;
		public float minXSpeed = 100f;
		public float maxXSpeed = 200f;
		public float minYSpeed = -50f;
		public float maxYSpeed = 50f;
		public float minCourseCorrectionSeconds = 2f;
		public float maxCourseCorrectionSeconds = 10f;
		public float minMissilePause = 1f;
		public float maxMissilePause = 3f;

		public int level;

		public Vector3 motiveForce;

		public void init(int level)
		{
			this.level = level;
			courseCorrect ();
			StartCoroutine(delayThenFireMissile ());
			transform.localScale = Vector3.one / level;
		}

		void OnEnable()
		{
			StartCoroutine(delayThenCourseCorrect());
		}

		void FixedUpdate()
		{
			mainRenderer.transform.Rotate (motiveForce * rotationSpeed);

			if (gameObject.activeSelf && !mainRenderer.isVisible)
			{
				StopAllCoroutines ();
				exitScreenSignal.Dispatch ();
			}
		}

		IEnumerator delayThenCourseCorrect()
		{
			while (true)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(minCourseCorrectionSeconds, maxCourseCorrectionSeconds));
				courseCorrect ();
			}
		}

		IEnumerator delayThenFireMissile()
		{
			while (true)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(minMissilePause/level, maxMissilePause/level));
				fireWeaponSignal.Dispatch ();
			}
		}

		void courseCorrect()
		{
			motiveForce = new Vector3 (
				UnityEngine.Random.Range (minXSpeed * level, maxXSpeed * level),
				0f,
				UnityEngine.Random.Range (minYSpeed, maxYSpeed)
			);
			rigidbody.AddRelativeForce (motiveForce);
		}
	}
}

