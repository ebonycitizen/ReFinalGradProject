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
        if (GameObject.Find("OrcaSoulBall") == null)
            return;
        if(GameObject.Find("OrcaSoulBall").GetComponent<Collider>().enabled == false)
        {
            StartCoroutine("Dissapear");
        }
    }

    private IEnumerator Dissapear()
    {
        text.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void Show()
    {
        if (GameObject.Find("OrcaSoulBall").GetComponent<Collider>().enabled == false)
            return;
        text.DOFade(1f, 1f).SetDelay(15f);
    }
}
