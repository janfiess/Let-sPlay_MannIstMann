using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDefinition : ScriptableObject
{
    public float Cooldown;

    public float range;
    public float minDamage;
    public float maxDamage;
    public float criticalMultiplier;
    public float criticalChance;

    public Attack CreateAttack(CharacterStats wielderStats, CharacterStats defenderStats)
    {
        float coreDamage = Random.Range(minDamage, maxDamage + 1);

        coreDamage += wielderStats.characterDefinition.baseDamage;
        if (defenderStats != null)
        {
            coreDamage -= defenderStats.GetResistance();
        }
      
        bool isCritical = Random.value < criticalChance;

        if (isCritical)
            coreDamage *= criticalMultiplier;

        return new Attack((int)coreDamage, isCritical);
    }
}