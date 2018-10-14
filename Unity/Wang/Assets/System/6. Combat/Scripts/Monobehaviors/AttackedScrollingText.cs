using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedScrollingText : MonoBehaviour, IAttackable
{
	public ScrollingText Text;
	public Color TextColor;
    
	public void OnAttack(GameObject attacker, Attack attack)
	{
		var text = string.Format("{0}", attack.Damage);
		var color = TextColor;

		var scrollingText = Instantiate(Text, transform.position, Quaternion.identity);
		scrollingText.SetText(text, color, attack.IsCritical ? 1.25f : 1.0f);
	}
}