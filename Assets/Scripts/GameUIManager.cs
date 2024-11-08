using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : BaseUIMenu
{
    [SerializeField] Button resumeGameButton;
    [SerializeField] Button pauseGameButton;
    [SerializeField] Button mainMenuGameButton;
    [SerializeField] GameObject pause;
    [SerializeField] TextMeshProUGUI scoreLabel;

    #region Initialization
    private void OnValidate()
    {
        if (resumeGameButton == null)
            resumeGameButton = GameObject.Find("ResumeButton")?.GetComponent<Button>();

        if (pauseGameButton == null)
            pauseGameButton = GameObject.Find("PauseButton")?.GetComponent<Button>();

        if (mainMenuGameButton == null)
            mainMenuGameButton = GameObject.Find("MainMenuButton_Game")?.GetComponent<Button>();
    }
    private void Awake()
    {
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.RemoveAllListeners();
            resumeGameButton.onClick.AddListener(EventManager.OnGameResume);
        }

        if (pauseGameButton != null)
        {
            pauseGameButton.onClick.RemoveAllListeners();
            pauseGameButton.onClick.AddListener(EventManager.OnGamePause);
        }


        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
            mainMenuGameButton.onClick.AddListener(EventManager.OnMainMenu);
        }

        EventManager.OnScoreChanged += ScoreChanged;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += MainMenuSelected;
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

        EventManager.OnScoreChanged -= ScoreChanged;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= MainMenuSelected;
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            EventManager.OnGamePause();
        }
        else
        {
            EventManager.OnGameResume();
        }
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            EventManager.OnGamePause();
        }
        else
        {
            //EventManager.OnGameResume(); // don't auto resume game for now
        }
    }
    private void PauseGame()
    {
        pause.SetActive(true);
    }

    private void ResumeGame()
    {
        pause.SetActive(false);
    }
    private void MainMenuSelected()
    {
        pause.SetActive(false);
    }

    private void ScoreChanged(int currentScore)
    {
        scoreLabel.text = currentScore + "M";
    }
    #endregion Initialization
}

