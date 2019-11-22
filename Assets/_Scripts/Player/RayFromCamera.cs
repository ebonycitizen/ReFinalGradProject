using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFromCamera : MonoBehaviour
{
    [SerializeField]
    private Transform rayPos;

    private float rayLegth = 150;
    private RaycastHit hit;
    private Vector3 rayDirection;
    private GameObject hitObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public GameObject LockOn(LayerMask layerMask)
    {
        rayDirection = (rayPos.position - transform.position).normalized;
        
        Debug.DrawRay(transform.position, rayDirection * rayLegth);
        bool isHit = Physics.Raycast(transform.position, rayDirection, out hit, rayLegth, layerMask);

        if (isHit)
            return hit.transform.gameObject;

        return null;
    }
    public Vector3 ScrollHit(string layer)
    {
        rayDirection = (rayPos.position - transform.position).normalized;

        Debug.DrawRay(transform.position, rayDirection * rayLegth);
        bool isHit = Physics.Raycast(transform.position, rayDirection, out hit, rayLegth, 1<<LayerMask.NameToLayer(layer));

        if (isHit)
            return hit.point;

        return Vector3.zero;
    }
}
