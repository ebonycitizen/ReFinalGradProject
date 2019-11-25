using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

public class PenguinAnimatorCtr : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private NavMeshAgent m_agent;

    // Start is called before the first frame update
    void Start()
    {
        m_agent.ObserveEveryValueChanged(x => x.velocity.magnitude)
            .Subscribe(x =>
            {
                if (x > 0)
                    m_animator.SetBool("Walk", true);
                else
                    m_animator.SetBool("Walk", false);

            });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
