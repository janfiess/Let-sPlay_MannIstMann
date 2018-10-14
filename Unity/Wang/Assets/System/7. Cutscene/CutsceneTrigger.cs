using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;


public class CutsceneTrigger : MonoBehaviour {
    public PlayableDirector cutsceneDirector;
    public NavMeshAgent playerAgent;
    public float agentPause = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        playerAgent.isStopped = true;
        cutsceneDirector.Play();
        StartCoroutine(PauseTimer());
    }

    private IEnumerator PauseTimer ()
    {
        yield return new WaitForSeconds(agentPause);
        playerAgent.isStopped = false;
    }

}
