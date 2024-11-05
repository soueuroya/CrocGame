using UnityEngine;
using UnityEngine.UI;

public class OptionsUIManager : BaseUIMenu
{
    [SerializeField] Button deleteDataButton;
    [SerializeField] Button mainMenuGameButton;


    #region Initialization
    private void OnValidate()
    {
        if (deleteDataButton == null)
        deleteDataButton = GameObject.Find("DeleteButton")?.GetComponent<Button>();

        if (mainMenuGameButton == null)
        mainMenuGameButton = GameObject.Find("MainMenuButton")?.GetComponent<Button>();
    }
    private void Awake()
    {
        anim.Play("OptionsHidden", 0, 0f);

        if (deleteDataButton != null)
        {
            deleteDataButton.onClick.RemoveAllListeners();
            deleteDataButton.onClick.AddListener(EventManager.OnDataDelete);
        }

        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
            mainMenuGameButton.onClick.AddListener(EventManager.OnMainMenu);
        }
    }

    private void OnDestroy()
    {
        if (deleteDataButton != null)
        {
            deleteDataButton.onClick.RemoveAllListeners();
        }

        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
        }
    }
    #endregion Initialization
}

