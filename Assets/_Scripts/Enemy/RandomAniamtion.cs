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

        var randomStartValue = Random.value;

        animator.speed -= 2 * randomStartValue * 0.1f;
    }

}
