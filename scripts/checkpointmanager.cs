using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class checkpointmanager : MonoBehaviour
{
    private checkPoint[] checkPoints; // 4 checkpoints of the game
    public static checkpointmanager Instance;
    public Action OnReachedCheckpoint;
    private void Awake()
    {
        if (Instance != null) // set up singleton
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        checkPoints = GetComponentsInChildren<checkPoint>(); // get 4 checkpoints in children
    }


    public void HandleCheckpointReached()
    {
        soundManager.Instance.playGamesound(gameSounds.checkpoint); // play checkpoint sound
        OnReachedCheckpoint?.Invoke();
    }


    public checkPoint getLastPassedCheckPoint()
    {
        if (checkPoints[0].passed)
        {
            return checkPoints.LastOrDefault(t => t.passed); // lambda to see last passed checkpoint

        }
        else
        {
            return checkPoints[0]; // return first checkpoint
        }
    }
}
