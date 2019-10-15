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

    public GameObject LockOn(string layer)
    {
        int layerMask = LayerMask.NameToLayer(layer);
        Debug.DrawRay(palmCenter.position, forward * rayLegth * 15);
        bool isHit = Physics.Raycast(palmCenter.position, forward, out hit, rayLegth * 15f, 1 << layerMask);
        //bool isHit = Physics.BoxCast(palmCenter.position, Vector3.one * 0.5f, forward, out hit, palmCenter.rotation, Mathf.Infinity, 1 << layer);

        if (isHit)
            return hit.transform.gameObject;

        return null;
    }

    private float rayLegth = 20;
    public float GetRayLength()
    {
        return rayLegth;
    }

    //private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        hasGrab = false;
        fingers = new List<GameObject>();
        previousFingerCount = fingers.Count;
        previousPos = transform.position;

        //line = GetComponent<LineRenderer>();

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

        if (previousFingerCount != fingers.Count && previousFingerCount == 0 && fingers.Count > 2)
            hasGrab = true;

        previousFingerCount = fingers.Count;

        forward = (palmForward.position - palmCenter.position).normalized;

        //line.SetPosition(0, palmCenter.position);
        //line.SetPosition(1, palmCenter.position + forward * 10);

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FingerTrigger" && fingers.Contains(other.gameObject))
            fingers.Remove(other.gameObject);
    }

}
