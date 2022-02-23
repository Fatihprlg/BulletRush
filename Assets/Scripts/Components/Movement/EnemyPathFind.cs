using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFind : MonoBehaviour
{
    [SerializeField] float actionDistance = 7f;
    [SerializeField] float agentSpeed = 5f;
    bool activated = false;
    
    GameObject target;
    NavMeshAgent agent;
    
    [HideInInspector] public bool goneBehind = false;
    public bool isBigEnemy;

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
            EnemyMovement();
        }
    }

    void EnemyMovement()
    {
        if (isBigEnemy)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < actionDistance/2)
            {
                transform.LookAt(new Vector3(target.transform.position.x, 2, target.transform.position.z));
                if (!goneBehind)
                {
                    agent.ResetPath();
                    transform.RotateAround(target.transform.position, Vector3.up, 0.5f);
                }
                else
                {
                    agent.destination = target.transform.position;
                }
                
            }
            else if (Vector3.Distance(transform.position, target.transform.position) < actionDistance)
            {
                agent.destination = target.transform.position;
            }
        }
        else
        {
            transform.LookAt(new Vector3(target.transform.position.x, 1.5f, target.transform.position.z));
            agent.destination = target.transform.position;
            agent.speed = agentSpeed;
        }
    }

}
