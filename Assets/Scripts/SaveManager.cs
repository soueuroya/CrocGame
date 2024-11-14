using System.IO;
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
        string path = Application.persistentDataPath + "/" + Constants.Prefs.saveFileName;

        gameStatistics.currentLifes = 3;
        string json = JsonUtility.ToJson(gameStatistics);
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(json);
            writer.Close();
        }
    }

    private void OnStatisticsToLoaded()
    {
        EventManager.OnStatisticsLoad(LoadGameStatistics());
    }

    private GameStatistics LoadGameStatistics()
    {
        GameStatistics gameStatistics = new GameStatistics();
        string path = Application.persistentDataPath + "/" + Constants.Prefs.saveFileName;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            gameStatistics = JsonUtility.FromJson<GameStatistics>(json);
        }
        return gameStatistics;
    }
}