using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGem : MonoBehaviour
{
    [SerializeField]
    private Transform rayPos;
    [SerializeField]
    private Transform player;

    private float rayLegth = 140;
    RaycastHit hit;
    private Vector3 rayDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayDirection = (rayPos.position - transform.position).normalized;
        bool isHit = Physics.SphereCast(transform.position, 8, rayDirection, out hit, rayLegth, 1 << 18);

        if (isHit)
        {
            hit.transform.gameObject.GetComponent<Gem>().Hit(player);
        }
    }
}
