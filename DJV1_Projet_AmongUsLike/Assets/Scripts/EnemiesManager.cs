using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private float taskTimer = 15f;
    private float _timer;
    
    private GameResources _gr;
    private EnemyBehaviour[] _enemies;
    void Start()
    {
        _gr = GameResources.Instance;
        _timer = taskTimer;
        _enemies = GetComponentsInChildren<EnemyBehaviour>();
    }
    
    void Update()
    {
        if (_timer <= 0)
        {
            foreach (EnemyBehaviour enemy in _enemies)
            {
                enemy.agent.SetDestination(_gr.GetRandomTask().position);
            }

            _timer = taskTimer;
        }
        else _timer -= Time.deltaTime;
    }
}
