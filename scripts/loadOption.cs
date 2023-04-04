using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class loadOption : MonoBehaviour
{
    private TextMeshProUGUI text; // text to store player name
    private playButton button; //button to control loading
    public PlayerProgress prog; // this instances player progress
    public static PlayerProgress loadProg; // a static load Progress.  
    private const string GAME_SCENE = "GameScene"; // name of scene where the game is played
    public void Init()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponentInChildren<playButton>(true);
        button.gameObject.SetActive(true);
        text.text = prog.name;
        button.onClick.AddListener(() => SetupLoad()); // add listener to button click to trigger level load
    }

    private void OnDisable()
    {
        if (button != null)
            button.onClick.RemoveAllListeners();
    }

    void SetupLoad()
    {
        loadProg = prog; // set static prog this this load options prog
        Loading.LevelToLoad = GAME_SCENE; // set the level to load
        playButton.clicked = true; // tell the game state machine to transition to play as play has been clicked
    }
}
