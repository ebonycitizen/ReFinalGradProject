using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinBoid : MonoBehaviour
{
    //public DolphinSimulation simulation { get; set; }
    //public DolphinParam param { get; set; }
    //public Vector3 pos { get; private set; }
    //public Vector3 velocity { get; private set; }

    public DolphinSimulation simulation;
    public DolphinParam param;
    public Vector3 pos;
    public Vector3 velocity;

    
    public Transform orca;
    public Transform cameraEye;

    public Vector3 accel = Vector3.zero;

    List<Vector3> neighborsPos = new List<Vector3>();

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        velocity = transform.forward * param.initSpeed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNeighbors();

        UpdateWall();

        UpdateSeparation();

        UpdateAlignment();

        UpdateCohesion();

        UpdateMove();
    }

    private void UpdateNeighbors()
    {
        neighborsPos.Clear();

        if (!simulation)
            return;

        var distThreshold = param.neighborDistance;

        if (simulation.boids.Count != 0)
        {
            foreach (var boid in simulation.boids)
            {
                if (boid == this)
                    continue;

                var to = boid.pos - pos;
                var dist = to.magnitude;

                if (dist < distThreshold)
                {
                    neighborsPos.Add(boid.pos);
                }
            }

        }
       
        if ((orca.position - pos).magnitude < distThreshold)
            neighborsPos.Add(orca.position);
        if ((cameraEye.position - pos).magnitude < distThreshold)
            neighborsPos.Add(cameraEye.position);
    }

    private void UpdateWall()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, velocity.normalized * param.wallDistance);
        bool isHit = Physics.Raycast(transform.position, velocity.normalized, out hit, param.wallDistance, 1 << LayerMask.NameToLayer("Stage"));

        if(isHit)
        {
            accel += hit.normal * param.wallWeight;
        }
    }

    private void UpdateSeparation()
    {
        Vector3 force = Vector3.zero;

        if (neighborsPos.Count != 0)
        {
            foreach (var neighborPos in neighborsPos)
            {
                var diff = pos - neighborPos;
                force += diff.normalized * 100.0f / (diff.magnitude * diff.magnitude);
            }

            //force /= neighborsPos.Count;
        }


        accel += force * param.separationWeight;
    }

    private void UpdateAlignment()
    {
        var averaveVelocity = Vector3.zero;

        averaveVelocity = simulation.TargetRot * velocity.magnitude;

        accel += (averaveVelocity - velocity) * param.alignmentWeight;

    }
    void UpdateCohesion()
    {
        var averagePos = Vector3.zero;
        if(neighborsPos.Count!=0)
        {
            foreach(var neighborPos in neighborsPos)
            {
                averagePos += neighborPos;
            }
            averagePos /= neighborsPos.Count;
        }
        
        averagePos += simulation.TargetPos;
        if (neighborsPos.Count != 0)
            averagePos /= 2;

        var diff = pos - simulation.TargetPos;

        accel += (averagePos - pos) * param.cohesionWeight;
        
    }
    //private void UpdateSeparation()
    //{
    //    Vector3 force = Vector3.zero;
    //    if (neighbors.Count != 0)
    //    {
    //        foreach (var neighbor in neighbors)
    //        {
    //            var diff = pos - neighbor.pos;
    //            force += diff.normalized;
    //        }
    //        force /= neighbors.Count;
    //    }

    //    if (neighborPlayers.Count != 0)
    //    {
    //        foreach (var neighborPlayer in neighborPlayers)
    //        {
    //            var diff = pos - neighborPlayer.position;
    //            force += diff.normalized * 10.0f / (diff.magnitude * diff.magnitude);
    //        }

    //        force /= neighborPlayers.Count;
    //    }



    //    accel += force * param.separationWeight;
    //}

    //private void UpdateAlignment()
    //{
    //    if (neighbors.Count == 0)
    //        return;

    //    var averaveVelocity = Vector3.zero;
    //    foreach (var neighbor in neighbors)
    //    {
    //        averaveVelocity += neighbor.velocity;
    //    }

    //    averaveVelocity /= neighbors.Count;

    //    accel += (averaveVelocity - velocity) * param.alignmentWeight;
    //}
    //void UpdateCohesion()
    //{
    //    var averagePos = Vector3.zero;
    //    if (neighbors.Count != 0)
    //    {

    //        foreach (var neighbor in neighbors)
    //        {
    //            averagePos += neighbor.pos;
    //        }
    //        averagePos /= neighbors.Count;
    //    }
    //    averagePos += orca.position;
    //    averagePos /= 2;

    //    accel += (averagePos - pos) * param.cohesionWeight;
    //}
    private void UpdateMove()
    {
        var dt = Time.deltaTime;

        velocity += accel * dt;
        var dir = velocity.normalized;
        var speed = velocity.magnitude;
        velocity = Mathf.Clamp(speed, param.minSpeed, param.maxSpeed) * dir;
        pos += velocity * dt;

        var rot = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.3f);
        transform.position = pos;
        //rb.velocity = velocity;
        

        accel = Vector3.zero;
    }
}
