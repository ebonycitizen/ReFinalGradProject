using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class Dolphin : MonoBehaviour
{
    [SerializeField]
    private float dushSpeed;
    [SerializeField]
    private float restSpeed;

    private float normalSpeed;
    private splineMove splineMove;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        splineMove = GetComponent<splineMove>();
        normalSpeed = splineMove.speed;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dush()
    {
        splineMove.ChangeSpeed(dushSpeed);
        animator.speed = 3f;
    }

    public void Rest()
    {
        splineMove.ChangeSpeed(restSpeed);
        animator.speed = 0.5f;
    }

    public void Normal()
    {
        splineMove.ChangeSpeed(normalSpeed);
        animator.speed = 1f;
    }
}
