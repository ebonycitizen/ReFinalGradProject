using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HI5;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Transform palmForward;
    [SerializeField]
    private Transform palmCenter;
    [SerializeField]
    private GameObject indexFinger;
    [SerializeField]
    private GameObject thumb;

    private List<GameObject> fingers;
    private int previousFingerCount;

    private RaycastHit hit;

    private Vector3 previousPos;
    private Vector3 velocity = Vector3.zero;
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    private bool hasGrab;
    public bool HasGrab()
    {
        return hasGrab;
    }

    public Vector3 GetPalmCenterPos()
    {
        return palmCenter.position;
    }

    public int NumberOfGrabFingers()
    {
        return fingers.Count;
    }

    private bool hasRelease;
    public bool HasRelease()
    {
        return hasRelease;
    }

    private Vector3 forward;
    public Vector3 GetForward()
    {
        return forward;
    }

    private bool isPoint;
    public bool GetIsPoint()
    {
        return isPoint;
    }

    private GameObject pointObj;
    public GameObject GetPointObj()
    {
        return pointObj;
    }

    private bool isThumbUp;
    public bool GetIsThumbUp()
    {
        return isThumbUp;
    }

    private bool isApproach;
    public bool GetIsApproach()
    {
        return isApproach;
    }

    private GameObject approachObj;
    public GameObject GetApproachObj()
    {
        return approachObj;
    }

    private LineRenderer line;

    //public GameObject LockOn(LayerMask layerMask)
    //{
    //    //int layerMask = LayerMask.NameToLayer(layer);
    //    Debug.DrawRay(palmCenter.position, forward * rayLegth * 15);
    //    bool isHit = Physics.Raycast(palmCenter.position, forward, out hit, rayLegth * 15f, layerMask);
    //    //bool isHit = Physics.BoxCast(palmCenter.position, Vector3.one * 0.5f, forward, out hit, palmCenter.rotation, Mathf.Infinity, 1 << layer);

    //    if (isHit)
    //        return hit.transform.gameObject;

    //    return null;
    //}

    private float rayLegth = 150;
    public float GetRayLength()
    {
        return rayLegth;
    }

    // Start is called before the first frame update
    void Start()
    {
        hasGrab = false;
        fingers = new List<GameObject>();
        previousFingerCount = fingers.Count;
        previousPos = transform.position;

        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!HI5_Manager.GetGloveStatus().IsGloveAvailable(HI5.Hand.LEFT) &&
              !HI5_Manager.GetGloveStatus().IsGloveAvailable(HI5.Hand.RIGHT))
            return;
        
        if (fingers.Count == 0)
            hasRelease = false;

        if (previousFingerCount != fingers.Count && previousFingerCount > 0 && fingers.Count == 0)
            hasRelease = true;

        hasGrab = false;

        if (previousFingerCount != fingers.Count && previousFingerCount == 0 && fingers.Count > 1)
            hasGrab = true;

        isPoint = false;
        if (fingers.Count > 2 && !fingers.Contains(indexFinger))
            isPoint = true;

        isThumbUp = false;
        if (fingers.Count > 2 && !fingers.Contains(thumb) && (thumb.transform.position - palmCenter.position).normalized.y >= 0.9f)
            isThumbUp = true;

        previousFingerCount = fingers.Count;

        forward = (palmForward.position - palmCenter.position).normalized;

        line.SetPosition(0, palmCenter.position);
        line.SetPosition(1, palmCenter.position + forward * 10);

        if (isPoint)
            line.enabled = true;
        else
            line.enabled = false;
    }

    public GameObject LockOn(LayerMask layerMask)
    {
        bool isHit = Physics.Raycast(transform.position, forward, out hit, rayLegth, layerMask);
        Debug.DrawRay(palmCenter.position, forward * rayLegth );
        if (isHit)
            return hit.transform.gameObject;

        return null;
    }


    private void FixedUpdate()
    {
        if (!HI5_Manager.GetGloveStatus().IsGloveAvailable(HI5.Hand.LEFT) &&
            !HI5_Manager.GetGloveStatus().IsGloveAvailable(HI5.Hand.RIGHT))
            return;

        velocity = (transform.parent.localPosition - previousPos) / Time.fixedDeltaTime;
        previousPos = transform.parent.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FingerTrigger" && !fingers.Contains(other.gameObject))
            fingers.Add(other.gameObject);

        if (other.gameObject.layer == LayerMask.NameToLayer("ApproachTrigger") && fingers.Count == 0)
        {
            isApproach = true;
            approachObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FingerTrigger" && fingers.Contains(other.gameObject))
            fingers.Remove(other.gameObject);

        if (other.gameObject.layer == LayerMask.NameToLayer("ApproachTrigger"))
            isApproach = false;
    }

}
