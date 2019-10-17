using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Renderer renderer;

    public void DoFadeOut(float duration)
    {
        renderer.material.DOFade(0, duration);
    }
}
