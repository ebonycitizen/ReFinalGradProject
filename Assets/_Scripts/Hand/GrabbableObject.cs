using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    private bool isAutoFix;
    [SerializeField]
    private Vector3 fixedPosition;
    [SerializeField]
    private Vector3 fixedRotation;

    protected Transform specificParentR;
    public Transform GetRightPalm()
    { return specificParentR; }

    protected Transform specificParentL;
    public Transform GetLeftPalm()
    { return specificParentL; }

    protected GameObject palmCollisionR;
    public GameObject GetRightPalmCollsion()
    { return palmCollisionR; }

    protected GameObject palmCollisionL;
    public GameObject GetLeftPalmCollision()
    { return palmCollisionL; }

    private Vector3 size;
    private bool isGrabbed;
    public bool IsGrabbed()
    { return isGrabbed; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        size = GetComponent<Collider>().bounds.extents;
        specificParentR = GameObject.Find("PalmCenterRight").transform;
        specificParentL = GameObject.Find("PalmCenterLeft").transform;

        palmCollisionR = GameObject.Find("PalmCollisionRight");
        palmCollisionL = GameObject.Find("PalmCollisionLeft");

        if (isAutoFix)
            fixedPosition = new Vector3(0, -size.y, 0);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        isGrabbed = false;
        if(transform.parent == specificParentR.transform || transform.parent == specificParentL.transform)
        {
            isGrabbed = true;
            if (transform.localPosition != fixedPosition)
                transform.localPosition = fixedPosition;
            if (!isAutoFix && transform.localRotation != Quaternion.Euler(fixedRotation))
            {
                transform.localRotation = Quaternion.Euler(fixedRotation);
            }
        }
    }
    public virtual void Excute()
    {
    }
}
