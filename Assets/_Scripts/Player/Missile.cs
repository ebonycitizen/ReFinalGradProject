using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float period;

    [SerializeField]
    private Vector2 initVelocityRangeX;
    [SerializeField]
    private Vector2 initVelocityRangeZ;

    private Vector3 velocity;
    private Vector3 position;

    private GameObject target;

    public void initMissile(GameObject target,int initDirectionX)
    {
        this.target = target;
        velocity.x *= initDirectionX;
    }

    // Start is called before the first frame update
    void Awake()
    {
        position = transform.position;

        velocity.x = Random.Range(initVelocityRangeX.x, initVelocityRangeX.y);
        velocity.z = Random.Range(initVelocityRangeZ.x, initVelocityRangeZ.y);
        velocity.z *= -1;
        Destroy(gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        if(target==null)
        {
            Destroy(gameObject);
            return;
        }

        var acceleration = Vector3.zero;

        var diff = target.transform.position - position;
        acceleration += (diff - velocity * period) * 2f / (period * period);

        period -= Time.deltaTime;
        if(period>0f)
        {
            velocity += acceleration * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        position += velocity * Time.deltaTime;
        transform.position = position;

        transform.rotation = Quaternion.LookRotation(diff);
    }
}
