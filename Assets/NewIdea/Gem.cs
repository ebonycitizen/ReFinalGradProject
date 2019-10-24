using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{

    [SerializeField]
    private float drag;

    private Vector3 direction;
    private float speed;

    private Vector2 speedRange=new Vector2(100,120);

    public void Init(Vector3 dir)
    {

        var angle = new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0);

        direction = (Quaternion.Euler(angle) * Vector3.forward).normalized;

        speed = Random.Range(speedRange.x, speedRange.y);

        direction += dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawn");
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    IEnumerator spawn()
    {
        float time = 3;
        while (0 < time)
        {
            var velocity = direction * speed * Time.deltaTime;

            transform.localPosition += velocity;

            speed *= drag;

            time -= Time.deltaTime;

            yield return null;
        }
        yield return null;
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
