using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DolphinGlitter : MonoBehaviour
{
    [SerializeField]
    private GameObject sendObj;
    [SerializeField]
    private DolphinState dolphinState;

    [SerializeField]
    private ParticleSystem ripple;
    [SerializeField]
    private ParticleSystem startLockOn;
    [SerializeField]
    private ParticleSystem endLockOn;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color lockonColor;

    private Speaker speaker;

    [SerializeField]
    private string sendObjStr = null;

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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartUp(GameObject cameraTarget)
    {
        if (cameraTarget != gameObject)
            return;

        if(sendObj!=null)
            sendObj= GameObject.Find(sendObjStr);

        bool hasChangeState = dolphinState.ChangeState(gameObject.tag, sendObj);
        if (!hasChangeState)
            return;

        //HI5_Manager.EnableBothGlovesVibration(400, 400);

        GetComponent<Collider>().enabled = false;

        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Action, speaker);
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
    
}
