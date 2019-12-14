using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpark : MonoBehaviour
{
    [SerializeField]
    private GameObject spark;

    // Start is called before the first frame update
    void Awake()
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
            spark.SetActive(true);
        }
    }
}
