using System;


public static class UIEvents
{
    public static event Action HighScoresButtonPressed;
    public static event Action PlayGameButtonPressed;
    public static event Action BackToMainMenuButtonPressed;
    public static void FireHighScoresButtonPressed()
    {
        HighScoresButtonPressed?.Invoke();
    }

    public static void FirePlayGameButtonPressed()
    {
        PlayGameButtonPressed?.Invoke();
    }

    public static void FireBackToMainMenuButtonPressed()
    {
        BackToMainMenuButtonPressed?.Invoke();
    }


}
