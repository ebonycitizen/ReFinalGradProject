using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFlowFish : MonoBehaviour
{
    public float pos { get; set; }
    
    public Vector3 randOffset { get; set; }

    public float speed { get; set; }

    public FlowBoidCart cart { get; set; }

    private void Start()
    {

    }

    private void Update()
    {
        pos += Time.deltaTime* speed;
        transform.position = cart.GetPosition(pos) + randOffset;
        transform.rotation = cart.GetRotation(pos);
    }

}
