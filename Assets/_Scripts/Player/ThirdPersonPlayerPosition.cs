using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerPosition : MonoBehaviour
{
    [SerializeField]
    private Transform forwardPos;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance;
    private float maxDistance=7;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private float torque;

    private ContactPoint[] c;

    private Rigidbody rb;

    private Vector3 targetPos;

    private Vector3 oldPos;
    private Vector3 oldForwardPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetTargetPos()
    {
        return targetPos;
    }

    private void FixedUpdate()
    {
        MovePos();

        //LookForward();

        Roll();
    }

    private void MovePos()
    {

        RaycastHit hit;
        var direction = Quaternion.Euler(target.localEulerAngles) * transform.forward;

        Debug.DrawRay(transform.parent.position, direction.normalized*34);
        bool isHit = Physics.Raycast(transform.parent.position, direction, out hit,34 , layerMask);

        if (isHit)
            targetPos = transform.parent.InverseTransformPoint(hit.point-direction*10);//distance = (transform.position - hit.point).magnitude;
        else
            targetPos = direction * distance;//distance = maxDistance;

        //targetPos = direction * distance;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 2f);
    }

    private void LookForward()
    {
        var d = forwardPos.position - oldForwardPos;
        if (d.magnitude > 0)
        {
            var q = Quaternion.LookRotation(d);

            if (q.eulerAngles != Vector3.zero)
                transform.localEulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, transform.eulerAngles.z);
        }
        oldForwardPos = forwardPos.position;
    }

    private void Roll()
    {
        var h = (transform.localPosition.x - oldPos.x) * 200;
        var v = (transform.localPosition.y - oldPos.y) * 200;

        rb.AddRelativeTorque(new Vector3(0, h, -h));
        rb.AddRelativeTorque(new Vector3(v, 0, 0));
        var left = transform.TransformVector(Vector3.left);
        var horiLeft = new Vector3(left.x, 0, left.z).normalized;
        rb.AddTorque(Vector3.Cross(left, horiLeft) * torque);

        var forward = transform.TransformVector(Vector3.forward);
        var horiForward = new Vector3(forward.x, 0, forward.z).normalized;
        rb.AddTorque(Vector3.Cross(forward, horiForward) * torque);

        oldPos = transform.localPosition;
    }


    private float time=0;
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] c = collision.contacts;

        Instantiate(hitPrefab, c[0].point, Quaternion.identity);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Stage"))
            return;

        ContactPoint[] c = collision.contacts;

        //Debug.Log(c[0].normal + " " + c[0].separation);
        transform.position = transform.position + c[0].normal * -c[0].separation;

        time += 10;

        Debug.Log(time);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(time);
    }

    #region gomi

    //Vector3 moveDirection = (targetPos - targetPosOld).normalized;

    //if (collideTarget != null)
    //{
    //    foreach (ContactPoint ca in c)
    //    {
    //        if (ca.normal.x != 0)
    //        {
    //            if ((ca.normal.x > 0 && moveDirection.x < 0) || (ca.normal.x < 0 && moveDirection.x > 0))
    //                targetPos.x = targetPosOld.x;
    //        }
    //        if (ca.normal.y != 0)
    //        {
    //            if ((ca.normal.y > 0 && moveDirection.y < 0) || (ca.normal.y < 0 && moveDirection.y > 0))
    //                targetPos.y = targetPosOld.y;
    //        }
    //        if (ca.normal.z != 0)
    //        {
    //            if ((ca.normal.z > 0 && moveDirection.z < 0) || (ca.normal.z < 0 && moveDirection.z > 0))
    //                targetPos.z = targetPosOld.z;
    //        }
    //    }
    //}
    #endregion
}
