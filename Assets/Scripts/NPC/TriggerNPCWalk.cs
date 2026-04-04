using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNPCWalk : MonoBehaviour
{
    public NpcMovement npcMovement;
    public int[] guideTargetSteps;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !hasTriggered)
        {
            hasTriggered = true;
            npcMovement.StartGuide(guideTargetSteps[0]);
        }
    }
}
