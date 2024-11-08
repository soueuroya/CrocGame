using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Services.Analytics;
//using UnityEngine.Analytics;
using UnityEngine.UI;
using Unity.Services.Core;

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
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AnalyticsService.Instance.SetAnalyticsEnabled(true);
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
        //AnalyticsResult analyticsResult = Analytics.CustomEvent("Returned to Menu", new Dictionary<string, object> { { "Score", currentDistance } });
        AnalyticsService.Instance.RecordInternalEvent(new Unity.Services.Analytics.Internal.Event("Returned to Menu", 0));
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
        //AnalyticsResult analyticsResult = Analytics.CustomEvent("Game restarted", new Dictionary<string, object> { { "Score", currentDistance } });
        AnalyticsService.Instance.RecordInternalEvent(new Unity.Services.Analytics.Internal.Event("Game restarted", 0));
    }
    #endregion Initialization
}