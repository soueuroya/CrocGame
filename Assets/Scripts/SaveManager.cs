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
        SafePrefs.SetFloat("SCORE", gameStatistics.totalScore + gameStatistics.currentScore);
        SafePrefs.SetInt("JUMPS", gameStatistics.totalJumps + gameStatistics.currentJumps);
        SafePrefs.SetInt("MUSHROOMS", gameStatistics.totalMushrooms + gameStatistics.currentMushrooms);
        SafePrefs.SetInt("LIFES_USED", gameStatistics.totalLifesUsed + (3-gameStatistics.currentLifes));
        //SafePrefs.SetInt("HITS", gameStatistics.totalHits + gameStatistics.currentHits);
        //SafePrefs.SetInt("OBSTACLES", gameStatistics.totalObstacles);
        SafePrefs.Save();
    }

    private void OnStatisticsToLoaded()
    {
        EventManager.OnStatisticsLoad(LoadGameStatistics());
    }

    private GameStatistics LoadGameStatistics()
    {
        GameStatistics gameStatistics = new GameStatistics();
        gameStatistics.totalScore = SafePrefs.GetFloat("SCORE");
        gameStatistics.totalJumps = SafePrefs.GetInt("JUMPS");
        gameStatistics.totalMushrooms = SafePrefs.GetInt("MUSHROOMS");
        gameStatistics.totalLifesUsed = SafePrefs.GetInt("LIFES_USED");
        //gameStatistics.totalHits = SafePrefs.GetInt("HITS");
        //gameStatistics.totalObstacles = SafePrefs.GetInt("OBSTACLES");
        return gameStatistics;
    }

}