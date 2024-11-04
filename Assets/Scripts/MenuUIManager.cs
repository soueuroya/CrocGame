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
        startGameButton = GameObject.Find("StartButton")?.GetComponent<Button>();
        exitGameButton = GameObject.Find("ExitButton")?.GetComponent<Button>();
        optionsGameButton = GameObject.Find("OptionsButton")?.GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (startGameButton != null)
        {
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


        if (startGameButton != null)
        {
            exitGameButton.onClick.AddListener(EventManager.OnGameExit);
        }

        if (startGameButton != null)
        {
            optionsGameButton.onClick.AddListener(EventManager.OnOptionsSelect);
        }
    }

    private void OnDisable()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveListener(EventManager.OnGameStart);
        }

        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveListener(EventManager.OnGameExit);
        }

        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveListener(EventManager.OnOptionsSelect);
        }
    }
    #endregion Initialization
}

