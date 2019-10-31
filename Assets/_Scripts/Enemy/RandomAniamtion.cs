using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAniamtion : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        var randomStartTime=Random.value;

        Invoke("StartAnimation", randomStartTime);
    }

    void StartAnimation()
    {
        animator.SetTrigger("Play");
    }

}
