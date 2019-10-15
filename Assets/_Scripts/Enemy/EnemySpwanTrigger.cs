using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwanTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObj;
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
        if (spawnObj.activeSelf || other.gameObject.layer != LayerMask.NameToLayer("Dolly"))
            return;

        spawnObj.SetActive(true);
    }
}
