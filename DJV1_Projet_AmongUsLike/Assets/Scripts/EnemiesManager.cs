using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    private GameResources _gr;
    
    private HashSet<EnemyBehaviour> _enemies;
    private int _enemiesNumber;
    private int _tasksDone;
    
    void Start()
    {
        _gr = GameResources.Instance;
        EnemyBehaviour[] enemiesTab = GetComponentsInChildren<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemiesTab)
        {
            enemy.TaskCompleted += OnTaskCompleted;
        }
        _enemiesNumber = enemiesTab.Length;
        _tasksDone = 0;
        
        _enemies = new HashSet<EnemyBehaviour>(enemiesTab);
        
        Dispatch();
    }
    
    private void Dispatch()
    {
        /*Vector3 pos1, pos2, pos3;
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
        }*/

        HashSet<EnemyBehaviour> nonSelectedEnemies = new HashSet<EnemyBehaviour>(_enemies);
        HashSet<Vector3> destinations = new HashSet<Vector3>();
        while (nonSelectedEnemies.Count > 0)
        {
            Vector3 destination;
            do destination = _gr.GetRandomTask().position;
            while (destinations.Contains(destination));
            destinations.Add(destination);
            
            int numberShouldBeAtTask = (nonSelectedEnemies.Count < 6 ? nonSelectedEnemies.Count : 3);
            int numberPresentAtTask;
            
            for (numberPresentAtTask = 0;
                 numberPresentAtTask < Mathf.Min(3, numberShouldBeAtTask);
                 numberPresentAtTask++)
            {
                EnemyBehaviour enemy = SelectNewEnemy(nonSelectedEnemies);
                enemy.agent.SetDestination(destination);
            }
        }
    }

    /*private (Vector3, Vector3, Vector3) SelectDestinations()
    {
        Vector3 pos1, pos2, pos3;
        pos1 = _gr.GetRandomTask().position;
        
        do pos2 = _gr.GetRandomTask().position;
        while (pos2 == pos1);
        
        do pos3 = _gr.GetRandomTask().position;
        while (pos3 == pos1 || pos3 == pos2);

        return (pos1, pos2, pos3);
    }*/

    private EnemyBehaviour SelectNewEnemy(HashSet<EnemyBehaviour> nonSelectedEnemies)
    {
        EnemyBehaviour newEnemy = nonSelectedEnemies.ElementAt(Random.Range(0, nonSelectedEnemies.Count));
        nonSelectedEnemies.Remove(newEnemy);
        return newEnemy;
    }

    private void OnTaskCompleted()
    {
        _tasksDone += 1;
        if (_tasksDone == _enemiesNumber)
        {
            Dispatch();
            _tasksDone = 0;
        }
    }
}
