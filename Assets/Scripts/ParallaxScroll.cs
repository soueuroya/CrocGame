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
        material = GetComponent<Image>().material;
    }

    private void Awake()
    {
        currentPosition = material.GetVector("_Offset").x;
        EventManager.OnCharacterMove += MoveParallax;
        EventManager.OnScrollForDuration += ScrollForDuration;
    }

    private void OnDestroy()
    {
        EventManager.OnCharacterMove -= MoveParallax;
        EventManager.OnScrollForDuration -= ScrollForDuration;
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