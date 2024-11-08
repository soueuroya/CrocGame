using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxProperties
{
    public float duration;
    public float speed;
}

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float speedMult;
    private float currentPosition;

    #region Initialization
    private void OnValidate()
    {
        if (material == null)
        {
            material = GetComponent<Image>().material;
        }

        currentPosition = 0;
    }

    private void Awake()
    {
        currentPosition = material.GetVector("_Offset").x;
        EventManager.OnCharacterMoved += MoveParallax;
        EventManager.OnScrolledForDuration += ScrollForDuration;
    }

    private void OnDestroy()
    {
        EventManager.OnCharacterMoved -= MoveParallax;
        EventManager.OnScrolledForDuration -= ScrollForDuration;
    }
    #endregion Initialization


    #region Public Methods
    public void ScrollForDuration(ParallaxProperties pp)
    {
        StopAllCoroutines();
        StartCoroutine(ScrollForRoutine(pp));
    }
    #endregion Public Methods

    #region Private Helpers
    private void MoveParallax(float speed)
    {
        currentPosition += speed * speedMult;
        material.SetVector("_Offset", new Vector4(currentPosition, 0, 0, 0));
    }
    IEnumerator ScrollForRoutine(ParallaxProperties pp)
    {
        float elapsedTime = 0f;

        while (elapsedTime < pp.duration)
        {
            MoveParallax(pp.speed);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    #endregion Private Helpers
}