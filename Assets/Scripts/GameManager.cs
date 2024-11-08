using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Initialization

    private void Awake()
    {
        EventManager.OnStartGameSelected += StartGame;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
    }

    #endregion Initialization

    #region Private Helpers
    private void StartGame()
    {
        
    }

    private void PauseGame()
    {

    }

    private void ResumeGame()
    {

    }
    #endregion Private Helpers
}
