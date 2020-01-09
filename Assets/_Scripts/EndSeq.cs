using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using SWS;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndSeq : MonoBehaviour
{
    [SerializeField]
    private splineMove lastRoute;
    [SerializeField]
    private ParticleSystem heart;
    [SerializeField]
    private Speaker speaker;

    private MyCinemachineDollyCart dollyCart;
    private float dollySpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = GameObject.FindObjectOfType<MyCinemachineDollyCart>();
        //StartCoroutine("InitRoute");

        Sequence s = DOTween.Sequence();

        s.AppendCallback(() => lastRoute.StartMove())
            .AppendInterval(1f)
            .AppendCallback(() => lastRoute.Pause());

        s.Play();
    }

    // Update is called once per frame
    void Update()
    {
//#if DEBUG
        if (Input.GetKeyDown(KeyCode.Space))
            StartSeq();
//#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PalmTrigger")
        {
            var grab = other.GetComponent<Grab>();
            if (grab.GetIsRightHand())
                HI5.HI5_Manager.EnableRightVibration(500);
            else
                HI5.HI5_Manager.EnableRightVibration(500);

            StartSeq();
        }
    }

    private IEnumerator InitRoute()
    {
        lastRoute.StartMove();
        yield return new WaitForSeconds(1f);
        lastRoute.Pause();
    }

    private void StartSeq()
    {
        StartCoroutine("End");
    }

    private IEnumerator End()
    {
        GetComponent<Collider>().enabled = false;
        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Orac_4, speaker);
        heart.Play();
        yield return new WaitForSeconds(0.5f);

        lastRoute.Resume();
        yield return new WaitForSeconds(3f);
        StartCoroutine("ChangeSpeed");
        yield return new WaitForSeconds(6f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 8f);

        yield return new WaitForSeconds(8f);
        SceneManager.LoadSceneAsync("ThankF", LoadSceneMode.Single);
    }

    private IEnumerator ChangeSpeed()
    {
        float time = 0;
        float curSpeed = dollyCart.m_Speed;
        while (time < 1)
        {
            dollyCart.m_Speed = Mathf.Lerp(curSpeed, dollySpeed, time);
            time += Time.deltaTime / 3f;
            yield return null;
        }

        dollyCart.m_Speed = dollySpeed;

        yield return null;
    }

}
