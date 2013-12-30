//The "View" for an Enemy. This MonoBehaviour is attached to the enemy prefab inside Unity.

using System;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.Collections;

namespace strange.examples.strangerocks.game
{
	public class EnemyView : View
	{
		//These Signals inform the Mediator that certain events have occurred
		internal Signal exitScreenSignal = new Signal ();
		internal Signal fireWeaponSignal = new Signal ();

		public Renderer mainRenderer;

		//Settable/configurable values in Unity
		public float rotationSpeed = .1f;
		public float minXSpeed = 100f;
		public float maxXSpeed = 200f;
		public float minYSpeed = -50f;
		public float maxYSpeed = 50f;
		public float minCourseCorrectionSeconds = 2f;
		public float maxCourseCorrectionSeconds = 10f;
		public float minMissilePause = 1f;
		public float maxMissilePause = 3f;

		//Set from CreateEnemyCommand to determine this Enemy's level (higher levels are faster, smaller, worth more points)
		public int level;

		//The force driving this ship
		private Vector3 motiveForce;

		//Initialize called by the Mediator. Init is a little like
		//Start()...but by calling it from the Mediator's OnRegister(),
		//we know the Mediator is in place before doing anything important.
		public void Init(int level)
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
			//Some silly rotation for effect
			mainRenderer.transform.Rotate (motiveForce * rotationSpeed);

			//Have we flown offscreen?
			if (gameObject.activeSelf && !mainRenderer.isVisible)
			{
				StopAllCoroutines ();
				exitScreenSignal.Dispatch ();
			}
		}

		//Pause for a random number of seconds before changing course
		IEnumerator delayThenCourseCorrect()
		{
			while (true)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(minCourseCorrectionSeconds, maxCourseCorrectionSeconds));
				courseCorrect ();
			}
		}

		//Pause for a random number of seconds before firing a missile
		IEnumerator delayThenFireMissile()
		{
			while (true)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(minMissilePause/level, maxMissilePause/level));
				fireWeaponSignal.Dispatch ();
			}
		}

		//Change direction
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

