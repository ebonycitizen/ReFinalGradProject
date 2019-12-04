using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGroupFish : MonoBehaviour
{
    [SerializeField]
    private Simulation simulation;
    [SerializeField]
    private int boidCount = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            simulation.boidCount = boidCount;
            Destroy(gameObject);
        }
    }
}
