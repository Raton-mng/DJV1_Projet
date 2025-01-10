using System;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private void Start()
    {
        CallMeeting.MeetingTriggered.AddListener(DestroyCorpse);
    }

    private void DestroyCorpse()
    {
        Destroy(gameObject);
    }
}
