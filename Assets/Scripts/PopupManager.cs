using System;
using UnityEngine;

public class PopupProperties
{
    private PopupProperties()
    {
    }

    public PopupProperties(string _body, string _title, string _primaryButtonText, Action _primaryCallback = null, string _secondaryButtonText = null, Action _secondaryCallback = null)
    {
        // Minimum requirements
        body = _body;
        title = _title;
        primaryButtonText = _primaryButtonText;

        // Optional requirements
        primaryCallback = _primaryCallback;
        secondaryButtonText = _secondaryButtonText;
        secondaryCallback = _secondaryCallback;
    }

    private string body;
    private string title = "";
    private string primaryButtonText = "";
    private string secondaryButtonText = "";
    private Action primaryCallback = null;
    private Action secondaryCallback = null;


    public string Body { get { return body; } private set { body = value; } }
    public string Title { get { return title; } private set { title = value; } }
    public string PrimaryButtonText { get { return primaryButtonText; } private set { primaryButtonText = value; } }
    public string SecondaryButtonText { get { return secondaryButtonText; } private set { secondaryButtonText = value; } }
    public Action PrimaryCallback { get { return primaryCallback; } private set { primaryCallback = value; } }
    public Action SecondaryCallback { get { return secondaryCallback; } private set { secondaryCallback = value; } }
}

public class PopupManager : MonoBehaviour
{
    [SerializeField] PopupBase prefab;

    // Singleton start
    private void Awake()
    {
        EventManager.OnCreatePopup += OnCreatePopup;
    }

    private void OnDestroy()
    {
        EventManager.OnCreatePopup -= OnCreatePopup;
    }


    private void OnCreatePopup(PopupProperties popupProperties)
    {
        var popup = Instantiate(prefab, gameObject.transform);
        popup.Initialize(popupProperties);
    }
}