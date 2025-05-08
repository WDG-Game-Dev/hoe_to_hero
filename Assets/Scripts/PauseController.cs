using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Static property to check if the game is paused
    public static bool IsGamePaused { get; private set; }

    private void Start()
    {
        IsGamePaused = false;
    }

    // Method to set the game pause state
    public static void SetPause(bool isPaused)
    {
        IsGamePaused = isPaused;

        if (IsGamePaused)
        {
            Time.timeScale = 0f; // Stop time in the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }
}
