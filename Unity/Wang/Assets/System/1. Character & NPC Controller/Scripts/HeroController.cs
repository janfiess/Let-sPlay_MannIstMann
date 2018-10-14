using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HeroController : MonoBehaviour, IDestructible
{
    Animator animator;
    NavMeshAgent agent;
    
    public AttackDefinition demoAttack;
    public AOESpell StompAttack;
    
    private float timeSinceLastStomp;
    private GameObject attackTarget;
    private CharacterStats CharacterStats;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        CharacterStats = GetComponent<CharacterStats>();
        
        timeSinceLastStomp = float.MinValue;
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void AttackTarget(GameObject target)
    {
        if (target != null)
        {
            var currentWeapon = CharacterStats.GetCurrentWeapon();

            if (currentWeapon != null)
            {
                StopAllCoroutines();

                agent.isStopped = false;
                attackTarget = target;
                StartCoroutine(PursueAndAttackTarget(currentWeapon));
            }
        }
    }
    
    public void Hit()
    {
        if(attackTarget != null)
            CharacterStats.GetCurrentWeapon().ExecuteAttack(gameObject,attackTarget);
    }
    
    public void Stomp()
    {
        StompAttack.Cast(gameObject, LayerMask.NameToLayer("HeroSpell"));
    }
    
    private IEnumerator PursueAndAttackTarget(Weapon currentWeapon )
    {
        //agent.isStopped = false;
		
        while (Vector3.Distance(transform.position, attackTarget.transform.position) > currentWeapon.range)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        
        //agent.isStopped = true;
        
        transform.LookAt(attackTarget.transform);
        animator.SetTrigger("Attack");
    }

    public void DoStomp(Vector3 destination)
    {
        bool stompIsOnCooldown = Time.time - timeSinceLastStomp < StompAttack.Cooldown;
        if (!stompIsOnCooldown)
        {
            StopAllCoroutines();

            StartCoroutine(GoToTargetAndStomp(destination));
        }
    }

    private IEnumerator GoToTargetAndStomp(Vector3 destination)
    {
        agent.isStopped = false;

        while (Vector3.Distance(transform.position, destination) > StompAttack.range)
        {
            agent.destination = destination;

            yield return null;
        }

        timeSinceLastStomp = Time.time;
        animator.SetTrigger("Stomp");
    }


    public void OnDestruction(GameObject destroyer)
    {
        gameObject.SetActive(false);
    }
}
