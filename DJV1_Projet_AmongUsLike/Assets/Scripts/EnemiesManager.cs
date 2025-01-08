using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    
    private GameResources _gr;
    
    private HashSet<EnemyBehaviour> _enemies;
    private int _enemiesNumber;
    private int _tasksDone;
    
    private static int _numberOfImposter;
    [SerializeField] private int numberOfImposterWanted = 2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        _numberOfImposter = 0;
        
        EnemyBehaviour[] enemiesTab = GetComponentsInChildren<EnemyBehaviour>();
        _enemiesNumber = enemiesTab.Length;

        int parcoursCpt;
        for (parcoursCpt = _enemiesNumber - 1; parcoursCpt >= 0; parcoursCpt--)
        {
            if (Random.Range(0, parcoursCpt + 1) < numberOfImposterWanted - _numberOfImposter)
            {
                enemiesTab[parcoursCpt].isImposter = true;
                _numberOfImposter++;
            }
            else enemiesTab[parcoursCpt].isImposter = false;
            enemiesTab[parcoursCpt].TaskCompleted += OnTaskCompleted;
        }
        
        _tasksDone = 0;
        
        _enemies = new HashSet<EnemyBehaviour>(enemiesTab);
    }

    void Start()
    {
        _gr = GameResources.Instance;
        
        Dispatch();
    }
    
    private void Dispatch()
    {

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

    private EnemyBehaviour SelectNewEnemy(HashSet<EnemyBehaviour> nonSelectedEnemies)
    {
        EnemyBehaviour newEnemy = nonSelectedEnemies.ElementAt(Random.Range(0, nonSelectedEnemies.Count));
        nonSelectedEnemies.Remove(newEnemy);
        return newEnemy;
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

    public EnemyBehaviour GetCloseCrewmate(Rooms localRoom)
    {
        foreach (EnemyBehaviour enemy in _enemies)
        {
            if (!enemy.isImposter && enemy.currentRoom == localRoom) return enemy;
        }
        
        return null;
    }

    public void SomeoneDied(EnemyBehaviour enemy)
    {
        _enemies.Remove(enemy);
        _enemiesNumber -= 1;
        if (enemy.isImposter) _numberOfImposter -= 1;
    }
}
