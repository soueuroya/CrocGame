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
        PopupManager.Instance.CreateMessagePopup(body: "Do you wish to delete all data?", title: "Delete Data", primaryButtonText: "Yes", DeleteData, "No", null);
    }

    private void DeleteData()
    {
        SafePrefs.DeleteAll();
        EventManager.OnDataUpdate();
    }

}
