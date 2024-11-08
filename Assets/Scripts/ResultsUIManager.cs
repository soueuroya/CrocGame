using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUIManager : BaseUIMenu
{
    [SerializeField] Button mainMenuGameButton;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI jumps;
    [SerializeField] TextMeshProUGUI mushrooms;

    [SerializeField] TextMeshProUGUI totalDistance;
    [SerializeField] TextMeshProUGUI totalJumps;
    [SerializeField] TextMeshProUGUI totalMushrooms;

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
        EventManager.OnStatisticsResulted += OnStatisticsResulted;
    }

    private void OnDestroy()
    {
        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
        }

        EventManager.OnMainMenuSelected -= MainMenuSelected;
        EventManager.OnStatisticsResulted -= OnStatisticsResulted;
    }
    
    private void MainMenuSelected()
    {

    }

    private void OnStatisticsResulted(GameStatistics gameStatistics)
    {
        distance.text = ((int)gameStatistics.currentScore) + "M";
        jumps.text = gameStatistics.currentJumps.ToString();
        mushrooms.text = gameStatistics.currentMushrooms.ToString();

        totalDistance.text = ((int)(gameStatistics.currentScore + gameStatistics.totalScore)) + "M";
        totalJumps.text = (gameStatistics.currentJumps + gameStatistics.totalJumps).ToString();
        totalMushrooms.text = (gameStatistics.currentMushrooms + gameStatistics.totalMushrooms).ToString();
    }
    #endregion Initialization
}

