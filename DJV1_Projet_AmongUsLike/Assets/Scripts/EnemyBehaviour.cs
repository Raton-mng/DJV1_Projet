using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float taskTimer = 2f;
    private bool _hasArrived;
    public NavMeshAgent agent;
    public UnityAction TaskCompleted;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _hasArrived = false;
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !_hasArrived)
        {
            StartCoroutine(DestinationReached());
            _hasArrived = true;
        }
        else if (_hasArrived && agent.remainingDistance > agent.stoppingDistance) _hasArrived = false;
    }

    private IEnumerator DestinationReached()
    {
        yield return new WaitForSeconds(taskTimer);
        TaskCompleted.Invoke();
    }
}
