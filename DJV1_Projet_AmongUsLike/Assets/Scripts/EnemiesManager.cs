using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private GameResources _gr;
    
    private EnemyBehaviour[] _enemies;
    private int _enemiesNumber;
    private int _tasksDone;
    
    void Start()
    {
        _gr = GameResources.Instance;
        _enemies = GetComponentsInChildren<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in _enemies)
        {
            enemy.TaskCompleted += OnTaskCompleted;
        }
        _enemiesNumber = _enemies.Length;
        _tasksDone = 0;
        
        Dispatch();
    }
    
    private void Dispatch()
    {
        Vector3 pos1, pos2, pos3;
        (pos1, pos2, pos3) = SelectDestinations();
        foreach (EnemyBehaviour enemy in _enemies)
        {
            switch (Random.Range(0, 3))
            {
                case 0 :
                    enemy.agent.SetDestination(pos1);
                    break;
                case 1 :
                    enemy.agent.SetDestination(pos2);
                    break;
                default:
                    enemy.agent.SetDestination(pos3);
                    break;
            }
        }
    }

    private (Vector3, Vector3, Vector3) SelectDestinations()
    {
        Vector3 pos1, pos2, pos3;
        pos1 = _gr.GetRandomTask().position;
        
        do pos2 = _gr.GetRandomTask().position;
        while (pos2 == pos1);
        
        do pos3 = _gr.GetRandomTask().position;
        while (pos3 == pos1 || pos3 == pos2);

        return (pos1, pos2, pos3);
    }

    private void OnTaskCompleted()
    {
        _tasksDone += 1;
        Debug.Log(_tasksDone);
        if (_tasksDone == _enemiesNumber)
        {
            Dispatch();
            _tasksDone = 0;
        }
    }
}
