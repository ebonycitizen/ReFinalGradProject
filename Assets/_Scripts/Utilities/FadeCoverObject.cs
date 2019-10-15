using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeCoverObject : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float fadeTime;

    private RaycastHit hit;
    private bool isHit;

    private Vector3 rayDirection;
    private float rayDistance;

    private MeshRenderer hitRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayDirection = (target.position - origin.position).normalized;
        rayDistance = Vector3.Distance(target.position, origin.position);

        isHit = Physics.Raycast(origin.position, rayDirection, out hit, rayDistance, 1 << LayerMask.NameToLayer("Stage"));

        if(isHit)
        {
            hitRenderer = hit.transform.GetComponent<MeshRenderer>();

            hitRenderer.material.DOFade(0.5f, fadeTime);     
        }
        else
        {
            if (hitRenderer == null)
                return;

            hitRenderer.material.DOFade(1f, fadeTime);
            hitRenderer = null;
        }
    }
}
