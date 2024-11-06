using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        EventManager.OnStartGameSelected += StartGame;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
    }


    private void StartGame()
    {
        //EventManager.OnScrolledForDuration(new ParallaxProperties() { duration = 2f, speed = 0.02f });
    }

}
