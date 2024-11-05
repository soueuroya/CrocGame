using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUIManager : BaseUIMenu
{
    [SerializeField] Button startGameButton;
    [SerializeField] Button exitGameButton;
    [SerializeField] Button optionsGameButton;

    #region Initialization
    private void OnValidate()
    {
        if (startGameButton == null)
        startGameButton = GameObject.Find("StartButton")?.GetComponent<Button>();

        if (exitGameButton == null)
        exitGameButton = GameObject.Find("ExitButton")?.GetComponent<Button>();

        if (optionsGameButton == null)
        optionsGameButton = GameObject.Find("OptionsButton")?.GetComponent<Button>();
    }
    private void Awake()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(EventManager.OnGameStart);

            // Add hover events
            EventTrigger eventTrigger = startGameButton.gameObject.AddComponent<EventTrigger>();

            // OnPointerEnter (Hover Start)
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => { EventManager.OnStartHover(); });
            eventTrigger.triggers.Add(pointerEnter);

            // OnPointerExit (Hover End)
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => { EventManager.OnStartUnhover(); });
            eventTrigger.triggers.Add(pointerExit);
        }


        if (exitGameButton != null)
        {
            exitGameButton.onClick.RemoveAllListeners();
            exitGameButton.onClick.AddListener(EventManager.OnGameExit);
        }

        if (optionsGameButton != null)
        {
            optionsGameButton.onClick.RemoveAllListeners();
            optionsGameButton.onClick.AddListener(EventManager.OnOptionsSelect);
        }
    }

    private void OnDestroy()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveAllListeners();
        }

        if (exitGameButton != null)
        {
            exitGameButton.onClick.RemoveAllListeners();
        }

        if (optionsGameButton != null)
        {
            optionsGameButton.onClick.RemoveAllListeners();
        }
    }
    #endregion Initialization
}

