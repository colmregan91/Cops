using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : Istate
{

    private AsyncOperation operation = new AsyncOperation();
    public static string LevelToLoad;
    public bool isFinished => operation.isDone;

   
    public void OnEnter()
    {
        playButton.clicked = false;
        RestartButton.clicked = false;
        ReturnButton.clicked = false;

        operation = SceneManager.LoadSceneAsync(LevelToLoad);

    }

    public void OnExit()
    {

        LevelToLoad = "";
        operation = null;
        loadOption.loadProg = null;
    }

    public void OnUpdate()
    {

    }
}