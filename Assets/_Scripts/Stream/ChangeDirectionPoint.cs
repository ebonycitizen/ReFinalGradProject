using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionPoint : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Fish"))
            return;

        other.GetComponent<FlowFish>().direction = direction;
        //other.GetComponent<FlowFish>().SetPingPong(other.GetComponent<FlowFish>().transform.position.y);
            //= other.GetComponent<FlowFish>().transform.position.y;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + direction);
    }
}
