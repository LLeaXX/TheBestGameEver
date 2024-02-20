using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    public PlayerController player;

    public List<Transform> patrolPoints;

    private bool _isPlayerNoticed;

    public float viewAngle;

    // Start is called before the first frame update
    private void Start()
    {
        InitComponentLinks();
        PickNewPatrolPoint();
    }

    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    { 
        PatrolUptade();
        ChaseUpdate();
        NoticePlayerUpdate();
    }
    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }

    }  

    private void PatrolUptade()
    {
        if (!_isPlayerNoticed)
        {

            if (_navMeshAgent.remainingDistance == 0)
            {
                PickNewPatrolPoint();
            }
        }
    }

    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }
}
