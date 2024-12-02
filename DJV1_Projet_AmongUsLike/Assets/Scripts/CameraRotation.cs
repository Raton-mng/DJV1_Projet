using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float verticalRotationSpeed;
    [SerializeField] private PlayerInput plInput;
    
    private InputAction _mouseMove;
    
    private float _verticalRotation;

    private void Awake()
    {
        _mouseMove = plInput.actions["Look"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 input = _mouseMove.ReadValue<Vector2>();
        _verticalRotation = Mathf.Clamp(_verticalRotation - input.y * verticalRotationSpeed * Time.deltaTime, -30, 60);

        transform.localRotation = Quaternion.AngleAxis(_verticalRotation, Vector3.right);
    }
}
