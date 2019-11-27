using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gritter : MonoBehaviour
{
    public GameObject effect;
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
        Destroy(gameObject);
        var obj = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(obj, 2);
    }
}
