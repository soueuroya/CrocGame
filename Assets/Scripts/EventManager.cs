using System.Collections;
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


    public static event EventFired OnDataChanged;
    public static void OnDataChange() { FireEvent(OnDataChanged); }

    
    public static event EventFired<PopupProperties> OnCreatedPopup;
    public static void OnCreatePopup(PopupProperties popupProperties) { FireEvent(OnCreatedPopup, popupProperties); }


    public static event EventFired<ParallaxProperties> OnScrolledForDuration;
    public static void OnScrollForDuration(ParallaxProperties pp) { FireEvent(OnScrolledForDuration, pp); }


    public static event EventFired<float> OnCharacterMoved;
    public static void OnCharacterMove(float speed) { FireEvent(OnCharacterMoved, speed); }

    public static event EventFired OnCharacterJumped;
    public static void OnCharacterJump() { FireEvent(OnCharacterJumped); }


    public static event EventFired OnCharacterHitten;
    public static void OnCharacterHit() { FireEvent(OnCharacterHitten); }


    public static event EventFired<GameStatistics> OnStatisticsSaved;
    public static void OnStatisticsSave(GameStatistics gameStatistics) { FireEvent(OnStatisticsSaved, gameStatistics); }


    public static event EventFired<GameStatistics> OnStatisticsLoaded;
    public static void OnStatisticsLoad(GameStatistics gameStatistics) { FireEvent(OnStatisticsLoaded, gameStatistics); }


    public static event EventFired OnStatisticsToLoaded;
    public static void OnStatisticsToLoad() { FireEvent(OnStatisticsToLoaded); }


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