using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] BaseUIMenu mainMenu;
    [SerializeField] BaseUIMenu gameMenu;
    [SerializeField] BaseUIMenu optionsMenu;
    public enum Menus {Main, Game, Options}
    [SerializeField] Menus currentMenu;
    private Dictionary<Menus, BaseUIMenu> menuDictionary;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;

    private void Awake()
    {
        // Initialize the dictionary and assign menus
        menuDictionary = new Dictionary<Menus, BaseUIMenu>
        {
            { Menus.Main, mainMenu },
            { Menus.Game, gameMenu },
            { Menus.Options, optionsMenu }
        };
        currentMenu = Menus.Main;

        EventManager.OnStartGameSelected += OnStartGameSelected;
        EventManager.OnExitGameSelected += OnExitGameSelected;
        EventManager.OnOptionsSelected += OnOptionsSelected;
        EventManager.OnMainMenuSelected += OnMainMenuSelected;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= OnStartGameSelected;
        EventManager.OnExitGameSelected -= OnExitGameSelected;
        EventManager.OnOptionsSelected -= OnOptionsSelected;
        EventManager.OnMainMenuSelected -= OnMainMenuSelected;
    }


    public void OnStartGameSelected()
    {
        MusicManager.Instance.StartMusic(gameMusic);

        menuDictionary[currentMenu].Hide();
        gameMenu.gameObject.SetActive(true);
        gameMenu.Show();
        currentMenu = Menus.Game;
    }

    public void OnOptionsSelected()
    {
        menuDictionary[currentMenu].Hide();
        optionsMenu.gameObject.SetActive(true);
        optionsMenu.Show();
        currentMenu = Menus.Options;

        Invoke("ScrollBackgroundToGame", 0.29f);
    }

    private void ScrollBackgroundToGame()
    {
        EventManager.OnScrolledForDuration(new ParallaxProperties() { duration = 2.2f, speed = 0.02f });
    }

    public void OnMainMenuSelected()
    {
        MusicManager.Instance.StartMusic(menuMusic);

        menuDictionary[currentMenu].Hide();
        mainMenu.gameObject.SetActive(true);
        mainMenu.Show();
        currentMenu = Menus.Main;

        Invoke("ScrollBackgroundToMenu", 0.29f);
    }

    private void ScrollBackgroundToMenu()
    {
        EventManager.OnScrolledForDuration(new ParallaxProperties() { duration = 2.2f, speed = -0.02f });
    }

    public void OnExitGameSelected()
    {
        Invoke("ExitGame", 1);
    }

    private void ExitGame()
    {
        AppHelper.Quit();
    }
}
