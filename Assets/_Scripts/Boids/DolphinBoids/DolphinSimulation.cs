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
    [SerializeField]
    private MyCinemachineDollyCart dolly;

    public List<DolphinBoid> boids
    {
        get { return boids_; }
    }
    public Vector3 TargetPos
    {
        get { return dolly.forwardPos; }
    }
    public Vector3 TargetRot
    {
        get { return dolly.forward * Vector3.forward; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitBoid(DolphinBoid boid)
    {
        boid.simulation = this;
        boid.param = param;
        boid.orca = orca;
        boid.cameraRig = cameraRig;
    }
    public void AddBoid(DolphinBoid boid)
    {
        
        boids_.Add(boid);

        boidCount = boids_.Count;
    }
    public void RemoveVoid(DolphinBoid boid)
    {
        boids_.Remove(boid);
    }
}
