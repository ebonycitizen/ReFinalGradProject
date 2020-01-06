using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using SWS;

public class EndSeq : MonoBehaviour
{
    [SerializeField]
    private splineMove lastRoute;

    private MyCinemachineDollyCart dollyCart;
    private float dollySpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = GameObject.FindObjectOfType<MyCinemachineDollyCart>();
        StartCoroutine("InitRoute");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartSeq();
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
        lastRoute.Resume();
        yield return new WaitForSeconds(2f);
        StartCoroutine("ChangeSpeed");
        yield return new WaitForSeconds(6f);

        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 2f);
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

}
