using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SWS;

public class WakeSeq : MonoBehaviour
{
    [SerializeField]
    private GameObject soulOrca;
    [SerializeField]
    private GameObject boids;
    [SerializeField]
    private Color soulColor;
    [SerializeField]
    private splineMove splineRoute;
    [SerializeField]
    private splineMove lastRoute;
    [SerializeField]
    private ParticleSystem orcaTrail;
    [SerializeField]
    private Material soulHand;

    private Animator orcaAnimator;
    private Material orcaMat;
    private SkinnedMeshRenderer[] hands;

    // Start is called before the first frame update
    void Start()
    {
        orcaAnimator = soulOrca.GetComponent<Animator>();
        orcaMat = soulOrca.GetComponentInChildren<SkinnedMeshRenderer>().material;

        soulOrca.SetActive(false);

        var viveInstance = Object.FindObjectsOfType<HI5.HI5_VIVEInstance>();
        hands = new SkinnedMeshRenderer[viveInstance.Length];
        for(int i=0;i<hands.Length;i++)
        {
            hands[i] = viveInstance[i].GetComponentInChildren<SkinnedMeshRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSeq()
    {
        StartCoroutine("Wake");
    }

    private IEnumerator Wake()
    {
        foreach (var h in hands)
        {
            if(h != null)
            h.material = soulHand;
        }

        GameObject.Find("OrcaModel").SetActive(false);
        yield return null;
        soulOrca.SetActive(true);

        orcaMat.DOColor(soulColor, "_Color", 1f);
        orcaMat.DOFloat(0.8f, "_RimStrength", 1f);
        orcaAnimator.SetTrigger("Start");
        orcaTrail.Play();

        Instantiate(boids);

        yield return new WaitForSeconds(2f);
        splineRoute.StartMove();

        yield return new WaitForSeconds(0.3f);
        soulOrca.transform.parent = splineRoute.transform;
        soulOrca.transform.DORotate(new Vector3(0, 90, 0), 2f);

        yield return new WaitForSeconds(splineRoute.speed - 0.5f);
        soulOrca.transform.parent = lastRoute.transform;
        soulOrca.transform.DOLocalRotate(new Vector3(0, 180, 0), 5);
    }
}
