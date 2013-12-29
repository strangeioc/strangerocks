using System;
using UnityEngine;
using System.Collections;

namespace strange.examples.strangerocks
{
	public interface IRoutineRunner
	{
		Coroutine StartCoroutine(IEnumerator method);
	}
}

