using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGritter : MonoBehaviour
{
    public float speed = 1;
    public GameObject gritter;
    public bool IsDrop = false;
    // Start is called before the first frame update
    void Start()
    {
#if DEBUG
        if(IsDrop)
            StartCoroutine("Drop");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Drop()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            yield return new WaitForSeconds(speed);
            Instantiate(gritter, transform.position, Quaternion.identity);
        }
       
    }
}
