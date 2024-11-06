using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIMenu : MonoBehaviour
{
    //[SerializeField] List<Transform> menuObjects;
    [SerializeField] protected Animator anim;

    #region Initialization
    private void OnValidate()
    {
        anim = GetComponent<Animator>();
    }
    #endregion Initialization

    #region Public Methods
    public void Show()
    {
        anim.SetTrigger("Show");
    }

    public void Hide()
    {
        anim.SetTrigger("Hide");
    }
        
    public void OnHide()
    {
        gameObject.SetActive(false);
        EventManager.OnInputUnlock();
    }
    #endregion Public Methods
}