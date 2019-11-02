using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaCollision : MonoBehaviour
{
    private ContactPoint[] c;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private GameObject bigSplash;
    [SerializeField]
    private GameObject waterSplash;

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.name);
        c = collision.contacts;

        if (collision.gameObject.tag != "Water")
            Instantiate(hitPrefab, c[0].point, Quaternion.identity);
        else
            Instantiate(bigSplash, c[0].point, bigSplash.transform.rotation);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Stage"))
            return;

        c = collision.contacts;

        //Debug.Log(c[0].normal + " " + c[0].separation);
        transform.position = transform.position + c[0].normal * -c[0].separation;

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Instantiate(bigSplash, c[0].point, bigSplash.transform.rotation);
            Instantiate(waterSplash, transform);
        }
    }
}
