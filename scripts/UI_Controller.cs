using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject OptionsMenu;
    public GameObject GameOverMenu;
    public GameObject saveConfirmation;
    private void OnEnable()
    {
        GameStateMachine.OnGameStateChanged += HandleStateChange;
        PlayerStateMachine.OnStateChanged += HandlePlayerStateChange;
        GameOverMenu.SetActive(false); // turn off menu at start
        pauseMenu.SetActive(false);// turn off menu at start

    }

    private void OnDisable()
    {
        GameStateMachine.OnGameStateChanged -= HandleStateChange;
        PlayerStateMachine.OnStateChanged -= HandlePlayerStateChange;
    }

    private void HandleStateChange(Istate state)
    {

        pauseMenu.SetActive(state is Pause);

        if (state is Play)
        {
            OptionsMenu.SetActive(false);
        }

    }

    private void HandlePlayerStateChange(Istate state)
    {

        GameOverMenu.SetActive(state is GameOver);
    }

    public void SaveProgress()
    {
        PlayerProgress prog = new PlayerProgress // gets all data to be saved
        {
            name = GameStateMachine.GetPlayerName(),
            coins = GameManager.Instance.getCoins(),
            lives = GameManager.Instance.getLives(),
            pos = checkpointmanager.Instance.getLastPassedCheckPoint().transform.position,
            backgroundIndex = FindObjectOfType<backgroundCheck>().getBackgroundIndex(),
            SFXvolume = soundManager.Instance.getSFXvolume(),
            BackgroundVolume = soundManager.Instance.getBackgroundVolume(),
            jumpVolume = soundManager.Instance.getJumpVolume(),
        };
        var savedProg = JsonUtility.ToJson(prog); // parse to json

        File.WriteAllText(Application.persistentDataPath + $"/saves/{GameStateMachine.GetPlayerName()}_save.txt", savedProg); // write to persistent data path
        saveConfirmation.SetActive(true); // turn on save confirmation text object
        Invoke("saveOff", 2f); // turn it off after 2 seconds
    }

    void saveOff()
    {
        saveConfirmation.SetActive(false);
    }


    public void optionsMenu()
    {
        pauseMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void ApplyOptions()
    {
        pauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
}
