using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinSimulation : MonoBehaviour
{
    [SerializeField]
    private int boidCount = 0;
    [SerializeField]
    private Transform orca;
    [SerializeField]
    private Transform cameraRig;
    [SerializeField]
    private DolphinParam param;
    public List<DolphinBoid> boids_;

    public List<DolphinBoid> boids
    {
        get { return boids_; }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBoid();
    }

    // Update is called once per frame
    void Update()
    {
        while (boids_.Count < boidCount)
        {
            //AddBoid();
        }
    }

    void InitBoid()
    {
        foreach (DolphinBoid boid in boids_)
        {
            boid.simulation = this;
            boid.param = param;
            boid.orca = orca;
            boid.cameraRig = cameraRig;
        }
    }
    void AddBoid(DolphinBoid boid)
    {
        boid.simulation = this;
        boid.param = param;
        boid.orca = orca;
        boid.cameraRig = cameraRig;
        boids_.Add(boid);

        boidCount = boids_.Count;
    }
}
