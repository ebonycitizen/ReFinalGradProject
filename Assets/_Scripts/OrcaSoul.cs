using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrcaSoul : MonoBehaviour
{
    [SerializeField]
    private GameObject core;

    [SerializeField]
    private ParticleSystem kira;
    [SerializeField]
    private ParticleSystem light;

    [SerializeField]
    private ParticleSystem action1;
    [SerializeField]
    private ParticleSystem action2;

    [SerializeField]
    private ParticleSystem floorfly;

    [SerializeField]
    private GameObject leaveEffect;
    [SerializeField]
    private WakeSeq wakeSeq;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartCoroutine("StartEffect");
    }

    private IEnumerator StartEffect()
    {
        kira.Play();
        light.Play();

        Vector3 newPos = new Vector3(0, 8, 14);
        float duration = 4f;
        transform.DOMove(transform.position + newPos, duration).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(duration + 0.2f);

        action1.Play();
        action2.Play();
        floorfly.Play();

        yield return null;
        Destroy(core);
        Destroy(kira);
        Destroy(light);

        Instantiate(leaveEffect);
        wakeSeq.StartSeq();
    }
}
