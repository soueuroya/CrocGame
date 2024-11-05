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
        if (resumeGameButton == null)
        resumeGameButton = GameObject.Find("ResumeButton")?.GetComponent<Button>();

        if (mainMenuGameButton == null)
        mainMenuGameButton = GameObject.Find("MainMenuButton_Game")?.GetComponent<Button>();
    }
    private void Awake()
    {
        anim.Play("GameHidden", 0, 0f);
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.RemoveAllListeners();
            resumeGameButton.onClick.AddListener(EventManager.OnGameResume);

            // TODO // Unpause game
        }


        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
            mainMenuGameButton.onClick.AddListener(EventManager.OnMainMenu);
        }
    }

    private void OnDestroy()
    {
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.RemoveAllListeners();
        }

        if (mainMenuGameButton != null)
        {
            mainMenuGameButton.onClick.RemoveAllListeners();
        }

    }
    #endregion Initialization
}

