using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] BaseUIMenu mainMenu;
    [SerializeField] BaseUIMenu gameMenu;
    [SerializeField] BaseUIMenu optionsMenu;

    [SerializeField] BaseUIMenu currentMenu;

    private void OnValidate()
    {
        mainMenu = GameObject.Find("MainMenuCanvas")?.GetComponent<BaseUIMenu>();
        gameMenu = GameObject.Find("GameCanvas")?.GetComponent<BaseUIMenu>();
        optionsMenu = GameObject.Find("OptionsCanvas")?.GetComponent<BaseUIMenu>();
    }

    private void Awake()
    {
        currentMenu = mainMenu;
    }

    private void OnEnable()
    {
        EventManager.OnStartGameSelected += OnStartGameSelected;
        EventManager.OnExitGameSelected += OnExitGameSelected;
        EventManager.OnOptionsSelected += OnOptionsSelected;
    }

    private void OnDisable()
    {
        EventManager.OnStartGameSelected -= OnStartGameSelected;
        EventManager.OnExitGameSelected -= OnExitGameSelected;
        EventManager.OnOptionsSelected -= OnOptionsSelected;
    }


    public void OnStartGameSelected()
    {
        //SceneManager.LoadScene("Game");
        currentMenu.Hide();
        gameMenu.Show();

        currentMenu = gameMenu;
    }

    public void OnOptionsSelected()
    {
        currentMenu.Hide();
        optionsMenu.Show();

        currentMenu = optionsMenu;
    }

    public void OnMainMenuSelected()
    {
        currentMenu.Hide();
        mainMenu.Show();

        currentMenu = mainMenu;
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
