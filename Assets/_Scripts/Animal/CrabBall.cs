using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class CrabBall : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private Transform kickTarget;
    [SerializeField]
    private GameObject kickEffect;

    private Vector3 initPos;
    private Sequence s;
    private bool hitPlayer;
    private Transform originParent;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Behaviour behaviour;

    // Start is called before the first frame update
    void Start()
    {
        originParent = transform.parent;
        initPos = transform.position;
        agent.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Kick()
    {
        agent.enabled = false;
        behaviour.enabled = false;
        StopAllCoroutines();

        kickTarget = GameObject.Find("Main Camera (eye)").transform;

        StartCoroutine("MoveToPlayer");
    }

    private IEnumerator MoveToPlayer()
    {
        float time = 0;
        float moveSpeed = 40f;
        float duration = 10f;

        Vector3 initTargetPos = kickTarget.position;
        Vector3 initPos = transform.position;
        //Vector3 kickPos = Vector3.zero;
        Vector3 axis = Random.insideUnitSphere * 360;

        Instantiate(kickEffect, transform.position, kickEffect.transform.rotation);

        while (time < duration)
        {
            if (hitPlayer)
            {
                transform.eulerAngles = new Vector3(-42, -172, -3);
                yield break;
            }
            time += Time.deltaTime;

            //if (time < duration/2)
                //kickPos = kickTarget.position;
            //else
            //    kickPos = initTargetPos;

            transform.position += (kickTarget.position - transform.position).normalized * Time.deltaTime * moveSpeed;
            //transform.position += Vector3.up * 0.03f;

            transform.rotation = Quaternion.AngleAxis(10, axis) * transform.rotation;
            yield return null;
        }

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Head"))
        {
            behaviour.enabled = false;
            agent.enabled = false;
            hitPlayer = true;
            transform.parent = other.transform;
        }

        if(hitPlayer && other.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            behaviour.enabled = false;
            agent.enabled = false;
            transform.parent = originParent;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitPlayer && collision.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            behaviour.enabled = false;
            agent.enabled = false;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = originParent;
            transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
