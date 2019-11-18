using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Crack : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();

        Sequence s = DOTween.Sequence();
        s.Append(DOTween.ToAlpha(() => image.color, color => image.color = color, 0f, 2f))
        .AppendCallback(() => Destroy(gameObject));
        s.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
