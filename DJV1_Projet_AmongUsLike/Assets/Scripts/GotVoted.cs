using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotVoted : MonoBehaviour
{
    [SerializeField] private Canvas meetingUI;
    [SerializeField] private EnemyBehaviour myself;

    private void OnEnable()
    {
        if (myself && !myself.gameObject.activeSelf) gameObject.SetActive(false);
    }

    public void EliminateMe()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        meetingUI.gameObject.SetActive(false);
        if (myself) myself.Die();
    }
}
