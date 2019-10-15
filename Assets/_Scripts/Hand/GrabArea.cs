using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabArea : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Grab grab;
    private GameObject grababbleObject;
    private GameObject grabbedObject;

    private void GrabObj()
    {
        if (grababbleObject == null || grababbleObject.GetComponent<Rigidbody>() == null || grababbleObject.GetComponent<GrabbableObject>() == null)
            return;

        grababbleObject.transform.parent = parent;
        grababbleObject.transform.localPosition = Vector3.zero;
        grababbleObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject = grababbleObject;

        grababbleObject.GetComponent<GrabbableObject>().Excute();
    }

    private void ReleaseObj()
    {
        if (grabbedObject.transform.parent != parent)
            return;

        grabbedObject.transform.parent = null;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.HasGrab() && grababbleObject != null
            && grababbleObject.GetComponent<GrabbableObject>() != null
            && grababbleObject.transform.parent != parent 
            && !grababbleObject.GetComponent<GrabbableObject>().IsGrabbed())
        {
            GrabObj();
        }

        if((grab.HasRelease() && grabbedObject != null))
            ReleaseObj();
    }

    private void OnTriggerEnter(Collider other)
    {
        grababbleObject = other.gameObject;
        if (other.GetComponent<Renderer>() != null && other.GetComponent<GrabbableObject>() != null)
            other.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.1f);
    }

    private void OnTriggerExit(Collider other)
    {
        grababbleObject = null;
        if(other.GetComponent<Renderer>() != null && other.GetComponent<GrabbableObject>() != null)
            other.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0f);
    }
}
