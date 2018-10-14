using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedRagdoll : MonoBehaviour, IDestructible
{
	public Ragdoll ragdollObject;
	public float Force;
	public float Lift;

	public bool DestroyOriginal = true;
	
	public void OnDestruction(GameObject destroyer)
	{
		var ragdoll = Instantiate(ragdollObject, transform.position, transform.rotation);

		var vectorFromDestroyer = transform.position - destroyer.transform.position;
		vectorFromDestroyer.Normalize();
		vectorFromDestroyer.y += Lift;
		
		ragdoll.ApplyForce(vectorFromDestroyer * Force);
		
		if(DestroyOriginal)
			Destroy(gameObject);
		else
			gameObject.SetActive(false);
	}
}
