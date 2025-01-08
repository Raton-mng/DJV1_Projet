using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MainRooms : MonoBehaviour
{
    [SerializeField] private Rooms room; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerControl player))
        {
            PlayerControl.CurrentRoom = room;
            return;
        }
        
        if (other.TryGetComponent(out EnemyBehaviour enemy))
        {
            enemy.currentRoom = room;
        }
    }
}
