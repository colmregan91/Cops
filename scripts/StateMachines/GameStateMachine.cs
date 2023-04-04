using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private static string PlayerName = "";

    public static GameStateMachine _instance; // singleton instance
    private Play play;
    private Pause pause;
    private Loading loading;
    private MainMenu mainmenu;
    private GameObject loadingImage;
    private StateMachine StateMachine; // state machine ref
    public static Action<Istate> OnGameStateChanged;
    private void Awake()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/saves/"); // create a directory to store saves

        if (_instance != null) // destroy instance if its not equal to this (Singleton pattern)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        play = new Play(); // implementation of our states
        pause = new Pause();
        loading = new Loading();
        mainmenu = new MainMenu();
        StateMachine = new StateMachine();

        OnGameStateChanged += OnLoading; // subscribe the onLoading method to the game state change event
        StateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state); // subscribe the invokation if OnGameStateChanged event to the state machine on game state changed event
        StateMachine.SetState(mainmenu); // set the default state to the main menu
    }

    private void OnLoading(Istate obj)
    {
        if (obj is Loading) // if new state is loading
        {
            loadingImage = FindObjectOfType<loadImage>(true).gameObject; // find loading image
            loadingImage.SetActive(true); // turn it on
        }

    }

    public static string GetPlayerName()
    {
        return PlayerName;
    }
    public static void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    private void OnDisable()
    {
        OnGameStateChanged -= OnLoading;
    }


    private void Start()
    {
        StateMachine.AddTransition(mainmenu, loading, () => playButton.pressed); // go from main menu to loading if play button pressed
        StateMachine.AddTransition(loading, play, () => loading.isFinished && !Loading.LevelToLoad.Equals("MainMenu")); // loading to play 
        StateMachine.AddTransition(play, pause, () => Input.GetKeyDown(KeyCode.Escape));// play to pause is esc button is pressed
        StateMachine.AddTransition(pause, play, () => Input.GetKeyDown(KeyCode.Escape));// pause back to play
        StateMachine.AddTransition(pause, loading, () => RestartButton.pressed);
        StateMachine.AddTransition(play, loading, () => RestartButton.pressed);
        StateMachine.AddTransition(play, loading, () => playButton.pressed);
        StateMachine.AddTransition(play, loading, () => ReturnButton.pressed);
        StateMachine.AddTransition(pause, loading, () => ReturnButton.pressed);
        StateMachine.AddTransition(loading, mainmenu, () => loading.isFinished && Loading.LevelToLoad.Equals ("MainMenu"));
        StateMachine.AddTransition(play, loading, () => Loading.LevelToLoad.Equals("MainMenu"));
    }

    private void Update()
    {
        StateMachine.Tick(); // tick state machine, check for conditions
    }
}
