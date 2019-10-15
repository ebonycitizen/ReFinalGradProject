using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovePlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float ratio;

    [SerializeField]
    private float rotateMax;

    [SerializeField]
    private Rigidbody player;

    

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
        var q = target.rotation * Quaternion.Inverse(transform.rotation);
        if (q.w < 0f)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        var torque = new Vector3(q.x, q.y, q.z) * ratio;

        GetComponent<Rigidbody>().AddTorque(torque);

        float rotateMin = 360 - rotateMax;

        Vector3 rot = transform.localEulerAngles;

        if (rot.x > rotateMax && rot.x < 180)
            rot.x = rotateMax;
        if (rot.x < rotateMin && rot.x > 180)
            rot.x = rotateMin;

        if (rot.y > rotateMax && rot.y < 180)
            rot.y = rotateMax;
        if (rot.y < rotateMin && rot.y > 180)
            rot.y = rotateMin;

        transform.eulerAngles = rot;

        Vector3 moveVec = Vector3.zero;

        if (rot.x > 0 && rot.x < 180)
            moveVec.y = -rot.x;
        else
            moveVec.y = (360 - rot.x);

        if (rot.y > 0 && rot.y < 180)
            moveVec.x = rot.y;
        else
            moveVec.x = -(360 - rot.y);

        player.AddForce(100 * (moveVec/5 - player.velocity));

        transform.localPosition = Vector3.zero;
    }

}
