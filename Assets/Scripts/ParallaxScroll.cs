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
    [SerializeField] public EasingType easing = EasingType.Linear;
    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut }
    private float currentPosition;

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

    private void MoveParallax(float speed)
    {
        currentPosition += speed * speedMult;
        material.SetVector("_Offset", new Vector4(currentPosition, 0, 0, 0));
    }

    public void ScrollForDuration(ParallaxProperties pp)
    {
        StartCoroutine(ScrollForRoutine(pp));
    }

    IEnumerator ScrollForRoutine(ParallaxProperties pp)
    {
        float elapsedTime = 0f;

        while (elapsedTime < pp.duration)
        {
            float t = elapsedTime / pp.duration;
            float easedSpeed = pp.speed * ApplyEasing(t);
            MoveParallax(easedSpeed);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    // Function to apply different easing functions based on the selected type
    float ApplyEasing(float t)
    {
        switch (easing)
        {
            case EasingType.EaseIn:
                return EaseIn(t);
            case EasingType.EaseOut:
                return EaseOut(t);
            case EasingType.EaseInOut:
                return EaseInOut(t);
            default:
                return t; // Linear
        }
    }

    // Ease-in function (slow at the beginning, then fast)
    float EaseIn(float t)
    {
        return t * t; // Quadratic easing in
    }

    // Ease-out function (fast at the beginning, then slow)
    float EaseOut(float t)
    {
        return t * (2 - t); // Quadratic easing out
    }

    float EaseInOut(float t)
    {
        return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }
}