using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoSeq : MonoBehaviour
{
    [SerializeField]
    private Image logo1;
    [SerializeField]
    private Image logo2;
    [SerializeField]
    private Image logo3;
    [SerializeField]
    private Image logo4;
    [SerializeField]
    private Image logo5;
    [SerializeField]
    private Image logo6;

    [SerializeField]
    private Image bg;

    [SerializeField]
    private SkinnedMeshRenderer orcaLogo;
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private GameObject postEffect;

    // Start is called before the first frame update
    void Start()
    {
        logo1.color = new Color(0, 0, 0, 0);
        logo2.fillAmount = 0;
        logo3.color = new Color(0, 0, 0, 0);
        logo4.color = new Color(0, 0, 0, 0);
        logo5.color = new Color(0, 0, 0, 0);
        logo6.color = new Color(0, 0, 0, 0);

        effect.SetActive(false);
        //postEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLogoSeq()
    {
        StartCoroutine("LogoStart");
    }

    private IEnumerator LogoStart()
    {
        var duration = 1f;

        yield return new WaitForSeconds(duration / 2);

        orcaLogo.material.DOColor(new Color(1, 1, 1, 0), duration);
        logo1.DOColor(Color.white, duration);
        yield return new WaitForSeconds(duration);

        logo2.DOFillAmount(1f, duration);
        yield return new WaitForSeconds(duration);

        logo3.DOColor(Color.white, duration / 2);
        yield return new WaitForSeconds(duration / 2);

        logo4.DOColor(Color.white, duration / 2);
        yield return new WaitForSeconds(duration / 2);

        logo5.DOColor(Color.white, duration);
        yield return new WaitForSeconds(duration);

        logo6.DOColor(Color.white, duration);
        yield return new WaitForSeconds(duration);

        bg.DOColor(new Color(0, 0, 0, 0), duration * 2);
        effect.SetActive(true);
        //postEffect.SetActive(true);
        yield return new WaitForSeconds(duration * 2);
    }
}
