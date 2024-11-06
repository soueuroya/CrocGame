using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;

    bool isPaused;

    #region Initialization
    private void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (anim == null)
            anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        EventManager.OnStartGameSelected += StartGame;
        
    }
    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= MainMenuSelected;
    }
    private void Start()
    {
        PositionCharacter();
    }
    #endregion Initialization

    #region Private Helpers
    private void PositionCharacter()
    {
        Vector3 bottomLeftScreen = new Vector3(0, 0, Camera.main.nearClipPlane);

        // Use viewport coordinates to get the bottom-left corner of the screen
        Vector3 bottomLeftWorld = Camera.main.ViewportToWorldPoint(bottomLeftScreen);

        // Position the sprite's center to align with the bottom-left corner
        transform.position = bottomLeftWorld + offset;
    }

    private void StartGame()
    {
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += MainMenuSelected;

        anim.SetTrigger("Start");
        Run(speed);
    }
    private void PauseGame()
    {
        isPaused = true;
        Run(0);
    }
    private void ResumeGame()
    {
        isPaused = false;
        Run(speed);
    }

    private void Jump()
    {
        anim.SetTrigger("Jump");
    }

    private void Duck()
    {
        anim.SetTrigger("Duck");
    }

    private void Unduck()
    {
        anim.SetTrigger("Unduck");
    }

    private void Hit()
    {
        StopAllCoroutines();
        anim.SetTrigger("Hit");
    }

    private void Run(float speed)
    {
        StopAllCoroutines();
        anim.SetFloat("SpeedX", speed);
        StartCoroutine(RunRoutine(speed));
    }
    private void MainMenuSelected()
    {
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= MainMenuSelected;

        StopAllCoroutines();
        anim.SetTrigger("Hide");
    }

    private void Update()
    {
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
    }
    private void FixedUpdate()
    {
        if (isPaused) return;

        anim.SetFloat("SpeedY", rb.velocity.y);
    }
    
    IEnumerator RunRoutine(float speed)
    {
        while (true)
        {
            EventManager.OnCharacterMoved(speed);
            yield return null;
        }
    }

    #endregion Private Helpers
}
