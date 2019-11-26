using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class EnableSplineMove : MonoBehaviour
{
    [SerializeField]
    private splineMove[] splineMoves;

    // Start is called before the first frame update
    void Start()
    {
        //splineMoves = Object.FindObjectsOfType<splineMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            foreach (splineMove s in splineMoves)          
                s.StartMove();
            
        }
    }
}
