using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIManager : BaseUIMenu
{
    [SerializeField] Button resumeGameButton;
    [SerializeField] Button mainMenuGameButton;
    

    #region Initialization
    private void OnValidate()
    {
        resumeGameButton = GameObject.Find("ResumeButton")?.GetComponent<Button>();
        mainMenuGameButton = GameObject.Find("MainMenuButton")?.GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.AddListener(EventManager.OnGameResume);

            // TODO // Unpause game
        }


        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.AddListener(EventManager.OnMainMenu);
        }
    }

    private void OnDisable()
    {
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.RemoveListener(EventManager.OnGameResume);
        }

        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveListener(EventManager.OnMainMenu);
        }

    }
    #endregion Initialization
}

