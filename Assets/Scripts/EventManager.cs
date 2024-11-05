using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Prevent non-singleton constructor use.
    protected EventManager() { }
    public static EventManager Instance;

    public delegate void EventFired();
    public delegate void EventFired<T>(T payload);


    #region Initialization
    //Singleton awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion Initialization


    #region Public Methods
    public static event EventFired OnStartGameSelected;
    public static void OnGameStart() { FireEvent(OnStartGameSelected); }


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


    public static event EventFired OnOptionsSelected;
    public static void OnOptionsSelect() { FireEvent(OnOptionsSelected); }


    public static event EventFired OnDataDeleted;
    public static void OnDataDelete() { FireEvent(OnDataDeleted); }


    public static event EventFired OnDataSaved;
    public static void OnDataSave() { FireEvent(OnDataSaved); }


    public static event EventFired OnDataUpdated;
    public static void OnDataUpdate() { FireEvent(OnDataUpdated); }


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