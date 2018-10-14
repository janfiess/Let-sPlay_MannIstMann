using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedForce : MonoBehaviour, IAttackable
{

	public float ForceToAdd;
	private Rigidbody targetBody;

	private void Awake()
	{
		targetBody = GetComponent<Rigidbody>();
	}

	public void OnAttack(GameObject attacker, Attack attack)
	{
		var forceDirection = transform.position - attacker.transform.position;
		forceDirection.y += .5f;
		forceDirection.Normalize();
		
		targetBody.AddForce(forceDirection * ForceToAdd);
	}
}
