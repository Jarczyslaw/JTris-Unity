using UnityEngine;
using System.Collections;

public class GenericScreen : MonoBehaviour
{
    public GraphicalUI gui;

    public virtual void Init()
    {

    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}


