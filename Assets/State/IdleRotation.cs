using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRotation : MonoBehaviour
{

    [SerializeField]
    private Transform orca;
    [SerializeField]
    private float torque;
    [SerializeField]
    private float force=200;
    [SerializeField]
    float limit = 60;


    private Rigidbody rb;

    private Vector3 oldPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var h = (orca.transform.localPosition.x - oldPos.x) * force;
        var v = (orca.transform.localPosition.y - oldPos.y) * force;

        rb.AddRelativeTorque(new Vector3(0, h, -h));
        rb.AddRelativeTorque(new Vector3(v, 0, 0));
        var left = orca.transform.TransformVector(Vector3.left);
        var horiLeft = new Vector3(left.x, 0, left.z).normalized;
        rb.AddTorque(Vector3.Cross(left, horiLeft) * torque);

        var forward = orca.transform.TransformVector(Vector3.forward);
        var horiForward = new Vector3(forward.x, 0, forward.z).normalized;
        rb.AddTorque(Vector3.Cross(forward, horiForward) * torque);

        oldPos = orca.transform.localPosition;

        var localAngle = transform.localEulerAngles;
        float maxLimit = limit, minLimit = 360 - limit;

        if (localAngle.z > maxLimit && localAngle.z < 180)
            localAngle.z = maxLimit;
        if (localAngle.z < minLimit && localAngle.z > 180)
            localAngle.z = minLimit;
        transform.localEulerAngles = localAngle;
    }
}
