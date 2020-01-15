using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Random = UnityEngine.Random;

public class WhaleShout : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float value = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            animator.SetTrigger("Shout");
            //value = Random.value*value/*0~1*value */;
            //Observable.Timer(TimeSpan.FromSeconds(value))
            //    .Subscribe(_ =>
            //    {
            //        animator.SetTrigger("Shout");
            //        //Play
            //    }).AddTo(this);
            
        }
    }
}
