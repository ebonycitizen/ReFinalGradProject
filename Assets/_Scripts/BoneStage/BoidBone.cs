using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBone : EmissionAction
{
    private Simulation simulation;

    private int boidCount;
    // Start is called before the first frame update
    void Start()
    {
        simulation = GetComponent<Simulation>();
        boidCount = simulation.boidCount;
        simulation.boidCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoAction()
    {
        simulation.boidCount = boidCount;
    }
}
