using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PopupBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    [SerializeField] TextMeshProUGUI body;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI primaryButtonText;
    [SerializeField] TextMeshProUGUI secondaryButtonText;

    bool isVisible = false;
    Action primaryCallback;
    Action secondaryCallback;

    private void OnValidate()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (secondaryButtonText.transform.parent.gameObject.activeSelf)
            {
                OnSecondaryClick();
            }
            else
            {
                OnPrimaryClick();
            }
        }
    }

    public virtual void Initialize(PopupProperties popupProperties)
    {
        canvasGroup.blocksRaycasts = true;

        // Sender info
        if (!string.IsNullOrEmpty(popupProperties.Body.ToString()))
        {
            body.gameObject.SetActive(true);
            body.text = popupProperties.Body.ToString();
        }
        else
        {
            body.gameObject.SetActive(false);
        }

        // if contains TITLE
        if (!string.IsNullOrEmpty(popupProperties.Title))
        {
            title.gameObject.SetActive(true);
            title.text = popupProperties.Title;
        }
        else
        {
            title.gameObject.SetActive(false);
        }

        // setup primary button text and callback
        primaryButtonText.text = popupProperties.PrimaryButtonText;
        primaryCallback = popupProperties.PrimaryCallback;

        // setup secondary button text
        if (!string.IsNullOrEmpty(popupProperties.SecondaryButtonText))
        {
            secondaryButtonText.transform.parent.gameObject.SetActive(true);
            secondaryButtonText.text = popupProperties.SecondaryButtonText;
        }
        else
        {
            secondaryButtonText.transform.parent.gameObject.SetActive(false);
        }

        // setup secondary button callback
        if (popupProperties.SecondaryCallback != null)
        {
            secondaryCallback = popupProperties.SecondaryCallback;
        }
    }

    public virtual void OnPrimaryClick()
    {
        primaryCallback?.Invoke(); // Send another callback to this function, and on that callback close the popup.
        AudioManager.Instance.PlayClick();
        ClosePopup();
    }

    public virtual void OnSecondaryClick()
    {
        secondaryCallback?.Invoke(); // Send another callback to this function, and on that callback close the popup.
        AudioManager.Instance.PlayFail();
        ClosePopup(); 
    }

    private void ClosePopup()
    {
        Hide(() => { 
            Destroy(this.gameObject);
        });
    }

    protected void Show(Action _callback)
    {
        // Show only if not visible but callback should be called anyway
        if (isVisible)
        {
            _callback?.Invoke();
            return;
        }

        AudioManager.Instance.PlayPopup();
        canvasGroup.blocksRaycasts = true;

        LeanTween.value(canvasGroup.alpha, to: 1, time: AnimationTimers.POPUP_SHOW)
        .setEaseInOutSine()
        .setOnUpdate((float val) =>
        {
            canvasGroup.alpha = val;
        }).setOnComplete(() => {
            isVisible = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            _callback?.Invoke();
        });

        EventSystem.current.SetSelectedGameObject(primaryButtonText.transform.parent.gameObject);
    }

    protected void Hide(Action _callback)
    {
        // Hide only if is visible but callback should be called anyway
        if (!isVisible)
        {
            _callback?.Invoke();
            return;
        }

        canvasGroup.interactable = false;

        LeanTween.value(canvasGroup.alpha, to: 0, time: AnimationTimers.POPUP_HIDE)
        .setEaseInOutSine()
        .setOnUpdate((float val) =>
        {
            canvasGroup.alpha = val;
        }).setOnComplete(() => {
            isVisible = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            _callback?.Invoke();
        });
    }
}
