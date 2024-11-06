using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.OnDataDeleted += OnDataDeleted;
    }

    private void OnDestroy()
    {
        EventManager.OnDataDeleted -= OnDataDeleted;
    }


    private void OnDataDeleted()
    {
        AudioManager.Instance.PlayType();
        PopupProperties popupProperties = new PopupProperties("Do you wish to delete all data?", "Delete Data", "Yes", DeleteData, "No", null);
        EventManager.OnCreatedPopup(popupProperties);
    }

    private void DeleteData()
    {
        SafePrefs.DeleteAll();
        EventManager.OnDataUpdate();
    }
}