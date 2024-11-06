using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : BaseUIMenu
{
    [SerializeField] Button resumeGameButton;
    [SerializeField] Button mainMenuGameButton;
    [SerializeField] GameObject pause;

    #region Initialization
    private void OnValidate()
    {
        if (resumeGameButton == null)
        resumeGameButton = GameObject.Find("ResumeButton")?.GetComponent<Button>();

        if (mainMenuGameButton == null)
        mainMenuGameButton = GameObject.Find("MainMenuButton_Game")?.GetComponent<Button>();
    }
    private void Awake()
    {
        anim.Play("GameHidden", 0, 0f);
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.RemoveAllListeners();
            resumeGameButton.onClick.AddListener(EventManager.OnGameResume);

            // TODO // Unpause game
        }


        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
            mainMenuGameButton.onClick.AddListener(EventManager.OnMainMenu);
        }

        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
    }

    private void OnDestroy()
    {
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.RemoveAllListeners();
        }

        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
        }

        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
    }

    private void PauseGame()
    {
        pause.SetActive(true);
    }

    private void ResumeGame()
    {
        pause.SetActive(false);
    }
    #endregion Initialization
}

