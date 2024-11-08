using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<Spawnable> spawnablePrefabs;
    [SerializeField] private List<Spawnable> forcedOnlyPrefabs; // New list for forced-only objects
    [SerializeField] private Transform topSpawnPosition;
    [SerializeField] private Transform bottomSpawnPosition;
    [SerializeField] private Transform poolContainer;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float forcedSpawnDelay = 0.5f;
    [SerializeField] private float screenLeftEdge = -10f;
    [SerializeField] private int initialPoolSize = 5;
    [SerializeField] Vector3 offset;

    private Dictionary<GameObject, ObjectPool> objectPools;
    private Spawnable lastSpawnedObject;
    private float nextSpawnTime;
    private float remainingTimeWhenPaused;
    private bool waitingForForcedSpawn;
    private bool isPaused;

    private void Start()
    {
        isPaused = true;
        InitializePools();
        PositionSpawner();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Awake()
    {
        EventManager.OnStartGameSelected += StartGame;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += TurnOffAllObjects;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= TurnOffAllObjects;
    }

    private void Update()
    {
        if (isPaused)
            return;

        CheckObjectsOffScreen();

        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();

            if (lastSpawnedObject != null && lastSpawnedObject.ForcedNextSpawnable != null)
            {
                nextSpawnTime = Time.time + forcedSpawnDelay;
                waitingForForcedSpawn = true;
            }
            else
            {
                nextSpawnTime = Time.time + spawnInterval;
                waitingForForcedSpawn = false;
            }
        }
    }

    private void InitializePools()
    {
        objectPools = new Dictionary<GameObject, ObjectPool>();

        // Initialize pools for regular spawnable prefabs
        foreach (Spawnable prefab in spawnablePrefabs)
        {
            ObjectPool pool = new ObjectPool(prefab.gameObject, initialPoolSize, poolContainer);
            objectPools.Add(prefab.gameObject, pool);
        }

        // Initialize pools for forced-only prefabs
        foreach (Spawnable prefab in forcedOnlyPrefabs)
        {
            ObjectPool pool = new ObjectPool(prefab.gameObject, initialPoolSize, poolContainer);
            objectPools.Add(prefab.gameObject, pool);
        }
    }

    private void SpawnObject()
    {
        if (spawnablePrefabs.Count == 0 && !waitingForForcedSpawn)
        {
            Debug.LogWarning("No spawnable prefabs assigned.");
            return;
        }

        Spawnable selectedPrefab;

        // Check if we need to spawn a forced object
        if (waitingForForcedSpawn && lastSpawnedObject != null && lastSpawnedObject.ForcedNextSpawnable != null)
        {
            selectedPrefab = lastSpawnedObject.ForcedNextSpawnable;

            // Validate that the forced spawnable exists in either list
            if (!spawnablePrefabs.Contains(selectedPrefab) && !forcedOnlyPrefabs.Contains(selectedPrefab))
            {
                Debug.LogWarning($"Forced spawnable {selectedPrefab.name} not found in any spawn list.");
                return;
            }

            waitingForForcedSpawn = false;
        }
        else
        {
            selectedPrefab = GetRandomSpawnable();
        }

        if (selectedPrefab != null)
        {
            Transform spawnPoint = bottomSpawnPosition;

            switch (selectedPrefab.AllowedSpawnPosition)
            {
                case Spawnable.SpawnPosition.Top:
                    spawnPoint = topSpawnPosition;
                    break;

                case Spawnable.SpawnPosition.Both:
                    spawnPoint = Random.value > 0.5f ? topSpawnPosition : bottomSpawnPosition;
                    break;
            }

            GameObject spawnedObject = objectPools[selectedPrefab.gameObject].GetObject();
            spawnedObject.transform.position = spawnPoint.position;
            lastSpawnedObject = spawnedObject.GetComponent<Spawnable>();
        }
        else
        {
            Debug.LogWarning("No prefabs matched the spawn position requirements.");
        }
    }

    private Spawnable GetRandomSpawnable()
    {
        return spawnablePrefabs[Random.Range(0, spawnablePrefabs.Count)];
    }

    #region Game State Management
    private void StartGame()
    {
        Invoke("StartSpawning", 2.5f);
    }

    private void StartSpawning()
    {
        isPaused = false;
    }

    private void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            remainingTimeWhenPaused = nextSpawnTime - Time.time;

            //foreach (Transform child in poolContainer)
            //{
            //    if (child.gameObject.activeInHierarchy)
            //    {
            //        Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            //        if (rb != null)
            //        {
            //            rb.velocity = Vector2.zero;
            //        }
            //    }
            //}
        }
    }

    private void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            nextSpawnTime = Time.time + remainingTimeWhenPaused;

            //foreach (Transform child in poolContainer)
            //{
            //    if (child.gameObject.activeInHierarchy)
            //    {
            //        Spawnable spawnable = child.GetComponent<Spawnable>();
            //        if (spawnable != null)
            //        {
            //            spawnable.OnEnable();
            //        }
            //    }
            //}
        }
    }

    private void PositionSpawner()
    {
        Vector3 bottomRightScreen = new Vector3(1, 0, Camera.main.nearClipPlane);
        Vector3 bottomRightWorld = Camera.main.ViewportToWorldPoint(bottomRightScreen);
        transform.position = bottomRightWorld + offset;
    }

    private void CheckObjectsOffScreen()
    {
        foreach (Transform child in poolContainer)
        {
            if (child.gameObject.activeInHierarchy && child.position.x < screenLeftEdge)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void TurnOffAllObjects()
    {
        isPaused = true;
        foreach (Transform child in poolContainer)
        {
            child.gameObject.SetActive(false);
        }
    }
    #endregion
}