using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFlowFish : MonoBehaviour
{
    public float pos { get; set; }
    
    public float speed { get; set; }

    public float speedMin { get; set; }
    public float speedMax { get; set; }

    public FlowBoidCart cart { get; set; }
    public Vector3 spawnBowndSize;

    private void Start()
    {
        //speed = Random.Range(speedMin, speedMax);
    }

    private void Update()
    {
        pos += Time.deltaTime* speed;
        transform.position = cart.GetPosition(pos);
        transform.rotation = cart.GetRotation(pos);
    }

}
