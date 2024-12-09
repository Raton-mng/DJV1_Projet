using UnityEngine;
using Random = UnityEngine.Random;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance;
    
    [SerializeField] private TasksPositions[] tasks;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public TasksPositions GetRandomTask()
    {
        return tasks[Random.Range(0, tasks.Length)];
    }
}
