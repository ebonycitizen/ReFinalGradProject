using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class DolphinSimulation : MonoBehaviour
{
    enum State
    {
        Enter,
        Jump,
        Exit
    }

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
    [SerializeField]
    private splineMove s2;
    [SerializeField]
    private Transform exitPos;
    private State state = State.Enter;

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

    private void Awake()
    {
        dolly = GameObject.FindObjectOfType<MyCinemachineDollyCart>();
        orca = GameObject.Find("OrcaModel").transform;
        cameraEye = GameObject.Find("Main Camera (eye)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Enter:
                targetPos = dolly.forwardPos;
                targetRot= dolly.forward * Vector3.forward;
                break;
            case State.Jump:
            targetPos = transform.position;
            targetRot = transform.rotation * Vector3.forward;
                 break;
            case State.Exit:
                targetPos = exitPos.position;
                targetRot = exitPos.rotation * Vector3.forward;
                break;
                
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
        if (state==State.Jump)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            if (boids_.Count == 0)
                return;
            //foreach (var dolphin in boids_)
               // dolphin.GetComponent<DolphinState>().ChangeState(gameObject.tag, this.gameObject);

            s.StartMove();
            state = State.Jump;

        }
    }
    public void ChangeExit()
    {
        if (state == State.Exit)
            return;
        s2.StartMove();
        s.Stop();
        state = State.Exit;

    }
}
