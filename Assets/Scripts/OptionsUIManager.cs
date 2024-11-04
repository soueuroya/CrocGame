using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIManager : BaseUIMenu
{
    [SerializeField] Button deleteDataButton;


    #region Initialization
    private void OnValidate()
    {
        deleteDataButton = GameObject.Find("DeleteButton")?.GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (deleteDataButton != null)
        {
            deleteDataButton.onClick.AddListener(EventManager.OnDataDelete);
        }
    }

    private void OnDisable()
    {
        if (deleteDataButton != null)
        {
            deleteDataButton.onClick.RemoveListener(EventManager.OnGameStart);
        }
    }
    #endregion Initialization
}

