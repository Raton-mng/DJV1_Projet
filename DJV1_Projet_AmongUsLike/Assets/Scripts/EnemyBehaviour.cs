using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float taskTimer = 2f;
    private EnemiesManager _enemiesManager;
    
    public bool hasKilled;
    public bool hasArrived;
    public bool hasFinishedTask;
    public Rooms currentRoom;
    
    public NavMeshAgent agent;

    public bool isImposter;

    [SerializeField] private Corpse corpse;
    [SerializeField] private MeshRenderer myRenderer;

    private void Awake()
    {
        hasKilled = false;
        hasArrived = true;
        hasFinishedTask = false;
        
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
            if (isImposter && !hasKilled && PlayerControl.CurrentRoom != currentRoom && !GameResources.Instance.GetNeighbours(currentRoom).Contains(PlayerControl.CurrentRoom))
            {
                Kill();
            }

            if (!hasArrived)
            {
                StartCoroutine(DestinationReached());
                hasArrived = true;
            }
        }
    }

    private IEnumerator DestinationReached()
    {
        yield return new WaitForSeconds(taskTimer);
        _enemiesManager.TaskCompleted();
        hasFinishedTask = true;
    }

    private void Kill()
    {
        EnemyBehaviour target = _enemiesManager.GetCloseCrewmate(currentRoom);
        
        if (target) target.Die();
        //si target est null, la cible n'est pas encore arrivée ou a deja ete tuee
        
        hasKilled = true;
    }

    public void Die()
    {
        if (hasFinishedTask) _enemiesManager.TaskCanceled();
        _enemiesManager.SomeoneDied(this);
        gameObject.SetActive(false);

        Corpse myCorpse = Instantiate(corpse, transform.position, transform.rotation);
        myCorpse.gameObject.SetActive(true);
        myCorpse.meshRenderer.material.color = myRenderer.material.color;
    }
}
