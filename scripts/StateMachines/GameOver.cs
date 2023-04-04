public class GameOver : Istate
{
    public void OnEnter()
    {
        soundManager.Instance.playGamesound(gameSounds.gameover); // play gameover sound
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
