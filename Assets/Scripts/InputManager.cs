using UnityEngine;

public class InputManager : MonoBehaviour
{
    bool isPaused;
    bool isStarted;
    Vector2 touchStart;
    float verticalInput;
    float minSwipeDistance = 50f;
    float minSwipeVelocity = 1000f;

    [SerializeField] private KeyCode[] jumpKeys = { KeyCode.Space, KeyCode.UpArrow, KeyCode.W };

    
    #region Initialization
    private void Awake()
    {
        isPaused = true;
        EventManager.OnStartGameSelected += StartGame;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += ExitGame;
        EventManager.OnGameFinished += FinishGame;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= ExitGame;
        EventManager.OnGameFinished -= FinishGame;
    }

    private void Update()
    {
        if (!isStarted) return;

        // Pause/Resume detection
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                EventManager.OnGameResume();
            }
            else
            {
                EventManager.OnGamePause();
            }
        }

        if (isPaused) return;

        // Get all axis inputs
        ProcessAxisInputs();

        // Jump key detection
        DetectJumpInput();

        // Swipe detection
        DetectSwipeUp();
    }
    #endregion Initialization

    #region Input Processing
    private void ProcessAxisInputs()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        bool fire1 = Input.GetButton("Fire1");
        bool submit = Input.GetButtonDown("Submit");

        if (fire1 || submit || verticalInput > 0) EventManager.OnCharacterJump();
    }

    private void DetectJumpInput()
    {
        // Check custom jump keys
        foreach (KeyCode key in jumpKeys)
        {
            if (Input.GetKeyDown(key))
            {
                OnJumpInput();
                break;
            }
        }

        // Check default Unity jump button
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpInput();
        }
    }

    private void DetectSwipeUp()
    {
        // Handle mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 swipeDelta = touch.position - touchStart;
                    float velocity = touch.deltaPosition.magnitude / touch.deltaTime;

                    if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.y > minSwipeDistance && velocity > minSwipeVelocity)
                        {
                            OnJumpInput();
                        }
                    }
                    break;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeDelta = (Vector2)Input.mousePosition - touchStart;
            float duration = Time.time - Time.deltaTime;
            float velocity = swipeDelta.magnitude / duration;

            if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.y > minSwipeDistance && velocity > minSwipeVelocity)
                {
                    OnJumpInput();
                }
            }
        }
#endif
    }

    private void OnJumpInput()
    {
        EventManager.OnCharacterJump();
    }
    #endregion Input Processing


    #region Private Helpers
    private void StartGame()
    {
        Invoke("StartInput", 1.5f);
    }

    private void StartInput()
    {
        isStarted = true;
        isPaused = false;
    }

    private void PauseGame()
    {
        isPaused = true;
    }

    private void ResumeGame()
    {
        isPaused = false;
    }
    private void ExitGame()
    {
        isStarted = false;
    }
    private void FinishGame()
    {
        isStarted = false;
    }
    #endregion Private Helpers
}