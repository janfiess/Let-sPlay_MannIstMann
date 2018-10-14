using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
	public Rigidbody Core;
	public float timeToLive;
	
	private void Start()
	{
		Destroy(gameObject, timeToLive);
	}

	public void ApplyForce(Vector3 force)
	{
		Core.AddForce(force);
	}
}
