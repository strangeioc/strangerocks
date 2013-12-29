using System;
using strange.extensions.context.api;
using UnityEngine;
using System.Collections;
using strange.extensions.injector.api;

namespace strange.examples.strangerocks
{
	[Implements(typeof(IRoutineRunner), InjectionBindingScope.CROSS_CONTEXT)]
	public class RoutineRunner : IRoutineRunner
	{
		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{ get; set; }

		private MonoBehaviour mb;

		[PostConstruct]
		public void PostConstruct()
		{
			mb = contextView.AddComponent<MonoBehaviour> ();
		}

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return mb.StartCoroutine(routine);
		}
	}
}

