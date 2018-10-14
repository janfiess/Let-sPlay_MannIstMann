using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDebug : MonoBehaviour, IDestructible 
{
	public void OnDestruction(GameObject destroyer)
	{
		Debug.LogFormat("{0} was destroyed by {1}", this.gameObject.name, destroyer.name);
	}
}
