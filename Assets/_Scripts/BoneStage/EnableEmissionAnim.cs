using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class EnableEmissionAnim : MonoBehaviour
{
    [SerializeField]
    private Material lightMat;

    private Animator animator;
    private splineMove splineMove;
    private void Start()
    {
        animator = GetComponent<Animator>();
        splineMove = GetComponent<splineMove>();
    }

    private IEnumerator Emission()
    {
        GetComponent<Collider>().enabled = false;

        animator.SetTrigger("Start");
        if(splineMove != null)
            splineMove.StartMove();

        var renderer = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var r in renderer)
            r.material = lightMat;

        EmissionAction action = GetComponent<EmissionAction>();
        if (action != null)
            action.DoAction();

        yield return new WaitForSeconds(5f);

        //foreach (var r in renderer)
        //    r.material.DisableKeyword("_EMISSION");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeepLight")
            StartCoroutine("Emission");
    }
}
