using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class DolphinSimulation : MonoBehaviour
{
    [SerializeField]
    private int boidCount = 0;
    [SerializeField]
    private Transform orca;
    [SerializeField]
    private Transform cameraEye;
    [SerializeField]
    private DolphinParam param;
    private List<DolphinBoid> boids_ = new List<DolphinBoid>();
    [SerializeField]
    private MyCinemachineDollyCart dolly;
    [SerializeField]
    private splineMove s;

    private bool ChangeTarget = false;

    private Vector3 targetPos;
    private Vector3 targetRot;

    public List<DolphinBoid> boids
    {
        get { return boids_; }
    }
    public Vector3 TargetPos
    {
        get { return targetPos; }
    }
    public Vector3 TargetRot
    {
        get { return targetRot; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!ChangeTarget)
        {
            targetPos = dolly.forwardPos;
            targetRot= dolly.forward * Vector3.forward;
        }
        else
        {
            targetPos = transform.position;
            targetRot = transform.rotation * Vector3.forward;
        }
    }

    public void InitBoid(DolphinBoid boid)
    {
        boid.simulation = this;
        boid.param = param;
        boid.orca = orca;
        boid.cameraEye = cameraEye;
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
    private void OnTriggerEnter(Collider other)
    {
        if (ChangeTarget)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            if (boids_.Count == 0)
                return;
            //foreach (var dolphin in boids_)
               // dolphin.GetComponent<DolphinState>().ChangeState(gameObject.tag, this.gameObject);

            s.StartMove();
            ChangeTarget = true;

        }
    }
}
