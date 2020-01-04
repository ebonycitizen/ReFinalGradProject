using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGlitterDelay : MonoBehaviour
{
    [SerializeField]
    private GameObject glitter;
    [SerializeField]
    private float delay;

    public void ShowGlitter()
    {
        StartCoroutine("Show");
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(delay);
        glitter.SetActive(true);
    }

}
