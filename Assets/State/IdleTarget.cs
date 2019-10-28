using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTarget : MonoBehaviour
{
    [SerializeField]
    private Transform mainCameraEye;
    [SerializeField]
    private float ratio;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        var q = mainCameraEye.rotation * Quaternion.Inverse(transform.rotation);
        if(q.w<0f)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        var torque = new Vector3(q.x, q.y, q.z) * ratio;

        GetComponent<Rigidbody>().AddTorque(torque);
    }
}
