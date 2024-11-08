using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventFired();
    public delegate void EventFired<T>(T payload);


    #region Game Events
    public static event EventFired OnStartGameSelected;
    public static void OnGameStart() { FireEvent(OnStartGameSelected); }


    public static event EventFired OnPauseGameSelected;
    public static void OnGamePause() { FireEvent(OnPauseGameSelected); }


    public static event EventFired OnResumeGameSelected;
    public static void OnGameResume() { FireEvent(OnResumeGameSelected); }


    public static event EventFired OnGameFinished;
    public static void OnGameFinish() { FireEvent(OnGameFinished); }


    public static event EventFired OnInputUnlocked;
    public static void OnInputUnlock() { FireEvent(OnInputUnlocked); }


    public static event EventFired<int> OnLifesChanged;
    public static void OnLifesChange(int currentLifes) { FireEvent(OnLifesChanged, currentLifes); }


    public static event EventFired<int> OnScoreChanged;
    public static void OnScoreChange(int currentScore) { FireEvent(OnScoreChanged, currentScore); }
    #endregion Game Events

    #region UI Events
    public static event EventFired OnStartHovered;
    public static void OnStartHover() { FireEvent(OnStartHovered); }


    public static event EventFired OnStartUnhovered;
    public static void OnStartUnhover() { FireEvent(OnStartUnhovered); }


    public static event EventFired OnExitGameSelected;
    public static void OnGameExit() { FireEvent(OnExitGameSelected); }


    public static event EventFired OnMainMenuSelected;
    public static void OnMainMenu() { FireEvent(OnMainMenuSelected); }


    public static event EventFired OnOptionsSelected;
    public static void OnOptionsSelect() { FireEvent(OnOptionsSelected); }


    public static event EventFired<PopupProperties> OnCreatedPopup;
    public static void OnCreatePopup(PopupProperties popupProperties) { FireEvent(OnCreatedPopup, popupProperties); }


    public static event EventFired<ParallaxProperties> OnScrolledForDuration;
    public static void OnScrollForDuration(ParallaxProperties pp) { FireEvent(OnScrolledForDuration, pp); }

    #endregion UI Events

    #region Save Data Events
    public static event EventFired OnDataDeleted;
    public static void OnDataDelete() { FireEvent(OnDataDeleted); }


    public static event EventFired OnDataSaved;
    public static void OnDataSave() { FireEvent(OnDataSaved); }


    public static event EventFired OnDataChanged;
    public static void OnDataChange() { FireEvent(OnDataChanged); }


    public static event EventFired<GameStatistics> OnStatisticsResulted;
    public static void OnStatisticsResult(GameStatistics gameStatistics) { FireEvent(OnStatisticsResulted, gameStatistics); }


    public static event EventFired<GameStatistics> OnStatisticsSaved;
    public static void OnStatisticsSave(GameStatistics gameStatistics) { FireEvent(OnStatisticsSaved, gameStatistics); }


    public static event EventFired<GameStatistics> OnStatisticsLoaded;
    public static void OnStatisticsLoad(GameStatistics gameStatistics) { FireEvent(OnStatisticsLoaded, gameStatistics); }


    public static event EventFired OnStatisticsToLoaded;
    public static void OnStatisticsToLoad() { FireEvent(OnStatisticsToLoaded); }

    #endregion Save Data Events


    #region Character Events

    public static event EventFired<float> OnCharacterMoved;
    public static void OnCharacterMove(float speed) { FireEvent(OnCharacterMoved, speed); }

    
    public static event EventFired OnCharacterJumped;
    public static void OnCharacterJump() { FireEvent(OnCharacterJumped); }
    
    
    public static event EventFired OnCharacterTrampolined;
    public static void OnCharacterTrampoline() { FireEvent(OnCharacterTrampolined); }


    public static event EventFired OnCharacterHitten;
    public static void OnCharacterHit() { FireEvent(OnCharacterHitten); }

    #endregion Characters Events


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