using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialUISeq : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorial_1;
    [SerializeField]
    private RectTransform tutorialText_1;


    // Start is called before the first frame update
    void Start()
    {
        Seq1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Seq1()
    {
        Sequence s_1 = DOTween.Sequence();
        Sequence s_2 = DOTween.Sequence();

        var textGroup = tutorial_1.GetComponentInChildren<CanvasGroup>();
        var duration = 1f;
        var scale = tutorial_1.transform.localScale;
        var pos = tutorialText_1.anchoredPosition;
        var posOffset = new Vector2(0, 20);

        s_1.Append(textGroup.DOFade(0f, 0f))
            .AppendInterval(1f)
            .Append(textGroup.DOFade(1f, duration))
            .AppendCallback(() => s_2.Play());

        s_2 .Append(tutorialText_1.DOAnchorPos(pos - posOffset , duration * 0.5f))
            .Append(tutorialText_1.DOAnchorPos(pos, duration))
            .OnComplete(() => DisableSeq(textGroup, tutorial_1))
            .SetLoops(10);

        s_1.Play();
    }
    private void DisableSeq(CanvasGroup group, GameObject text)
    {
        group.DOFade(0f, 1f);
        text.SetActive(false);
    }
}
