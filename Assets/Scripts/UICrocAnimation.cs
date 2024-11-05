using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrocAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnValidate()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.OnStartGameSelected += OnStartGameSelected;
        EventManager.OnOptionsSelected += OnStartGameSelected;
        EventManager.OnExitGameSelected += OnStartGameSelected;
        EventManager.OnMainMenuSelected += OnMainMenuSelected;

        EventManager.OnStartHovered += OnStartHovered;
        EventManager.OnStartUnhovered += OnStartUnhovered;
    }

    private void OnDisable()
    {
        EventManager.OnStartGameSelected -= OnStartGameSelected;
        EventManager.OnOptionsSelected -= OnStartGameSelected;
        EventManager.OnExitGameSelected -= OnStartGameSelected;
        EventManager.OnMainMenuSelected -= OnMainMenuSelected;

        EventManager.OnStartHovered -= OnStartHovered;
        EventManager.OnStartUnhovered -= OnStartUnhovered;
    }


    public void OnStartGameSelected()
    {
        anim.SetTrigger("Start");
    }

    public void OnStartHovered()
    {
        anim.SetBool("Hover", true);
    }

    public void OnStartUnhovered()
    {
        anim.SetBool("Hover", false);
    }

    public void OnMainMenuSelected()
    {
        anim.SetTrigger("Return");
    }
}
