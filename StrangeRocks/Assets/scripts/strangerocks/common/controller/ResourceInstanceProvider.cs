//Pools require an instance provider to instantiate instances.
//Quite often we can leave this job to the InjectionBinder...but when prefabs
//are involved, we need to override the default behavior and do some of the
//work ourselves.

using System;
using strange.framework.api;
using UnityEngine;

namespace strange.examples.strangerocks
{
	public class ResourceInstanceProvider : IInstanceProvider
	{
		//The GameObject instantiated from the prefab
		GameObject prototype;

		//The name of the resource in Unity's resources folder
		private string resourceName;
		//The render layer to which the GameObjects will be assigned
		private int layer;
		//An id tacked on to the name to make it easier to track individual instances
		private int id = 0;

		//This provider is instantiated multiple times in GameContext.
		//Each time, we provide the name of the prefab we're loading from
		//a resources folder, and the layer to which the resulting instance
		//
		public ResourceInstanceProvider(string name, int layer)
		{
			resourceName = name;
			this.layer = layer;
		}

		#region IInstanceProvider implementation
		//Generate a typed instance
		public T GetInstance<T> ()
		{
			object instance = GetInstance (typeof(T));
			T retv = (T) instance;
			return retv;
		}

		//Generate an untyped instance
		public object GetInstance (Type key)
		{
			if (prototype == null)
			{
				//Get the resource from Unity
				prototype = Resources.Load<GameObject> (resourceName);
				prototype.transform.localScale = Vector3.one;
			}

			//Copy the prototype
			GameObject go = GameObject.Instantiate (prototype) as GameObject;
			go.layer = layer;
			go.name = resourceName + "_" + id++;

			return go;
		}
		#endregion
	}
}

