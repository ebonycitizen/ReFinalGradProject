using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyWithCore : MonoBehaviour
{
    [SerializeField]
    private GameObject deadEffect;

    public int coreNum { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        coreNum = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanDisappear()
    {
        if (coreNum == 0)
            StartCoroutine("DeadEffect");
    }

    IEnumerator DeadEffect()
    {
        yield return new WaitForSeconds(0.3f);

        Instantiate(deadEffect, transform);
        transform.parent.DOScale(Vector3.zero, 0.5f);
        Destroy(transform.parent.gameObject, 1);
    }
}
