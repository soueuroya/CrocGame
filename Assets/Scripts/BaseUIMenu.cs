using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIMenu : MonoBehaviour
{
    //[SerializeField] List<Transform> menuObjects;
    [SerializeField] protected Animator anim;

    private void OnValidate()
    {
        anim = GetComponent<Animator>();
    }
    
    public void Show()
    {
        anim.SetTrigger("Show");
    }

    public void Hide()
    {
        anim.SetTrigger("Hide");
    }

    public void Hidden()
    {
        gameObject.SetActive(false);
    }
}