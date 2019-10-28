using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFromCamera : MonoBehaviour
{
    [SerializeField]
    private Transform rayPos;

    private float rayLegth = 100;
    private RaycastHit hit;
    private Vector3 rayDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject LockOn(LayerMask layerMask)
    {
        rayDirection = (rayPos.position - transform.position).normalized;
        //int layerMask = LayerMask.NameToLayer(layer);
        Debug.DrawRay(transform.position, rayDirection * rayLegth);
        bool isHit = Physics.Raycast(transform.position, rayDirection, out hit, rayLegth, layerMask);

        if (isHit)
            return hit.transform.gameObject;

        return null;
    }
}
