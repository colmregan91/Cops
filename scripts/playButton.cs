using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : Button
{

    public static bool clicked;
    public static bool pressed =>  clicked;
    private const string GAME_SCENE = "GameScene"; // name of game scene
    public void StartNewGame()
    {
        Loading.LevelToLoad = GAME_SCENE; // triggers loading state to load the game scene 
        clicked = true; // triggers state transition to loading. 
    }

    protected override void OnDisable() // clean up
    {
        base.OnDisable();
        clicked = false;
        onClick.RemoveAllListeners();
    }
}
