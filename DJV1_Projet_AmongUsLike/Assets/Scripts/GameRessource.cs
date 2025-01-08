using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Rooms
{
    Cafet,
    Admin,
    Storage,
    Weapon,
    O2,
    Navigation,
    Shield,
    Comms,
    UpperEngine,
    LowerEngine,
    MedBay,
    Electrical,
    Security,
    Reactor
}

public class GameResources : MonoBehaviour
{
    public static GameResources Instance;
    
    [SerializeField] private TasksPositions[] tasks;

    private Dictionary<Rooms, HashSet<Rooms>> _neighborRoom;

    private void Awake()
    {
        _neighborRoom = new Dictionary<Rooms, HashSet<Rooms>>();
        
        //voisins de cafet
        HashSet<Rooms> neighbors = new HashSet<Rooms>();
        neighbors.Add(Rooms.Admin);
        neighbors.Add(Rooms.Storage);
        neighbors.Add(Rooms.Weapon);
        neighbors.Add(Rooms.UpperEngine);
        neighbors.Add(Rooms.MedBay);
        _neighborRoom.Add(Rooms.Cafet, new HashSet<Rooms> (neighbors));
        
        //voisins de admin
        neighbors.Clear();
        neighbors.Add(Rooms.Cafet);
        neighbors.Add(Rooms.Storage);
        _neighborRoom.Add(Rooms.Admin, new HashSet<Rooms> (neighbors));
        
        //voisins de weapons
        neighbors.Clear();
        neighbors.Add(Rooms.Cafet);
        neighbors.Add(Rooms.O2);
        neighbors.Add(Rooms.Navigation);
        neighbors.Add(Rooms.Shield);
        _neighborRoom.Add(Rooms.Weapon, new HashSet<Rooms> (neighbors));
        
        //voisins de O2
        neighbors.Clear();
        neighbors.Add(Rooms.Navigation);
        neighbors.Add(Rooms.Weapon);
        neighbors.Add(Rooms.Shield);
        _neighborRoom.Add(Rooms.O2, new HashSet<Rooms> (neighbors));
        
        //voisins de navigation
        neighbors.Clear();
        neighbors.Add(Rooms.Weapon);
        neighbors.Add(Rooms.O2);
        neighbors.Add(Rooms.Shield);
        _neighborRoom.Add(Rooms.Navigation, new HashSet<Rooms> (neighbors));
        
        //voisins de shield
        neighbors.Clear();
        neighbors.Add(Rooms.Comms);
        neighbors.Add(Rooms.O2);
        neighbors.Add(Rooms.Navigation);
        neighbors.Add(Rooms.Weapon);
        neighbors.Add(Rooms.Storage);
        _neighborRoom.Add(Rooms.Shield, new HashSet<Rooms> (neighbors));
        
        //voisins de Comms
        neighbors.Clear();
        neighbors.Add(Rooms.Storage);
        neighbors.Add(Rooms.Shield);
        _neighborRoom.Add(Rooms.Comms, new HashSet<Rooms> (neighbors));
        
        //voisins de storage
        neighbors.Clear();
        neighbors.Add(Rooms.Cafet);
        neighbors.Add(Rooms.Comms);
        neighbors.Add(Rooms.Admin);
        neighbors.Add(Rooms.Shield);
        neighbors.Add(Rooms.Electrical);
        neighbors.Add(Rooms.LowerEngine);
        _neighborRoom.Add(Rooms.Storage, new HashSet<Rooms> (neighbors));
        
        //voisins de electrical
        neighbors.Clear();
        neighbors.Add(Rooms.Storage);
        neighbors.Add(Rooms.LowerEngine);
        _neighborRoom.Add(Rooms.Electrical, new HashSet<Rooms> (neighbors));
        
        //voisins de lower engine
        neighbors.Clear();
        neighbors.Add(Rooms.Electrical);
        neighbors.Add(Rooms.Storage);
        neighbors.Add(Rooms.Security);
        neighbors.Add(Rooms.UpperEngine);
        neighbors.Add(Rooms.Reactor);
        _neighborRoom.Add(Rooms.LowerEngine, new HashSet<Rooms> (neighbors));
        
        //voisins de security
        neighbors.Clear();
        neighbors.Add(Rooms.LowerEngine);
        neighbors.Add(Rooms.Reactor);
        neighbors.Add(Rooms.UpperEngine);
        _neighborRoom.Add(Rooms.Security, new HashSet<Rooms> (neighbors));
        
        //voisins de reactor
        neighbors.Clear();
        neighbors.Add(Rooms.Security);
        neighbors.Add(Rooms.LowerEngine);
        neighbors.Add(Rooms.UpperEngine);
        _neighborRoom.Add(Rooms.Reactor, new HashSet<Rooms> (neighbors));
        
        //voisins de upper engine
        neighbors.Clear();
        neighbors.Add(Rooms.Security);
        neighbors.Add(Rooms.Reactor);
        neighbors.Add(Rooms.LowerEngine);
        neighbors.Add(Rooms.Cafet);
        neighbors.Add(Rooms.MedBay);
        _neighborRoom.Add(Rooms.UpperEngine, new HashSet<Rooms> (neighbors));
        
        //voisins de med bay
        neighbors.Clear();
        neighbors.Add(Rooms.Cafet);
        neighbors.Add(Rooms.UpperEngine);
        _neighborRoom.Add(Rooms.MedBay, new HashSet<Rooms> (neighbors));

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public TasksPositions GetRandomTask()
    {
        return tasks[Random.Range(0, tasks.Length)];
    }

    public HashSet<Rooms> GetNeighbours(Rooms myRoom)
    {
        return _neighborRoom[myRoom];
    }
}
