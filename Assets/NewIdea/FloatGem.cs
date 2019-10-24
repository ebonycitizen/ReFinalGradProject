using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatGem : MonoBehaviour
{

    [SerializeField]
    private float drag;

    private Vector3 direction;
    private float speed;

    private Vector2 speedRange=new Vector2(100,120);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Hit(Transform player)
    {
        StartCoroutine("Follow", player);
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator Follow(Transform player)
    {
        float time = 0;
        while (1>time)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, time);
            time += Time.deltaTime / 2;
            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }
}
