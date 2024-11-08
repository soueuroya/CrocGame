using System.Collections.Generic;
using UnityEngine;

public class LifesContainer : MonoBehaviour
{
    [SerializeField] List<GameObject> icons;

    private void Awake()
    {
        EventManager.OnLifesChanged += LifesChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnLifesChanged -= LifesChanged;
    }

    private void LifesChanged(int currentLifes)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            if (i < currentLifes)
            {
                icons[i].SetActive(true);
            }
            else
            {
                icons[i].SetActive(false);
            }
        }
    }
}
