using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public Text text;
    public float showDuration = 3f;

    private IEnumerator showCoroutine;

    public void Hide()
    {
        StopCoroutine();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StopCoroutine();
        showCoroutine = ShowCoroutine();
        StartCoroutine(showCoroutine);
    }

    public void Show(string message)
    {
        text.text = message;
        Show();
    }

    private IEnumerator ShowCoroutine()
    {
        yield return new WaitForSeconds(showDuration);
        gameObject.SetActive(false);
    }

    private void StopCoroutine()
    {
        if (showCoroutine != null)
            StopCoroutine(showCoroutine);
    }
    
}
