using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowFish : MonoBehaviour
{
    public Vector3 direction { get; set; }

    public float speedMin { get; set; }
    public float speedMax { get; set; }

    private float speed;
    private Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedMin, speedMax);
 
        oldPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 diff = transform.position - oldPos;
        oldPos = transform.position;


        transform.Translate(direction * speed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);
    }
}
