using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.OnDataDeleted += OnDataDeleted;
        EventManager.OnStatisticsSaved += OnStatisticsSaved;
        EventManager.OnStatisticsToLoaded += OnStatisticsToLoaded;
    }

    private void OnDestroy()
    {
        EventManager.OnDataDeleted -= OnDataDeleted;
        EventManager.OnStatisticsSaved -= OnStatisticsSaved;
        EventManager.OnStatisticsToLoaded -= OnStatisticsToLoaded;
    }


    private void OnDataDeleted()
    {
        AudioManager.Instance.PlayType();
        PopupProperties popupProperties = new PopupProperties("Do you wish to delete all data?", "Delete Data", "Yes", DeleteData, "No", null);
        EventManager.OnCreatePopup(popupProperties);
    }

    private void DeleteData()
    {
        SafePrefs.DeleteAll();
        EventManager.OnDataChange();
    }

    private void OnStatisticsSaved(GameStatistics gameStatistics)
    {
        SafePrefs.SetFloat("SCORE", gameStatistics.score);
        SafePrefs.SetInt("MUSHROOMS", gameStatistics.mushrooms);
        SafePrefs.SetInt("HITS", gameStatistics.hits);
        SafePrefs.SetInt("OBSTACLES", gameStatistics.obstacles);
        SafePrefs.SetInt("JUMPS", gameStatistics.jumps);
        SafePrefs.Save();
    }

    private void OnStatisticsToLoaded()
    {
        EventManager.OnStatisticsLoad(LoadGameStatistics());
    }

    private GameStatistics LoadGameStatistics()
    {
        GameStatistics gameStatistics = new GameStatistics();
        gameStatistics.score = SafePrefs.GetFloat("SCORE");
        gameStatistics.jumps = SafePrefs.GetInt("JUMPS");
        gameStatistics.mushrooms = SafePrefs.GetInt("MUSHROOMS");
        gameStatistics.hits = SafePrefs.GetInt("HITS");
        gameStatistics.obstacles = SafePrefs.GetInt("OBSTACLES");
        return gameStatistics;
    }

}