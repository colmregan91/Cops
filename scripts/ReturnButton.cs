using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : Button
{

    public static ReturnButton _instance;
    public static bool clicked;
    public static bool pressed => _instance != null && clicked;

    private const string MAIN_MENU = "MainMenu";

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

    public static void SetLevelToLoad()
    {
        Loading.LevelToLoad = MAIN_MENU;
        clicked = true;
    }
}
