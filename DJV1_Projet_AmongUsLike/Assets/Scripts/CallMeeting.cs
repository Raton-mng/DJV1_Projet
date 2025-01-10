using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CallMeeting : MonoBehaviour
{
    [SerializeField] private PlayerInput plInput;
    private InputAction _call;
    private bool _isInside;

    [SerializeField] private Canvas meetingUI;

    public static UnityEvent MeetingTriggered;

    private void Awake()
    {
        MeetingTriggered = new UnityEvent();
        _isInside = false;
        _call = plInput.actions["CallMeeting"];
    }

    private void Update()
    {
        if (_call.WasPerformedThisFrame() && _isInside)
        {
            MeetingTriggered.Invoke();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            meetingUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerControl player))
        {
            _isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerControl player))
        {
            _isInside = false;
        }
    }
}
