using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class checkPoint : MonoBehaviour
{
    public bool passed;
    public Sprite passedSprite; // green flag
    public static checkPoint Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerStateMachine>(); // checks for player
        if (player != null && !passed)  // if new checkpoint
        {
            passed = true;
            checkpointmanager.Instance.HandleCheckpointReached(); // tell checkpoint manager player has passed
        }
    }
}
