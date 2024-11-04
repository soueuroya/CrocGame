using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIMenu : MonoBehaviour
{
    [SerializeField] Transform menuObjectsContainer;
    [SerializeField] List<Transform> menuObjects;
    [SerializeField] Animator anim;

    private void OnValidate()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (var item in menuObjects)
        {
            item.SetParent(menuObjectsContainer);
        }
    }

    public void Show()
    {
        anim.SetTrigger("Show");
    }

    public void Hide()
    {
        anim.SetTrigger("Hide");
    }
}