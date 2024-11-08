using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] BaseUIMenu mainMenu;
    [SerializeField] BaseUIMenu gameMenu;
    [SerializeField] BaseUIMenu optionsMenu;
    [SerializeField] BaseUIMenu resultsMenu;
    public enum Menus { Main, Game, Options, Results }
    [SerializeField] Menus currentMenu;
    private Dictionary<Menus, BaseUIMenu> menuDictionary;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;

    private bool inputLock;

    #region Initialization

    private void Awake()
    {
        // Initialize the dictionary and assign menus
        menuDictionary = new Dictionary<Menus, BaseUIMenu>
        {
            { Menus.Main, mainMenu },
            { Menus.Game, gameMenu },
            { Menus.Results, resultsMenu },
            { Menus.Options, optionsMenu }
        };
        currentMenu = Menus.Main;

        EventManager.OnStartGameSelected += OnStartGameSelected;
        EventManager.OnExitGameSelected += OnExitGameSelected;
        EventManager.OnOptionsSelected += OnOptionsSelected;
        EventManager.OnMainMenuSelected += OnMainMenuSelected;
        EventManager.OnGameFinished += OnGameFinished;
        EventManager.OnInputUnlocked += OnInputUnlocked;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= OnStartGameSelected;
        EventManager.OnExitGameSelected -= OnExitGameSelected;
        EventManager.OnOptionsSelected -= OnOptionsSelected;
        EventManager.OnMainMenuSelected -= OnMainMenuSelected;
        EventManager.OnGameFinished -= OnGameFinished;
        EventManager.OnInputUnlocked -= OnInputUnlocked;
    }

    #endregion Initialization

    #region Private Helpers
    private void OnInputUnlocked()
    {
        inputLock = false;
    }

    private void OnStartGameSelected()
    {
        if (inputLock) return;
        inputLock = true;

        AudioManager.Instance.PlayType();
        MusicManager.Instance.StartMusic(gameMusic);

        menuDictionary[currentMenu].Hide();
        gameMenu.gameObject.SetActive(true);
        gameMenu.Show();
        currentMenu = Menus.Game;
    }

    private void OnOptionsSelected()
    {
        if (inputLock) return;
        inputLock = true;

        AudioManager.Instance.PlayType();

        menuDictionary[currentMenu].Hide();
        optionsMenu.gameObject.SetActive(true);
        optionsMenu.Show();
        currentMenu = Menus.Options;

        Invoke("ScrollBackgroundToRight", 0.5f);
    }

    private void OnMainMenuSelected()
    {
        if (inputLock) return;
        inputLock = true;

        AudioManager.Instance.PlayType();
        MusicManager.Instance.StartMusic(menuMusic);

        menuDictionary[currentMenu].Hide();
        mainMenu.gameObject.SetActive(true);
        mainMenu.Show();
        currentMenu = Menus.Main;

        Invoke("ScrollBackgroundToLeft", 0.5f);
    }

    private void OnGameFinished()
    {
        inputLock = true;
        MusicManager.Instance.StartMusic(menuMusic);
        menuDictionary[currentMenu].Hide();

        resultsMenu.gameObject.SetActive(true);
        resultsMenu.Show();
        currentMenu = Menus.Results;

        Invoke("ScrollBackgroundToLeft", 0.5f);
    }

    private void OnExitGameSelected()
    {
        if (inputLock) return;
        inputLock = true;

        AudioManager.Instance.PlayType();

        Invoke("ExitGame", 1);
    }

    private void ExitGame()
    {
        AppHelper.Quit();
    }

    private void ScrollBackgroundToRight()
    {
        EventManager.OnScrollForDuration(new ParallaxProperties() { duration = 2f, speed = 2f });
    }

    private void ScrollBackgroundToLeft()
    {
        EventManager.OnScrollForDuration(new ParallaxProperties() { duration = 2f, speed = -2f });
    }

    #endregion Private Helpers
}
