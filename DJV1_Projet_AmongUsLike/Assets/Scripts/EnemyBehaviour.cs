using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float taskTimer = 2f;
    private EnemiesManager _enemiesManager;
    
    private bool _hasKilled;
    private bool _hasArrived;
    public Rooms currentRoom;
    
    public NavMeshAgent agent;
    
    public UnityAction TaskCompleted;

    public bool isImposter;

    private void Awake()
    {
        _hasKilled = false;
        _hasArrived = true;
        
        currentRoom = Rooms.Cafet;
        
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _enemiesManager = EnemiesManager.Instance;
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (isImposter && !_hasKilled && PlayerControl.CurrentRoom != currentRoom && !GameResources.Instance.GetNeighbours(currentRoom).Contains(PlayerControl.CurrentRoom))
            {
                Debug.Log("Je suis imposteur : " + name + "; Je suis en " + currentRoom);
                Kill();
            }

            if (!_hasArrived)
            {
                StartCoroutine(DestinationReached());
                _hasArrived = true;
            }
        }
        else if (_hasArrived && agent.remainingDistance > agent.stoppingDistance)
        {
            _hasArrived = false;
            _hasKilled = false;
        }
    }

    private IEnumerator DestinationReached()
    {
        yield return new WaitForSeconds(taskTimer);
        Debug.Log("I finished : " + gameObject);
        TaskCompleted.Invoke();
    }

    private void Kill()
    {
        EnemyBehaviour target = _enemiesManager.GetCloseCrewmate(currentRoom);
        
        if (target) target.Die();
        //si target est null, la cible n'est pas encore arrivée ou a deja ete tuee
        
        _hasKilled = true;
    }

    private void Die()
    {
        _enemiesManager.SomeoneDied(this);
        Debug.Log("i died : " + gameObject);
        gameObject.SetActive(false);
    }
}
