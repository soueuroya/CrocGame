using UnityEngine;
using UnityEngine.UI;

public class ResultsUIManager : BaseUIMenu
{
    [SerializeField] Button mainMenuGameButton;
    
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
    }

    private void OnDestroy()
    {
        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
        }

        EventManager.OnMainMenuSelected -= MainMenuSelected;
    }
    
    private void MainMenuSelected()
    {

    }
    #endregion Initialization
}

