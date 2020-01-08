using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowTouchUI : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.DOFade(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        text.DOFade(1f, 1f).SetDelay(15f);
    }
}
