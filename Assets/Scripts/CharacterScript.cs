using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    bool isPaused;
    bool isGrounded;

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

        EventManager.OnRestartGameSelected -= RestartGame;
        EventManager.OnCharacterJumped -= Jump;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= MainMenuSelected;
        EventManager.OnGameFinished -= FinishGame;
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

        // Position the sprites center to align with the bottom-left corner
        transform.position = bottomLeftWorld + offset;
    }

    private void StartGame()
    {
        EventManager.OnRestartGameSelected += RestartGame;
        EventManager.OnCharacterJumped += Jump;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += MainMenuSelected;
        EventManager.OnGameFinished += FinishGame;

        Invoke("JumpOut", 0.5f);
    }
    private void RestartGame()
    {
        EventManager.OnRestartGameSelected += RestartGame;
        EventManager.OnCharacterJumped += Jump;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += MainMenuSelected;
        EventManager.OnGameFinished += FinishGame;

        Invoke("JumpOut", 0.5f);
    }
    private void JumpOut()
    {
        rb.simulated = true;
        isPaused = false;
        isGrounded = true;
        PositionCharacter();
        anim.SetTrigger("Start");
        Run(speed);
    }
    private void PauseGame()
    {
        isPaused = true;
        rb.simulated = false;
        anim.speed = 0;
        Run(0);
    }
    private void ResumeGame()
    {
        isPaused = false;
        rb.simulated = true;
        anim.speed = 1;
        Run(speed);
    }

    private void Jump()
    {
        if (!isGrounded)
        { 
            return; 
        }

        isGrounded = false;
        anim.SetTrigger("Jump");
        Invoke("Impulse", 0.05f);
        EventManager.OnCharacterJumpCount();
    }
    private void Impulse()
    {
        rb.velocity = Vector2.up * jumpForce;
    }
    private void StrongJump()
    {
        isGrounded = false;
        anim.SetTrigger("Jump");
        Invoke("StrongImpulse", 0.03f);
        EventManager.OnCharacterTrampoline();
    }
    private void StrongImpulse()
    {
        rb.velocity = Vector2.up * jumpForce * 1.3f;
    }
    private void Hit()
    {
        isGrounded = false;
        rb.velocity = Vector2.up * jumpForce/2;
        anim.SetTrigger("Hit");

        EventManager.OnCharacterHit();
    }

    private void Run(float speed)
    {
        StopAllCoroutines();
        anim.SetFloat("SpeedX", speed);
        StartCoroutine(RunRoutine(speed));
    }
    private void MainMenuSelected()
    {
        EventManager.OnRestartGameSelected -= RestartGame;
        EventManager.OnCharacterJumped -= Jump;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= MainMenuSelected;
        EventManager.OnGameFinished -= FinishGame;

        StopAllCoroutines();
        isPaused = true;
        anim.speed = 1;
        anim.SetTrigger("Hide");
    }

    private void FinishGame()
    {
        EventManager.OnCharacterJumped -= Jump;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= MainMenuSelected;
        EventManager.OnGameFinished -= FinishGame;

        StopAllCoroutines();
        isPaused = true;
        anim.speed = 1;
        anim.SetTrigger("Hide");
    }


    private void FixedUpdate()
    {
        if (isPaused)
        {
            return;
        }

        anim.SetFloat("SpeedY", rb.velocity.y);
    }
    
    IEnumerator RunRoutine(float speed)
    {
        while (true)
        {
            EventManager.OnCharacterMove(speed);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Trampoline") && collision.enabled)
        {
            StrongJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            //EventManager.OnGamePause();
            Hit();
        }
    }

    #endregion Private Helpers
}
