using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BrunoMikoski.TextJuicer;

public class TutorialUISeq : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorial_1;
    [SerializeField]
    private RectTransform tutorialText_1;

    [SerializeField]
    private GameObject tutorial_2;

    // Start is called before the first frame update
    void Start()
    {
        var textGroup = tutorial_2.GetComponentInChildren<CanvasGroup>();
        textGroup.DOFade(0f, 0f);

        //Seq1();
        Seq2();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Seq1()
    {
        Sequence s_1 = DOTween.Sequence();
        Sequence s_2 = DOTween.Sequence();
        Sequence s_3 = DOTween.Sequence();

        var textGroup = tutorial_1.GetComponentInChildren<CanvasGroup>();
        var duration = 1f;
        var scale = tutorial_1.transform.localScale;
        var pos = tutorialText_1.anchoredPosition;
        var posOffset = new Vector2(0, 20);

        s_1.Append(textGroup.DOFade(0f, 0f))
            .AppendInterval(1f)
            .Append(textGroup.DOFade(1f, duration))
            .AppendCallback(() => s_2.Play());

        s_2.Append(tutorialText_1.DOAnchorPos(pos - posOffset, duration * 0.5f))
            .Append(tutorialText_1.DOAnchorPos(pos, duration))
            .OnComplete(() => s_3.Play())
            .SetLoops(3);

        s_3.AppendCallback(() => Seq2())
            .AppendInterval(0.5f)
            .Append(textGroup.DOFade(0f, 1f))
            .AppendInterval(1f)
            .AppendCallback(() => tutorial_1.SetActive(false));

        s_1.Play();
    }

    private void Seq2()
    {
        Sequence s_1 = DOTween.Sequence();

        var textGroup = tutorial_2.GetComponentInChildren<CanvasGroup>();
        var anim = tutorial_2.GetComponentInChildren<JuicedText>();
        var duration = 1f;

        s_1.AppendInterval(1f)
            .Append(textGroup.DOFade(1f, duration))
            .AppendCallback(() => anim.Play());
        s_1.Play();
    }
}
