using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEmission : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Emission()
    {
        GetComponent<Collider>().enabled = false;

        var renderer = GetComponents<MeshRenderer>();

        foreach (var r in renderer)
            r.material.EnableKeyword("_EMISSION");

        EmissionAction action = GetComponent<EmissionAction>();
        if (action != null)
            action.DoAction();

        yield return new WaitForSeconds(5f);

        //foreach (var r in renderer)
        //    r.material.DisableKeyword("_EMISSION");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeepLight")
            StartCoroutine("Emission");
    }
}
