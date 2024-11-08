using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public enum SpawnPosition { Top, Bottom, Both }

    [SerializeField] private SpawnPosition allowedSpawnPosition;
    [SerializeField] private Spawnable forcedNextSpawnable; // Optional reference to force next spawn
    [SerializeField] private float moveSpeed = 5f;

    bool isPaused;
    private Rigidbody2D rb;

    public SpawnPosition AllowedSpawnPosition => allowedSpawnPosition;
    public Spawnable ForcedNextSpawnable => forcedNextSpawnable;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        EventManager.OnStartGameSelected += StartGame;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
    }
    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
    }
    public void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf && !isPaused)
        {
            if (rb != null)
            {
                rb.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
    }

    private void StartGame()
    {
        isPaused = false;
    }
    private void PauseGame()
    {
        isPaused = true;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ResumeGame()
    {
        isPaused = false;
    }
}
