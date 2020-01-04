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

    private Animator orcaAnimator;
    private Material orcaMat;

    // Start is called before the first frame update
    void Start()
    {
        orcaAnimator = soulOrca.GetComponent<Animator>();
        orcaMat = soulOrca.GetComponentInChildren<SkinnedMeshRenderer>().material;
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
        orcaMat.DOColor(soulColor, "_Color", 1f);
        orcaMat.DOFloat(0.8f, "_RimStrength", 1f);
        orcaAnimator.SetTrigger("Start");
        Instantiate(boids);

        yield return new WaitForSeconds(2f);
        splineRoute.StartMove();

        yield return new WaitForSeconds(0.3f);
        soulOrca.transform.parent = splineRoute.transform;
        //soulOrca.transform.DORotate(new Vector3(0, 90, 0), 2f);

        
        //splineRoute.StartMove();
        
    }
}
