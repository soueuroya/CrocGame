﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventFired();
    public delegate void EventFired<T>(T payload);


    #region Public Methods
    public static event EventFired OnStartGameSelected;
    public static void OnGameStart() { FireEvent(OnStartGameSelected); }


    public static event EventFired OnPauseGameSelected;
    public static void OnGamePause() { FireEvent(OnPauseGameSelected); }


    public static event EventFired OnResumeGameSelected;
    public static void OnGameResume() { FireEvent(OnResumeGameSelected); }


    public static event EventFired OnStartHovered;
    public static void OnStartHover() { FireEvent(OnStartHovered); }


    public static event EventFired OnStartUnhovered;
    public static void OnStartUnhover() { FireEvent(OnStartUnhovered); }


    public static event EventFired OnExitGameSelected;
    public static void OnGameExit() { FireEvent(OnExitGameSelected); }


    public static event EventFired OnMainMenuSelected;
    public static void OnMainMenu() { FireEvent(OnMainMenuSelected); }


    public static event EventFired OnInputUnlocked;
    public static void OnInputUnlock() { FireEvent(OnInputUnlocked); }


    public static event EventFired OnOptionsSelected;
    public static void OnOptionsSelect() { FireEvent(OnOptionsSelected); }


    public static event EventFired OnDataDeleted;
    public static void OnDataDelete() { FireEvent(OnDataDeleted); }


    public static event EventFired OnDataSaved;
    public static void OnDataSave() { FireEvent(OnDataSaved); }


    public static event EventFired OnDataUpdated;
    public static void OnDataUpdate() { FireEvent(OnDataUpdated); }
    
    
    public static event EventFired<PopupProperties> OnCreatePopup;
    public static void OnCreatedPopup(PopupProperties popupProperties) { FireEvent(OnCreatePopup, popupProperties); }



    public static event EventFired<ParallaxProperties> OnScrollForDuration;
    public static void OnScrolledForDuration(ParallaxProperties pp) { FireEvent(OnScrollForDuration, pp); }



    public static event EventFired<float> OnCharacterMove;
    public static void OnCharacterMoved(float speed) { FireEvent(OnCharacterMove, speed); }


    public static event EventFired OnGameEnded;
    public static void OnGameEnd() { FireEvent(OnGameEnded); }
    #endregion Public Methods


    #region Private Helpers
    protected static void FireEvent(EventFired triggerEvent)
    {
        if (triggerEvent != null)
        {
            triggerEvent();
        }
    }

    protected static void FireEvent<T>(EventFired<T> triggerEvent, T payload)
    {
        if (triggerEvent != null)
        {
            triggerEvent(payload);
        }
    }
    #endregion Private Helpers
}