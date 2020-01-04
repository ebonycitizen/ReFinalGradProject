using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowSoulOrca : MonoBehaviour
{
    [HideInInspector]
    public bool startShow;

    [SerializeField]
    private GameObject soulGameObj;
    [SerializeField]
    private GameObject soulBall;

    private Material soulOrca;
    private Material soulBallMat;
    private float rimStrength;
    private float ballOpacity;

    private MyCinemachineDollyCart dollyCart;
    private float dollySpeed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        dollyCart = GameObject.FindObjectOfType<MyCinemachineDollyCart>();

        startShow = false;
        soulOrca = soulGameObj.GetComponentInChildren<SkinnedMeshRenderer>().material;

        rimStrength = soulOrca.GetFloat("_RimStrength");
        soulOrca.SetFloat("_RimStrength", 10f);

        soulBallMat = soulBall.GetComponentInChildren<MeshRenderer>().material;
        ballOpacity = soulBallMat.GetFloat("_Globalopacity");
        soulBallMat.SetFloat("_Globalopacity", 0f);

        soulGameObj.transform.parent = soulBall.transform;

        soulGameObj.SetActive(false);
        soulBall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startShow)
        {
            StartCoroutine("StartShowSoul");
            startShow = false;
        }
    }

    private IEnumerator ChangeSpeed()
    {
        float time = 0;
        float curSpeed = dollyCart.m_Speed;
        while (time < 1)
        {
            dollyCart.m_Speed = Mathf.Lerp(curSpeed, dollySpeed, time);
            time += Time.deltaTime / 1f;
            yield return null;
        }

        dollyCart.m_Speed = dollySpeed;

        yield return null;
    }

    private IEnumerator StartShowSoul()
    {
        soulBall.SetActive(true);
        soulBallMat.DOFloat(1f, "_Globalopacity", 4f);
        yield return new WaitForSeconds(3f);

        soulGameObj.SetActive(true);
        soulOrca.DOFloat(rimStrength, "_RimStrength", 3f);

        yield return new WaitForSeconds(4f);
        StartCoroutine("ChangeSpeed");
    }
}
