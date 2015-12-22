using UnityEngine;
using System.Collections;

public abstract class SimpleSingleton<T> where T : class,new(){
	protected static T mInstance;
	public static T Instance{
		get {
			if (mInstance == null) mInstance = new T();
			return mInstance;
		}
	}
}
