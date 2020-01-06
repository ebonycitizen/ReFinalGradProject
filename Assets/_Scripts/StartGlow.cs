using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartGlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        var rimStrength = material.GetFloat("_RimStrength");
        material.SetFloat("_RimStrength", 10f);

        material.DOFloat(rimStrength, "_RimStrength", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
