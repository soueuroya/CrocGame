using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultsUIManager : BaseUIMenu
{
    [SerializeField] Button mainMenuGameButton;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI jumps;
    [SerializeField] TextMeshProUGUI mushrooms;

    [SerializeField] TextMeshProUGUI totalDistance;
    [SerializeField] TextMeshProUGUI totalJumps;
    [SerializeField] TextMeshProUGUI totalMushrooms;

    int currentDistance;

    #region Initialization
    private void OnValidate()
    {
        if (mainMenuGameButton == null)
            mainMenuGameButton = GameObject.Find("MainMenuButton_Game")?.GetComponent<Button>();
    }
    private void Awake()
    {
        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
            mainMenuGameButton.onClick.AddListener(EventManager.OnMainMenu);
        }

        EventManager.OnMainMenuSelected += MainMenuSelected;
        EventManager.OnRestartGameSelected += GameRestarted;
        EventManager.OnStatisticsResulted += OnStatisticsResulted;
    }
    private void OnDestroy()
    {
        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
        }

        EventManager.OnMainMenuSelected -= MainMenuSelected;
        EventManager.OnRestartGameSelected -= GameRestarted;
        EventManager.OnStatisticsResulted -= OnStatisticsResulted;
    }
    
    private void MainMenuSelected()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent("returnedToMenu", new Dictionary<string, object> { { "Score", currentDistance } });
        Debug.Log("Returned To Menu - Analytics Result: " + analyticsResult);
    }

    private void OnStatisticsResulted(GameStatistics gameStatistics)
    {
        currentDistance = (int)gameStatistics.currentScore;
        distance.text = currentDistance + "M";
        jumps.text = gameStatistics.currentJumps.ToString();
        mushrooms.text = gameStatistics.currentMushrooms.ToString();

        totalDistance.text = ((int)(gameStatistics.currentScore + gameStatistics.totalScore)) + "M";
        totalJumps.text = (gameStatistics.currentJumps + gameStatistics.totalJumps).ToString();
        totalMushrooms.text = (gameStatistics.currentMushrooms + gameStatistics.totalMushrooms).ToString();
    }
    private void GameRestarted()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent("gameRestarted", new Dictionary<string, object> { { "Score", currentDistance } });
        Debug.Log("Game Restarted - Analytics Result: " + analyticsResult);
    }
    #endregion Initialization
}