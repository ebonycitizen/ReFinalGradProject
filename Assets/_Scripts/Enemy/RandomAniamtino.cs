using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAniamtino : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        var randomStartTime=Random.value;

        Invoke("StartAnimation", randomStartTime);
    }

    void StartAnimation()
    {
        animator.SetTrigger("Play");
    }

}
