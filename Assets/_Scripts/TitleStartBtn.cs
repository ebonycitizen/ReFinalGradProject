using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HI5.VRInteraction;
using HI5.VRCalibration;
using DG.Tweening;

public class TitleStartBtn : VRButton
{
    [SerializeField]
    private TitleSeq titleSeq;
    [SerializeField]
    private SpriteRenderer sprite;

    new void OnEnable()
    {
        base.OnEnable();
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
    }

    new void OnDisable()
    {
        base.OnDisable();

        //ruige red
        // m_SelectionRadial.Hide();

        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
    }

    private void HandleSelectionComplete()
    {
        if (m_GazeOver)
        {
            Destroy(m_SelectionRadial.gameObject);
            var scale = transform.localScale;

            transform.DOScale(scale * 2, 4f);
            sprite.DOFade(0f, 0.6f);
            titleSeq.StartSeq();
        }
    }
}
