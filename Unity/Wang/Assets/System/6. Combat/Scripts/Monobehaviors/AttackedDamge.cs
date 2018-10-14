using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AttackedDamge : MonoBehaviour, IAttackable
{
	private CharacterStats stats;

	private void Awake()
	{
		stats = GetComponent<CharacterStats>();
	}

	public void OnAttack(GameObject attacker, Attack attack)
	{
		stats.TakeDamage(attack.Damage);

		if (stats.GetHealth() <= 0)
		{
			// Trigger destructibles
			var destructibles = GetComponents(typeof(IDestructible));
			foreach (var d in destructibles)
			{
				((IDestructible)d).OnDestruction(attacker);
			}
		}
	}
}