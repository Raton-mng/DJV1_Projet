using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    public static Rooms CurrentRoom;
    
    private InputAction _playerMove;
    private InputAction _mouseMove;

    private Vector3 _movement;
    private float _rotationAngle;

    private void Awake()
    {
        CurrentRoom = Rooms.Cafet;
        
        PlayerInput plInput = GetComponent<PlayerInput>();
        _playerMove = plInput.actions["Move"];
        _mouseMove = plInput.actions["Look"];
    }

    private void Update()
    {
        Transform tr = transform;
        
        Vector3 moveInput = _playerMove.ReadValue<Vector2>();
        _movement = (tr.forward * moveInput.y + tr.right * moveInput.x).normalized * (moveSpeed * Time.fixedDeltaTime);
        
        Vector2 mouseInput = _mouseMove.ReadValue<Vector2>();
        _rotationAngle = (_rotationAngle + mouseInput.x * rotationSpeed * Time.fixedDeltaTime) % 360;
    }

    private void FixedUpdate()
    {

        Transform tr = transform;

        tr.position += _movement;
        
        tr.localRotation = Quaternion.AngleAxis(_rotationAngle, Vector3.up);
    }
}
