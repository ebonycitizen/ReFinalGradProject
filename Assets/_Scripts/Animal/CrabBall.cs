using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        StartCoroutine("RandMove");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Kick()
    {
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
        Vector3 kickPos = Vector3.zero;
        Vector3 axis = Random.insideUnitSphere * 360;

        Instantiate(kickEffect, transform.position, kickEffect.transform.rotation);

        while (time < duration)
        {
            time += Time.deltaTime;

            if (time < duration/2)
                kickPos = kickTarget.position;
            else
                kickPos = initTargetPos;

            transform.position += (kickPos - initPos).normalized * Time.deltaTime * moveSpeed;
            transform.position += Vector3.up * 0.03f;

            transform.rotation = Quaternion.AngleAxis(10, axis) * transform.rotation;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator RandMove()
    {
        Vector3 randPos = initPos + Random.insideUnitSphere * radius;
        Vector3 targetPos = new Vector3(randPos.x, initPos.y, randPos.z);
        Vector3 oldPos = transform.position;
        float speed = 4f;

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position += (targetPos - transform.position).normalized * Time.deltaTime * speed;
            var rotation = Quaternion.LookRotation(transform.position - oldPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * (speed/2));

            oldPos = transform.position;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        StartCoroutine("RandMove");
    }
}
