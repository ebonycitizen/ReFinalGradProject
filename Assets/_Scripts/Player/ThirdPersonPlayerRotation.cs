using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerRotation : MonoBehaviour
{
    [SerializeField]
    private Transform forwardPos;
    private Vector3 oldForwardPos;

    // Start is called before the first frame update
    void Start()
    {
        oldForwardPos = forwardPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        LookForward();
    }

    private void LookForward()
    {
        var d = forwardPos.position - oldForwardPos;
        if (d.magnitude > 0)
        {
            var q = Quaternion.LookRotation(d);

            if (q.eulerAngles != Vector3.zero)
                transform.localEulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        }
        oldForwardPos = forwardPos.position;
    }
}
