using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using HI5;

public class Glitter : MonoBehaviour
{
    private Transform orca;
    [SerializeField]
    private GameObject sendObj;

    [SerializeField]
    private ParticleSystem ripple;
    [SerializeField]
    private ParticleSystem startLockOn;
    [SerializeField]
    private ParticleSystem endLockOn;
    [SerializeField]
    private ParticleSystem circle;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color lockonColor;

    private Speaker speaker;

    private void OnEnable()
    {
        //ripple.Stop();
    }

    private void Awake()
    {
        speaker = GetComponentInChildren<Speaker>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startLockOn.Stop();
        endLockOn.Stop();
        //ripple.Stop();

        gameObject.SetActive(false);
        orca = GameObject.Find("OrcaModel").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "G_Approach")
            transform.position = orca.position;
    }

    public void StartUp(OrcaState orcaState, GameObject cameraTarget)
    {
        if (cameraTarget != gameObject)
            return;

        bool hasChangeState = orcaState.ChangeState(gameObject.tag, sendObj);
        if (!hasChangeState)
            return;

        //HI5_Manager.EnableBothGlovesVibration(400, 400);

        GetComponent<Collider>().enabled = false;

        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Sparkle_2, speaker);

        circle.Clear();
        foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }

        endLockOn.Play();

        Destroy(gameObject, 3f);
    }

    public void HitEffect()
    {
        if (!startLockOn.isPlaying)
            startLockOn.Play();
        ripple.Play();

        foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
        {
            p.startColor = lockonColor;
        }
    }

    //public void DisableEffect()
    //{
    //    ripple.Stop();

    //    foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
    //    {
    //        p.startColor = normalColor;
    //    }
    //}

    //public void ChangeEffect(GameObject cameraTarget, GameObject lockTarget)
    //{
    //    if (cameraTarget == gameObject && cameraTarget != lockTarget)
    //    {
    //        if (!startLockOn.isPlaying)
    //            startLockOn.Play();
    //        ripple.Play();

    //        foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
    //        {
    //            p.startColor = lockonColor;
    //        }
    //    }

    //    if (cameraTarget == null && lockTarget == gameObject)
    //    {
    //        ripple.Stop();

    //        foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
    //        {
    //            p.startColor = normalColor;
    //        }
    //    }
    //}
}
