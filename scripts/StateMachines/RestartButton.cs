
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : Button
{
    public static RestartButton _instance;
    public static bool clicked;
    public static bool pressed => _instance != null && clicked;

    protected override void OnEnable()
    {
        base.OnEnable();
        _instance = this;
        _instance.onClick.AddListener(() => SetLevelToLoad());
    }


    protected override void OnDisable()
    {
        clicked = false;
        base.OnDisable();
        _instance.onClick.RemoveAllListeners();
    }

    void SetLevelToLoad()
    {

        clicked = true;
        Loading.LevelToLoad = SceneManager.GetActiveScene().name;
    }

}
