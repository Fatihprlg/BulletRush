using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFind : MonoBehaviour
{
    [SerializeField] float actionDistance = 7f;
    [SerializeField] float agentSpeed = 5f;
    GameObject target;
    NavMeshAgent agent;
    bool activated = false;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < actionDistance) activated = true;
        if (activated)
        {
            agent.destination = target.transform.position;
            agent.speed = agentSpeed;
        }
    }
}
