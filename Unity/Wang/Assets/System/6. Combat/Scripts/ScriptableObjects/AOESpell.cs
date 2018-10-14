using UnityEngine;

[CreateAssetMenu(fileName = "AOESpell.asset", menuName = "AOE Spell")]
public class AOESpell : AttackDefinition
{
    public AOE aoeObject;
    public float radius;
    
    public void Cast(GameObject caster, int layer)
    {
        Cast(caster, caster.transform.position, layer);
    }
    
    public void Cast(GameObject caster, Vector3 position, int layer)
    {
        AOE aoe = Instantiate(aoeObject, position, Quaternion.identity);
        aoe.Fire(this, caster, layer);
    }
}